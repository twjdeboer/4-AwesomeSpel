var url = require("url");											//dunno what happens here
var express = require("express");									//tell node.js to use express
var http = require('http');											//tell node.js to use http requests
var app = express();												//open express
var crypto =require('crypto');
app.use(express.static(__dirname+"/static"));						//tell express where to find the static content

// SERVER CONNECTION
var ip = "localhost";											//server ip adress
var port = 8084;													//get the server port from the constructor in CLI

var server;															//make a server object
server = http.createServer(app);									//create the server
server.listen(port)													//tell the server what port to listen at
console.log("Server listening on port " + port + " on " + ip + '\n');

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

app.get("/adddata", function(req, res) {
	var url_parts = url.parse(req.url, true);
	var query = url_parts.query;
	if (query["type"] != undefined){
		if(query["value"] != undefined){
			var querystring = "INSERT INTO playdata (type, value) VALUES (\'" + query["type"] + "\'," + query["value"] + ");";
		}
		var querystring = "INSERT INTO playdata (type) VALUES (\'" + query["type"] + "\');" ;
		
		mysqlserver.query(querystring, function(err, result){
			if (err) console.log(err);
		});
	} else console.log('ERROR WRITING DATA: undefined data type');
});

app.get("/readdata", function(req, res) {
	var querystring = "SELECT * FROM playdata";
	mysqlserver.query(querystring, function(err, result){
		if (err) console.log(err);
		res.json(result);
	});
});

app.get("/adduser", function(req, res){
	var url_parts = url.parse(req.url, true);
	var query = url_parts.query;
	var username = query["username"];
	var password = query["password"];
	var name = query["name"];
	var email = query["email"];

	var salt = crypto.randomBytes(16);
	var password_h = crypto.createHash('sha256').update(password+salt).digest('base64');

	var sqlquery = "INSERT INTO user (username, password_h, salt, name, email) " +
		"VALUES (?,?,?,?,?);";

	mysqlserver.query(sqlquery, [username, password_h, salt, name, email], function(err, result){
		if (err) console.log(err);
		else //console.log(result);
	});
});

app.get("/validateuser", function(req, res){
	var url_parts = url.parse(req.url, true);
	var query = url_parts.query;
	var username = query["username"];
	var password = query["password"];

	var getsaltquery = "SELECT salt FROM user WHERE username = ?";


	mysqlserver.query(getsaltquery, [username], function(err, result){
		if (err) {
			console.log(err);
		}
		else {
			var salt = result;
			var password_h = crypto.createHash('sha256').update(password+salt).digest('base64');
		}
	});
});