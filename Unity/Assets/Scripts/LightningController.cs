using UnityEngine;
using System.Collections;

public class LightningController : MonoBehaviour {
	private GameObject lightning_spot;
	private GameObject lightning_point;

	private double timer;
	private double lightninglength;
	private double elapsedtime;

	// Use this for initialization
	void Start () {
		lightning_spot = GameObject.Find ("Lightning_spot");
		lightning_point = GameObject.Find ("Lightning_point");

		lightning_spot.GetComponent<Light> ().intensity = 0;
		lightning_point.GetComponent<Light> ().intensity = 0;

		timer = Random.Range (2f, 4f);
		Debug.Log (timer);
	}
	
	// Update is called once per frame
	void Update () {
		elapsedtime += Time.deltaTime;
		lightninglength += Time.deltaTime;
		
		if (elapsedtime > timer) {
			playLightning ();
			timer = Random.Range (40F, 30F);
			elapsedtime = 0;
		}



	}

	void playLightning(){
		lightninglength = 0;
		lightning_spot.GetComponent<Light> ().intensity = 4.75f;
		lightning_point.GetComponent<Light> ().intensity = 3.1f;
		int loop = 1;
		while (loop == 1) {
						if (lightninglength > 0.2) {
								lightning_spot.GetComponent<Light> ().intensity = 0;
								lightning_point.GetComponent<Light> ().intensity = 0;
								loop = 0;
						}
				}

	}
}
