using UnityEngine;
using System.Collections;

public class GotoWorld : MonoBehaviour {

	public GameObject world;

	
	void Awake(){
		world = GameObject.Find ("HouseContainer");
		if (world==null)
			world = GameObject.Find ("Empty");
		world.SetActive(false);
		//DontDestroyOnLoad(players);
	}
	
	void World() {
			world.SetActive(true);
			
		Application.LoadLevel("TestWorldMerge");
	}


	

}
