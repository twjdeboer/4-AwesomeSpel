var url = require("url");											//dunno what happens here
var express = require("express");									//tell node.js to use express
var http = require('http');											//tell node.js to use http requests
var app = express();												//open express
app.use(express.static(__dirname+"/static"));						//tell express where to find the static content

// SERVER CONNECTION
var ip = "https://drproject.twi.tudelft.nl/ewi3620tu4";											//server ip adress
var port = 8084;													//get the server port from the constructor in CLI

var server;															//make a server object
server = http.createServer(app);									//create the server
server.listen(port)													//tell the server what port to listen at
console.log("Server listening on port " + port + " with url " + ip + '\n');

//MYSQL CONNECTION
var mysql = require('mysql');										//tell node.js to use mysql
 
var mysqlserver = mysql.createConnection(							//create a mysql connection with the following details
    {
      host     : 'localhost',
      user     : 'ewi3620tu4',
      password : 'DecCiemkup2',
      database : 'ewi3620tu4',
    }
);

app.get("/adddata", function(req, res) {
	var querystring = "";
	mysqlserver.query(querystring, function(err, result){
		if (err) console.log(err);
	});
});

app.get("/readdata", function(req, res) {
	var querystring = "SELECT * FROM playdata";
	mysqlserver.query(querystring, function(err, result){
		if (err) console.log(err);
		res.json(result);
	});
});