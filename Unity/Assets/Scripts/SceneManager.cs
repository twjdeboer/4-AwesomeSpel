using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Text.RegularExpressions;
using System.IO;

public class SceneManager : MonoBehaviour
{

		private bool gamePaused = false;
		private GameObject pauseMenu;
		private GameObject startMenu;
		private GameObject loginMenu;
		private GameObject createMenu;
		public string firstPlayScene;
		public string startScene;

		// Use this for initialization
		void Start ()
		{
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

		void ToggleMenu (bool toggle)
		{
				if (toggle) {
						setCanvasActive (pauseMenu);
				} else {
						setCanvasInactive (pauseMenu);
				}
		}

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

		void Play ()
		{
				setCanvasInactive (startMenu);
				Application.LoadLevel (firstPlayScene);
		}

		void Resume ()
		{
				SetPause (false);
				ToggleMenu (false);
		}

		void Quit ()
		{
				Application.Quit ();
		}

		// Update is called once per frame
		void Update ()
		{

				TogglePause ();
		}

		void BackToMain ()
		{
				setCanvasActive (startMenu);
				setCanvasInactive (loginMenu);
		}

		void LoginMenu ()
		{
				setCanvasInactive (startMenu);
				setCanvasActive (loginMenu);

		}

		void CreateMenu ()
		{
				setCanvasInactive (loginMenu);
				setCanvasActive (createMenu);
		}

		void BackToLogin ()
		{
				setCanvasActive (loginMenu);
				setCanvasInactive (createMenu);
		}

		void CheckLogin ()
		{
				string username = GameObject.Find ("username").GetComponent<InputField> ().text;
				string password = GameObject.Find ("password").GetComponent<InputField> ().text;
				GameObject.Find ("errortext").GetComponent<Text> ().text = "";
				string url = "http://drproject.twi.tudelft.nl:8084/validateuser?username=" + username + "&password=" + password;
				WWW www = new WWW (url);
				StartCoroutine (GETLogin (www));
		}
	
		IEnumerator GETLogin (WWW www)
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
								int userid = int.Parse( result [1].Split (':') [1]);
				WriteToSave(@"cloud.save", userid, 0f, 0f, new bool[10]);

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
								string userid = result [1].Split (':') [1];
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

		void LoadGame (int playerId)
		{
				string url = "http://drproject.twi.tudelft.nl:8084/readplaydata?id=" + playerId;
				WWW www = new WWW (url);
				StartCoroutine (GETSaveGame (www));
				
		}

		IEnumerator GETSaveGame (WWW www)
		{
				yield return www;

				if (www.error == null) {

				} else {
						Debug.Log ("WWW Error: " + www.error);
				}
		}

		void LoadGameOffline ()
		{

		}

		float[] ReadPlayerPos (string filename)
		{
				float[] pos = new float[2];	

				if (File.Exists (filename)) {

						string[] sc = File.ReadAllLines (filename);
						pos [0] = float.Parse( sc[1] );
						pos [1] = float.Parse( sc[2] );

				} else {
						Debug.Log ("No Save File found");
				}
				return pos;
		}

		bool[] ReadItemList (string filename)
		{
				bool[] items = new bool[10];
				if (File.Exists (filename)) {
			
						string[] content = File.ReadAllLines (filename);
				
						for (int i = 0; i<10; i++) {
								items [i] = bool.Parse( content[i+3]);
						}
						
			
				} else {
						Debug.Log ("No Save File found");
				}
				return items;

		}

		void WriteToSave (string filename, int playerId, float xpos, float ypos, bool[] items)
		{
				StreamWriter sr = File.CreateText (filename);
				sr.WriteLine (playerId);
				sr.WriteLine (xpos);
				sr.WriteLine (ypos);
				foreach (bool item in items) {
						sr.WriteLine (item);	
				}
				sr.Close ();
		}

		void setCanvasActive (GameObject canvas)
		{
				if (canvas != null) {
						canvas.GetComponent<CanvasGroup> ().alpha = 1.0f;
						canvas.GetComponent<CanvasGroup> ().interactable = true;
						canvas.GetComponent<CanvasGroup> ().blocksRaycasts = true;
				}
		}

		void setCanvasInactive (GameObject canvas)
		{
				if (canvas != null) {
						canvas.GetComponent<CanvasGroup> ().alpha = 0.0f;
						canvas.GetComponent<CanvasGroup> ().interactable = false;
						canvas.GetComponent<CanvasGroup> ().blocksRaycasts = false;
				}
		}
}

