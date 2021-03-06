﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Text.RegularExpressions;
using System.IO;
using SimpleJSON;

public class SceneManager : MonoBehaviour
{

		private bool gamePaused = false;
		private GameObject pauseMenu;
		private GameObject startMenu;
		private GameObject loginMenu;
		private GameObject createMenu;
		public string firstPlayScene;
		public string startScene;
		private string backToStart;

		// Use this for initialization
		void Start ()
		{
		backToStart = "startMenu";
			
            Time.timeScale = 1;
				pauseMenu = GameObject.Find ("PauseMenu");
				if (pauseMenu != null)
						setCanvasInactive (pauseMenu);
				startMenu = GameObject.Find ("StartMenu");
				if (startMenu != null) {
						if (Application.loadedLevelName == startScene)
								setCanvasActive (startMenu);
						else
								setCanvasInactive (startMenu);
				}
				loginMenu = GameObject.Find ("Login");
				if (loginMenu != null)
						setCanvasInactive (loginMenu);
				createMenu = GameObject.Find ("Create");
				if (pauseMenu != null)
						setCanvasInactive (createMenu);


		}
		
	// function to set pause boolean
		void SetPause (bool pause)
		{
				if (pause) {
						Time.timeScale = 0;
						GameObject.Find ("PlayerModel").GetComponent<Animator> ().speed = 0;
						gamePaused = true;
				} else {
						Time.timeScale = 1;
						GameObject.Find ("PlayerModel").GetComponent<Animator> ().speed = 1;
						gamePaused = false;

				}
		}

		// show pause menu if boolean toggle is true
		void ToggleMenu (bool toggle)
		{
				if (toggle) {
						setCanvasActive (pauseMenu);
				} else {
						setCanvasInactive (pauseMenu);
				}
		}

		// pause on escape and call pause menu
		void TogglePause ()
		{
				if (Input.GetKeyUp (KeyCode.Escape)) {
						if (!gamePaused) {
								SetPause (true);
								ToggleMenu (true);
						} else {
								ToggleMenu (false);
								SetPause (false);
						}
				}
		}
		
		// go to the first play scene and deactivate startMenu
		void Play ()
		{
				setCanvasInactive (startMenu);
				Application.LoadLevel (firstPlayScene);
		}

		// resume the game
		void Resume ()
		{
				SetPause (false);
				ToggleMenu (false);
		}

		//quit the game when pressing quit in the main menu
		void Exit (){
			Application.Quit ();
		}

		// go back to the main menu from the pause menu
		void Quit ()
		{
			Application.Quit();
		}

		// go back from the main menu from the pause menu in the outside world, saving your player position
		void QuitFromWorld ()
		{
				savePlayerPos ();
				Quit ();
		}

		// save player position in cloud.save
		void savePlayerPos ()
		{
				string filename = "cloud.save";
		
				GameObject playerModel = GameObject.Find ("PlayerModel");
		
				Vector3 playPosOld = ReadPlayerPos (filename);
				Vector3 playPosCur = playerModel.transform.position;
		
				float xNew = playPosCur.x;
				float yNew = playPosCur.y;
				float zNew = playPosCur.z;
		
				string[] content = new string[16];
		
				if (File.Exists (filename)) {
						content = File.ReadAllLines (filename);
				} else {
						Debug.Log ("No Save File found");
				}
		
		
				StreamWriter sr = File.CreateText (filename);
				sr.WriteLine (content [0]);
				sr.WriteLine (xNew);
				sr.WriteLine (yNew);
				sr.WriteLine (zNew);
		
				for (int i = 4; i < content.Length; i++) {
						sr.WriteLine (content [i]);			
				}
				sr.Close ();
				string url = "http://drproject.twi.tudelft.nl:8084/writeplayerpos?userId=" + content [0] + "&xpos=" + xNew + "&ypos=" + yNew + "&zpos=" + zNew;
				WWW www = new WWW (url);
		
				StartCoroutine (GETWritePlayerPos (www));
		
		}

		// sends a get request to write the playerposition to the server
		IEnumerator GETWritePlayerPos (WWW www)
		{
				yield return www;
				if (www.error == null) {
						string response = www.text;
						Debug.Log ("SUCCESS" + response);	
				} else {
						Debug.Log ("WWW Error: " + www.error);
				}
		}

		// read player position from file and return the position
		Vector3 ReadPlayerPos (string filename)
		{
				Vector3 pos = new Vector3 ();
		
				if (File.Exists (filename)) {
			
						string[] sc = File.ReadAllLines (filename);
						float x = float.Parse (sc [1]);
						float y = float.Parse (sc [2]);
						float z = float.Parse (sc [3]);
						pos = new Vector3 (x, y, z);
			
				} else {
						Debug.Log ("No Save File found");
				}
				return pos;
		}
		// Update is called once per frame
		void Update ()
		{

				TogglePause ();
		}
		
		// set startMenu active and loginMenu inactive
		void BackToMain ()
		{
				setCanvasActive (startMenu);
				setCanvasInactive (loginMenu);
		}
		
		// Set startMenu inactive and loginMunu active
		void LoginMenu ()
		{
				setCanvasInactive (startMenu);
				setCanvasActive (loginMenu);

		}
	 	
		// set loginMenu inactive and creatMenu active
		void CreateMenu ()
		{
				setCanvasInactive (loginMenu);
				setCanvasActive (createMenu);
		}

		//  set loginMenu active and createMenu inactive
		void BackToLogin ()
		{
				setCanvasActive (loginMenu);
				setCanvasInactive (createMenu);
		}

		// load the credits scene
		void Credits ()
		{
				Application.LoadLevel ("Credits");
		}

		// check the login details
		void CheckLogin ()
		{
				string username = GameObject.Find ("username").GetComponent<InputField> ().text;
				string password = GameObject.Find ("password").GetComponent<InputField> ().text;
				GameObject.Find ("errortext").GetComponent<Text> ().text = "";
				string url = "http://drproject.twi.tudelft.nl:8084/validateuser?username=" + username + "&password=" + password;
				WWW www = new WWW (url);
				StartCoroutine (GETLogin (www));
		}

		// send a get request to check the login details on the server
		IEnumerator GETLogin (WWW www)
		{
				yield return www;

				// check for errors
				if (www.error == null) {
						string jsonresponse = www.text;

				
						JSONNode response = JSON.Parse (jsonresponse);
	
						string msg = response ["msg"];
						switch (msg) {
						case ("SUCCESS"):
								int userid = int.Parse (response ["userid"]);
								float xpos = float.Parse (response ["xpos"]);
								float ypos = float.Parse (response ["ypos"]);
								float zpos = float.Parse (response ["zpos"]);
								Vector3 playpos = new Vector3 (xpos, ypos, zpos);
								bool[] items = new bool[12];
								bool hostop = false;
								int index = 0;
								string itemId;
								while (!hostop) {
										itemId = response ["items"] [index] ["itemId"];
										if (itemId == null || index > 20) {
												hostop = true;
										} else {
												items [int.Parse (itemId)] = true;
										}
										index++;
								}
								WriteToSave (@"cloud.save", userid, playpos, items);

								setCanvasInactive (startMenu);
								Application.LoadLevel (firstPlayScene);
								break;
						case ("INVALID USER"):
								GameObject.Find ("errortext").GetComponent<Text> ().text = "invalid username";
								GameObject.Find ("password").GetComponent<InputField> ().text = "";
								break;
						case ("INVALID PASSWORD"):
								GameObject.Find ("errortext").GetComponent<Text> ().text = "invalid password";
								GameObject.Find ("password").GetComponent<InputField> ().text = "";
								break;
						default:
								Debug.Log ("Fatal Error!");
								break;
						}
				} else {
						Debug.Log ("WWW Error: " + www.error);
				}
		}

		//  create an account with username and password
		void CreateAccount ()
		{
				GameObject error = GameObject.Find ("errortextcreate");
				string username = GameObject.Find ("newusername").GetComponent<InputField> ().text;
				string password = GameObject.Find ("newpassword").GetComponent<InputField> ().text;
				string passwordR = GameObject.Find ("newpasswordrepeat").GetComponent<InputField> ().text;
				string name = GameObject.Find ("newname").GetComponent<InputField> ().text;
				string email = GameObject.Find ("newemail").GetComponent<InputField> ().text;

				bool emailcheck = Regex.IsMatch (email,
						@"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
						@"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$",
						RegexOptions.IgnoreCase);

				error.GetComponent<Text> ().text = "";

				if (username == "") {
						error.GetComponent<Text> ().text = "Please enter a name";
				} else if (password == "" || passwordR == "") {
						error.GetComponent<Text> ().text = "Please enter and Repeat your password";
						GameObject.Find ("newpassword").GetComponent<InputField> ().text = "";
						GameObject.Find ("newpasswordrepeat").GetComponent<InputField> ().text = "";
				} else if (name == "") {
						error.GetComponent<Text> ().text = "Please enter a name";
				} else if (email == "" || !emailcheck) {
						error.GetComponent<Text> ().text = "Please enter an valid e-mail address";
				} else if (password != passwordR) {
						error.GetComponent<Text> ().text = "Passwords do not match";
						GameObject.Find ("newpassword").GetComponent<InputField> ().text = "";
						GameObject.Find ("newpasswordrepeat").GetComponent<InputField> ().text = "";
				} else {
						string url = "http://drproject.twi.tudelft.nl:8084/adduser?username=" + username + "&password=" + password +
								"&name=" + name + "&email=" + email;
						WWW www = new WWW (url);
						StartCoroutine (GETCreate (www));
				}

		}

		// send the getrequest to the server and check the username and password
		IEnumerator GETCreate (WWW www)
		{
				yield return www;

				// check for errors
				if (www.error == null) {
						string response = www.text;
						string[] result = response.Substring (1, response.Length - 2).Split (',');
						string msg = result [0].Split (':') [1];
						msg = msg.Substring (1, msg.Length - 2);
						switch (msg) {
						case ("SUCCESS"):
								setCanvasInactive (startMenu);
								Application.LoadLevel (firstPlayScene);
								break;
						case ("DUPLICATE USER"):
								GameObject.Find ("errortextcreate").GetComponent<Text> ().text = "Username already in use";
								break;
						default:
								Debug.Log ("Fatal Error!");
								break;
						}
				} else {
						Debug.Log ("WWW Error: " + www.error);
				}
		}

		// create an offline game with a local save file
		void LoadGameOffline ()
		{
				string filename = "cloud.save";
				if (File.Exists (filename)) {
						string[] content = File.ReadAllLines (filename);
						if (content [0] != "0") {
								CreateSaveOffline ();
						}
				} else {
						CreateSaveOffline ();
				}
				setCanvasInactive (startMenu);
				Application.LoadLevel (firstPlayScene);
		}

		// save player statistics to the local cloud.save file
		void CreateSaveOffline ()
		{
				Vector3 playPos = new Vector3 (-185f, 0.2f, -35f);
				bool[] items = new bool[12];
				WriteToSave ("cloud.save", 0, playPos, items);
		}


		// read booleans from cloud to save file to check player statistics
		public static bool[] ReadItemList (string filename)
		{
				bool[] items = new bool[10];
				if (File.Exists (filename)) {

						string[] content = File.ReadAllLines (filename);

						for (int i = 0; i<10; i++) {
								items [i] = bool.Parse (content [i + 4]);
						}


				} else {
						Debug.Log ("No Save File found");
				}
				return items;

		}

		// write player statistics to file
		void WriteToSave (string filename, int playerId, Vector3 playPos, bool[] items)
		{
				StreamWriter sr = File.CreateText (filename);
				sr.WriteLine (playerId);
				sr.WriteLine (playPos.x);
				sr.WriteLine (playPos.y);
				sr.WriteLine (playPos.z);
				foreach (bool item in items) {
						sr.WriteLine (item);
				}
				sr.Close ();
		}

		// set canvas active and make interactable
		void setCanvasActive (GameObject canvas)
		{
				if (canvas != null) {
						canvas.GetComponent<CanvasGroup> ().alpha = 1.0f;
						canvas.GetComponent<CanvasGroup> ().interactable = true;
						canvas.GetComponent<CanvasGroup> ().blocksRaycasts = true;
				}
		}

		// set canvas inactive and make not interactible
		void setCanvasInactive (GameObject canvas)
		{
				if (canvas != null) {
						canvas.GetComponent<CanvasGroup> ().alpha = 0.0f;
						canvas.GetComponent<CanvasGroup> ().interactable = false;
						canvas.GetComponent<CanvasGroup> ().blocksRaycasts = false;
				}
		}
}

