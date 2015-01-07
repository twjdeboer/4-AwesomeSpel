#pragma strict

function Start () {

}

function Update () {

}

function CheckLogin() {
		var username = GameObject.Find ("username").GetComponent<InputField>().text;
		var password = GameObject.Find ("password").GetComponent<InputField>().text;
		
		var req = {user: username, pass: password};
		
		var xmlHttp = null;
		
		xmlHttp = new XMLHttpRequest();
		xmlHttp.open( "GET", 
}