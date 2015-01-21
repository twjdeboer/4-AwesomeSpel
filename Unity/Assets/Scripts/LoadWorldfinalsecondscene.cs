using UnityEngine;
using System.Collections;

//not used
public class LoadWorldfinalsecondscene : MonoBehaviour {

	public GameObject world;
	//public GameObject players;

	void Awake(){
		world = GameObject.Find ("HouseContainer");
		if (world == null)
			world = GameObject.Find ("Empty");
		//players = GameObject.Find ("Playerall");
		//players.SetActive (false);
		world.SetActive(false);
		//DontDestroyOnLoad(players);
	}

	void Update () {
		if (Input.GetKeyDown ("space")) {
			world.SetActive(true);
			//players.SetActive (true);

			Application.LoadLevel("TestWorldMerge");
		}
	}

	
	
}
