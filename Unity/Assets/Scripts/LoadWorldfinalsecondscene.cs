﻿using UnityEngine;
using System.Collections;

public class LoadWorldfinalsecondscene : MonoBehaviour {

	void Update () {
		if (Input.GetKeyDown ("space")) {
			Application.LoadLevel("World final second scene");
		}
	}
}
