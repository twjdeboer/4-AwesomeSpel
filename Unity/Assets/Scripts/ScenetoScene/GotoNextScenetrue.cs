using UnityEngine;
using System.Collections;

public class GotoNextScenetrue : MonoBehaviour {
	public GameObject players;
	public string[] nextscenestring;
	void Awake(){
				
			players = GameObject.FindGameObjectWithTag ("Player");//GameObject.Find ("Playerall");
				//test inside /test inside2;
	}

	void start(){

		//Player.t		ransform.position += ResourceManager.newplayerpos;
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



}
