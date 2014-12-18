using UnityEngine;
using System.Collections;

public class Nextscenetrue : MonoBehaviour {
	private GameObject world;
	private GameObject players;
	void awake(){
		world = GameObject.Find ("HouseContainer");
		players = GameObject.Find ("Playerall");
		world.SetActive(true);
		players.SetActive (true);
		DontDestroyOnLoad(players);
	}

	void start(){

		//Player.transform.position += ResourceManager.newplayerpos;
		//Cameraplayer.transform.position += ResourceManager.newplayerpos;
		}

	void scene1(){
		players.SetActive (false);
		//world.SetActive(false);
		//ResourceManager.playerPosition= (Player.transform.position);
		//ResourceManager.newplayerpos.Set (-3,0.4f,0);
		Application.LoadLevel ("test inside");

	}

	void scene2(){
		players.SetActive (false);
		//world.SetActive(false);
		//ResourceManager.playerPosition=(Player.transform.position);

		//ResourceManager.newplayerpos.Set (-3,0.4f,0);
		Application.LoadLevel ("test inside2");
		
	}

}
