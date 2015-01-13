using UnityEngine;
using System.Collections;

public class Camchange : MonoBehaviour {
	public Camera c1;
	public Camera c2;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey ("m")) {
			c1.rect = new Rect(0,0,1,1);
			//c2.enabled = false;
		} else {
			//c1.enabled = false;
			c1.rect = new Rect(0.7f,0.7f,1,1);
		}

	}
}
