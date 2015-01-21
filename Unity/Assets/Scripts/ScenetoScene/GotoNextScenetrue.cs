using UnityEngine;
using System.Collections;
using System.IO;

public class GotoNextScenetrue : MonoBehaviour {

	private GameObject player;
	public int available_scenes=9;
	public string[] nextscenestring;


	void Awake(){

		player = GameObject.Find ("Player");

		Vector3 playerpos = ReadPlayerPos ("cloud.save");
		player.transform.position = playerpos;
	}

	Vector3 ReadPlayerPos (string filename)
	{
		Vector3 pos = new Vector3();
		
		if (File.Exists (filename)) {
			
			string[] sc = File.ReadAllLines (filename);
			float x = float.Parse (sc [1]);
			float y = float.Parse (sc [2]);
			float z = float.Parse (sc [3]);
			pos = new Vector3(x, y, z);
			
		} else {
			Debug.Log ("No Save File found");
            pos = player.transform.position;
		}
		return pos;
	}

	void savePlayerPos(){
		string filename = "cloud.save";

		GameObject playerModel = GameObject.Find ("PlayerModel");

		Vector3 playPosOld = ReadPlayerPos(filename);
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
		WWW www = new WWW(url);

		StartCoroutine (GETWritePlayerPos (www));
		
	}

	IEnumerator GETWritePlayerPos(WWW www){
		yield return www;
		if (www.error == null) {
			string response = www.text;
			Debug.Log ("SUCCESS" + response);	
		} else {
			Debug.Log ("WWW Error: " + www.error);
				}
	}
	

	/*
	 * goto scene  nextscenestring
	 * function will be run from button and goes to the scene of the string
	 * 
	 */
	void scene0(){
		//ResourceManager.playerPosition = new Vector3 ();
		savePlayerPos ();
		Application.LoadLevel (nextscenestring[0]);

	}

	void scene1(){
				savePlayerPos ();
		Application.LoadLevel (nextscenestring[1]);
		
	}
	void scene2(){
		savePlayerPos ();
		Application.LoadLevel (nextscenestring[2]);
	}

	void scene3(){
		savePlayerPos ();
		Application.LoadLevel (nextscenestring[3]);
	}

	void scene4(){
		savePlayerPos ();
		Application.LoadLevel (nextscenestring[4]);
	}

	void scene5(){
		savePlayerPos ();
		Application.LoadLevel (nextscenestring[5]);
	}

	void scene6(){
		savePlayerPos ();
		Application.LoadLevel (nextscenestring[6]);
	}

	void scene7(){
		savePlayerPos ();
		Application.LoadLevel (nextscenestring[7]);
	}

	void scene8(){
		savePlayerPos ();
		Application.LoadLevel (nextscenestring[8]);
	}

}
