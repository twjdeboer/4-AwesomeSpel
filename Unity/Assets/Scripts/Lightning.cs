using UnityEngine;
using System.Collections;

public class Lightning : MonoBehaviour {
	private double timer;
	private double elapsedtime;
	// Use this for initialization
	void Start () {
		timer = Random.Range (3F, 10F);

	}
	
	// Update is called once per frame
	void Update () {
		elapsedtime = elapsedtime + 0.1F;

		if (elapsedtime > timer) {
						playLightning ();
				timer = Random.Range (5F, 10F);
				}

	}

	private void playLightning(){

	}
}