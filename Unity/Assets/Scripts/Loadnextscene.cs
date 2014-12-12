using UnityEngine;
using System.Collections;

public class Loadnextscene : MonoBehaviour {
	GameObject[] housboxes;

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown ("space")) {
			ResourceManager.World.SetActive(false);
			Application.LoadLevel("test inside");
		}

	}
}
