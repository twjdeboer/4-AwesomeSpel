using UnityEngine;
using System.Collections;

public class GotoWorld : MonoBehaviour {

	public GameObject world;
	public string nextscene;
	
	void Awake(){
		world = GameObject.Find ("HouseContainer");
		if (nextscene == null) {
			nextscene ="Nextscenetest2";	
		}
		world.SetActive(false);
		//DontDestroyOnLoad(players);
	}
	
	void World() {
			world.SetActive(true);
			
			Application.LoadLevel(nextscene);
	}


	

}
