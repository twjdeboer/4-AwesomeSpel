using UnityEngine;
using System.Collections;

public class testscreenshot : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey ("p")) {
			Application.CaptureScreenshot("SCREENSHOTTEST.png");
		}

	}
}
