using UnityEngine;
using System.Collections;

public class Nextscenetrue : MonoBehaviour {
	public GameObject world;
	public GameObject players;
	void Awake(){
		world = GameObject.Find ("HouseContainer");
		players = GameObject.FindGameObjectWithTag ("EditorOnly");//GameObject.Find ("Playerall");
		//world.SetActive(true);
		//players.SetActive (true);
		DontDestroyOnLoad(players);
		DontDestroyOnLoad (world);
	}

	void start(){

		//Player.transform.position += ResourceManager.newplayerpos;
		//Cameraplayer.transform.position += ResourceManager.newplayerpos;
		}

	void scene1(){
		//players.SetActive (false);
		//world.SetActive(false);
		//ResourceManager.playerPosition= (Player.transform.position);
		//ResourceManager.newplayerpos.Set (-3,0.4f,0);
		Application.LoadLevel ("test inside");

	}

	void scene2(){
		//players.SetActive (false);
		//world.SetActive(false);
		//ResourceManager.playerPosition=(Player.transform.position);

		//ResourceManager.newplayerpos.Set (-3,0.4f,0);
		Application.LoadLevel ("test inside2");
		
	}

}
