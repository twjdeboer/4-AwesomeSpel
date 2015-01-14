var url = require("url");											//package for reading GETs from the url
var express = require("express");									//tell node.js to use express
var http = require('http');											//tell node.js to use http requests
var nodemailer = require('nodemailer');								//user management mailer package
var app = express();												//open express
var crypto =require('crypto');
app.use(express.static(__dirname+"/static"));						//tell express where to find the static content

// SERVER CONNECTION
var ip = "localhost";												//server ip adress
var port = 8084;													//get the server port from the constructor in CLI

var server;															//make a server object
server = http.createServer(app);									//create the server
server.listen(port)													//tell the server what port to listen at
console.log('\n' + "Server listening on port " + port + " on " + ip + '\n');

var mysql = require('mysql');
//MYSQL CONNECTION
function getMySQLConnection(){

	var mysqlserver = mysql.createConnection(							//create a mysql connection with the following details
	    {
	      host     : 'localhost',
	      user     : 'ewi3620tu4',
	      password : 'DecCiemkup2',
	      database : 'ewi3620tu4',
	    }
	);

	mysqlserver.connect();
	return mysqlserver;

}

	//dumpdatabase
app.get("/datadump", function(req, res){
	mysqlserver = getMySQLConnection();
	mysqlserver.query("SELECT * FROM user", function(err, result1){
		if (err) console.log(err);
		else{
			mysqlserver.query("SELECT * FROM statistics", function(err, result2){
				if (err) console.log(err);
				else{
					mysqlserver.query("SELECT * FROM itemdata", function(err, result3){
						if (err) console.log(err);
						else{
							console.log('Someone Requested all SQL data from ip ' + req.connection.remoteAddress);
							res.json([result1, result2, result3]);
						}
					});
					mysqlserver.end();	
				}
			});
		}
	});
});


//MAIL PROVIDER
var mailer = nodemailer.createTransport({
    service: 'gmail',
    auth: {
        user: 'awesomespel@gmail.com',
        pass: 'DecCiemkup2'
    }
});


//GAME STATISTICS
	//basic commands
app.get("/readdata", function(req, res) {
	var querystring = "SELECT * FROM statistics";
	mysqlserver = getMySQLConnection();
	mysqlserver.query(querystring, function(err, result){
		if (err) console.log(err);
		res.json(result);
	});
	mysqlserver.end();
});

	//adddata
app.get("/adddata", function(req, res){
	var url_parts = url.parse(req.url, true);
	var query = url_parts.query;
	var userid = query["userid"];
	var type = query["type"];
	var value = query["value"];
	if (value === null){
		value = 1;
	}	
	if (type != null){
		if (userid != null){	
			var sqlquery = "INSERT INTO statistics (type, value, userId) VALUES (?,?,?)";
			mysqlserver = getMySQLConnection();
			mysqlserver.query(sqlquery, [type, value, userid], function(err,result){
				if (err) {
					console.log(err)
					res.json();
				}else{
					console.log("Succesfully added data of type " + type + " for user with id " + userid);
					res.json({msg:"SUCCESS"});
				}
			});
			mysqlserver.end();
		} else {
			console.log('ERROR WRITING DATA: unknown user');
			res.json({msg:"UNKNOWN USER"});
		}
	} else {
		console.log('ERROR WRITING DATA: undefined data type');
		res.json({msg:"UNKNOWN TYPE"});
	}
});


//USER MANAGEMENT
	//adduser
app.get("/adduser", function(req, res){
	var url_parts = url.parse(req.url, true);
	var query = url_parts.query;
	var username = query["username"];
	var password = query["password"];
	var name = query["name"];
	var email = query["email"];
	if (username != null && name != null && password != null){
		var salt = crypto.randomBytes(16);
		var password_h = crypto.createHash('sha256').update(password+salt).digest('base64');
		var sqlquery = "INSERT INTO user (username, password_h, salt, name, email) " +
			"VALUES (?,?,?,?,?);";
		mysqlserver = getMySQLConnection();
		mysqlserver.query(sqlquery, [username, password_h, salt, name, email], function(err, result){
			if (err) {
				console.log(err);
				res.json({msg:"DUPLICATE USER"});
			} else {
				console.log("Succesfully added user " + username);
				res.json({msg:"SUCCESS", userid:result.insertId});

				mailer.sendMail({
				    to: email,
				    subject: 'Welcome to AwesomeSpel!',
				    text: ('Hey there ' + name + ',\n\nThanks for creating an account for AwesomeSpel. This will allow you to use our awesome Cloud Save System and to view Statistics of your playthrough. ' +
				    	'And the best thing is, you don\'t have to do anything: Everything is integrated in the game!' +
				    	'\n\nYou can log in from the game using your username:\n\t' + username + "\nand the password you gave us while creating the account.\n\n\n\n" +
				    	'Enjoy playing our game and please send us any feedback you might have! You can reach us at awesomespel@gmail.com\n\n' +
				    	'Sincerely,\n\tThe AwesomeSpel Team.\n\nThis user management system was developed by our team and uses a secure database, which stores your password with a 16byte salt that changes every time you log in. '+
				    	'Your password is hashed using the secure SHA256 algorithm. Our serverside system uses escapes and prepared statements to prevent malicious users from gaining access.')
				},function(err){
					if (err) console.log(err);
				});
			}
		});
		mysqlserver.end();
	} else {
		res.json({msg:"INVALID INPUT"});
		console.log("Failed to add user, incomplete user data.")
	}
});

	//user validation
app.get("/validateuser", function(req, res){
	var url_parts = url.parse(req.url, true);
	var query = url_parts.query;
	var username = query["username"];
	var password = query["password"];
	if (username != null && password != null){
		var getsaltquery = "SELECT id, salt, password_h, xpos, ypos, zpos FROM user WHERE username = ?";
		mysqlserver = getMySQLConnection();
		mysqlserver.query(getsaltquery, [username], function(err, resultuser){
			if (err) {
				console.log(err);
			} else {
				if (resultuser != ""){
					var salt = resultuser[0]["salt"];
					var server_password_h = resultuser[0]["password_h"];
					var id = resultuser[0]["id"];
					var xpos = resultuser[0]["xpos"];
					var ypos = resultuser[0]["ypos"];
					var zpos = resultuser[0]["zpos"];
					var password_h = crypto.createHash('sha256').update(password+salt).digest('base64');
					if (password_h === server_password_h){
						var salt = crypto.randomBytes(16);
						var password_h = crypto.createHash('sha256').update(password+salt).digest('base64');
						var sqlquery = "UPDATE user SET password_h = ?, salt = ? WHERE username = ?";
						mysqlserver.query(sqlquery, [password_h, salt, username], function(err){
							if (err) {
								console.log(err);
								res.json({msg:"FATAL ERROR COMMUNICATING WITH DATABASE"});
							} else {
								var sqlquery = "SELECT itemId FROM itemdata WHERE userId = ?";
								mysqlserver.query(sqlquery, [id], function(err, resultitems){
									if (err) console.log(err);
									else {
										res.json({msg:"SUCCESS", userid:id, xpos:xpos, ypos:ypos, zpos:zpos, items:resultitems});
									}
								});
								mysqlserver.end();
							}
						});
					} else {
						res.json({msg:"INVALID PASSWORD"})
					}
				} else {
					res.json({msg:"INVALID USER"})
				}
			}
		});
	} else {
		console.log("Invalid query")
	}
});

	//lostpassword
app.get("/lostpassword", function(req,res){
	res.json({msg:'Dat is vervelend voor je. Jammer joh!'});

});

app.get("/readitemdata", function(req, res){
	var url_parts = url.parse(req.url, true);
	var query = url_parts.query;
	var userId = query["userId"];
	if (userId != null){
		var querystring = 'SELECT itemId FROM itemdata WHERE userId = ?';
		var mysqlserver = getMySQLConnection();
		mysqlserver.query(querystring, [userId], function(err, result){
			if (err) console.log(err);
			else {
				res.json(result);				
			}
		});
		mysqlserver.end();
	}
});
	//pickupitem

app.get("/pickupitem", function(req, res){
	var url_parts = url.parse(req.url, true);
	var query = url_parts.query;
	var userId = query["userId"];
	var itemId = query["itemId"];
	if (userId != null && itemId != null){
		var querystring = 'INSERT INTO itemdata (itemId, userId) VALUES (?, ?)';
		var mysqlserver = getMySQLConnection();
		mysqlserver.query(querystring, [itemId, userId], function(err, result){
			if (err) console.log(err);
		});
		mysqlserver.end();
		res.end();
	}
});

app.get("/writeplayerpos", function(req, res){
	var url_parts = url.parse(req.url, true);
	var query = url_parts.query;
	var userId = query["userId"];
	var xpos = query["xpos"];
	var ypos = query["ypos"];
	var zpos = query["zpos"];
	if (userId != null && xpos != null && ypos != null && zpos  != null){
		var querystring = 'UPDATE user SET xpos = ?, ypos = ?, zpos = ? WHERE id = ?'
		var mysqlserver = getMySQLConnection();
		mysqlserver.query(querystring, [xpos, ypos, zpos, userId], function(err, result){
			if (err) console.log(err); else console.log("Received New Coordinates for player with id: " + userId);
		});
		mysqlserver.end();	
	}
	res.end();
});

app.get("/readplaypos", function(req,res){
	var url_parts = url.parse(req.url, true);
	var query = url_parts.query;
	var userId = query["userId"];

	if (userId != null){
		var querystring = 'SELECT xpos, ypos, zpos FROM user WHERE id = ?';
		var mysqlserver = getMySQLConnection();
		mysqlserver.query(querystring, [userId], function(err, result){
			if (err) console.log(err);
			res.json(result);

		});
		mysqlserver.end();
	}
});