using UnityEngine;
using System.Collections;

public class GotoWorld : MonoBehaviour {

	public GameObject world;


	/*
	 *search for housecontainer and set inactive
	 * if housecontainer not found search empty.
	 */
	void Awake(){
		world = GameObject.Find ("HouseContainer");
		if (world==null)
			world = GameObject.Find ("Empty");
		world.SetActive(false);
		//DontDestroyOnLoad(players);
	}

	/*
	 *set housecontainer active on scene exit
	 *and load world again
	 */
	void World() {
			world.SetActive(true);
			
		Application.LoadLevel("TestWorldMerge");
	}


	

}
