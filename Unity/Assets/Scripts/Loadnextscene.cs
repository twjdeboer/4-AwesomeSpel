using UnityEngine;
using System.Collections;

public class Loadnextscene : MonoBehaviour {
	public string nextscene;
	public bool activeworld;
	public string returned;

	void start(){
		bool activeworl = false;
		string nextsc ="test inside";
		print ("Loadnextscene script still in use?");
		//ResourceManager.World.SetActive(!activeworld);
	}


	void OnTriggerEnter(Collider other) {
		//ResourceManager.World.SetActive(activeworld);
		Application.LoadLevel(nextscene);
	}
}
