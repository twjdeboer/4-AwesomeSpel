using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GotoNextScene : MonoBehaviour {
	public GameObject Buttons;


	/*
	 * als trigger enter set(assigned) button active
	 */
	void OnTriggerEnter(Collider other) {
		if(other.gameObject.tag=="Player")
			Buttons.SetActive (true);
	}

	/*
	 * if trigger exit set(assigned) button inactive
	 */
	void OnTriggerExit(Collider other){
		if(other.gameObject.tag=="Player")
			Buttons.SetActive (false);
	}
}
