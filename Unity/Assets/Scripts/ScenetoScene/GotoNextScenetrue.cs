using UnityEngine;
using System.Collections;
using System.IO;

public class GotoNextScenetrue : MonoBehaviour {
	public GameObject players;
	private GameObject player;
	public int available_scenes=9;
	public string[] nextscenestring;

	void Awake(){
			players = GameObject.FindGameObjectWithTag ("Player");//GameObject.Find ("Playerall");
				//test inside /test inside2;
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
		}
		return pos;
	}
	
	void start(){
		
		//Player.transform.position += ResourceManager.newplayerpos;
		//Cameraplayer.transform.position += ResourceManager.newplayerpos;
		}

	void scene0(){
		//ResourceManager.playerPosition = new Vector3 ();

		Application.LoadLevel (nextscenestring[0]);

	}

	void scene1(){
		//ResourceManager.playerPosition=(Player.transform.position);

		//ResourceManager.newplayerpos.Set (-3,0.4f,0);

		Application.LoadLevel (nextscenestring[1]);
		
	}
	void scene2(){
		Application.LoadLevel (nextscenestring[2]);
	}

	void scene3(){
		Application.LoadLevel (nextscenestring[3]);
	}

	void scene4(){
		Application.LoadLevel (nextscenestring[4]);
	}

	void scene5(){
		Application.LoadLevel (nextscenestring[5]);
	}

	void scene6(){
		Application.LoadLevel (nextscenestring[6]);
	}

	void scene7(){
		Application.LoadLevel (nextscenestring[7]);
	}

	void scene8(){
		Application.LoadLevel (nextscenestring[8]);
	}

}
