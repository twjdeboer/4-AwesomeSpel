using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class testscreenshot : MonoBehaviour {

	private static int number;
	// Use this for initialization
	void Start () {
		number = 0;
		if (!System.IO.Directory.Exists (Application.dataPath+"/screenshots/"))
			System.IO.Directory.CreateDirectory (Application.dataPath+"/screenshots/");

	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown("p")) {
			Application.CaptureScreenshot(Application.dataPath+"/screenshots/Screenshot_"+number+"-"+System.DateTime.Now.Year+"_"+System.DateTime.Now.Month+"_"+System.DateTime.Now.Day+"_"+System.DateTime.Now.Hour+"_"+System.DateTime.Now.Minute+"_"+System.DateTime.Now.Second+".png");
			number++;
		}

	}
}
