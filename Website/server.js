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
var mysqlserver = mysql.createConnection(							//create a mysql connection with the following details
    {
      host     : 'localhost',
      user     : 'ewi3620tu4',
      password : 'DecCiemkup2',
      database : 'ewi3620tu4',
    }
);

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
	mysqlserver.query(querystring, function(err, result){
		if (err) console.log(err);
		res.json(result);
	});
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
			mysqlserver.query(sqlquery, [type, value, userid], function(err,result){
				if (err) {
					console.log(err)
					res.json();
				}else{
					console.log("Succesfully added data of type " + type + " for user with id " + userid);
					res.json({msg:"SUCCESS"});
				}
			});
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
		mysqlserver.query(sqlquery, [username, password_h, salt, name, email], function(err, result){
			if (err) {
				console.log(err);
				res.json({msg:"DUPLICATE USER"});
			} else {
				console.log("Succesfully added user " + username)
				res.json({msg:"SUCCESS"});

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
		var getsaltquery = "SELECT id, salt, password_h FROM user WHERE username = ?";
		mysqlserver.query(getsaltquery, [username], function(err, result){
			if (err) {
				console.log(err);
			} else {
				if (result != ""){
					var salt = result[0]["salt"];
					var server_password_h = result[0]["password_h"];
					var id = result[0]["id"];
					var password_h = crypto.createHash('sha256').update(password+salt).digest('base64');
					if (password_h === server_password_h){
						var salt = crypto.randomBytes(16);
						var password_h = crypto.createHash('sha256').update(password+salt).digest('base64');
						var sqlquery = "UPDATE user SET password_h = ?, salt = ? WHERE username = ?";
						mysqlserver.query(sqlquery, [password_h, salt, username], function(err, result){
							if (err) {
								console.log(err);
								res.json({msg:"FATAL ERROR COMMUNICATING WITH DATABASE"});
							} else {
								res.json({msg:"SUCCESS", userid:id});
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


});

	//pickupitem