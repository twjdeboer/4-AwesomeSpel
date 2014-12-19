using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GotoNextScene : MonoBehaviour {
	public GameObject Buttons;

	void OnTriggerEnter(Collider other) {
		if(other.gameObject.tag=="Player")
			Buttons.SetActive (true);
	}

	void OnTriggerExit(Collider other){
		if(other.gameObject.tag=="Player")
			Buttons.SetActive (false);
	}
}
