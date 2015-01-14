using UnityEngine;
using System.Collections;

public class Eindpraatje : MonoBehaviour {

	public int einde;
	public double time = 5;


	// Use this for initialization
	void Start () {
	

				if (einde == 1) {

						GameObject uit = GameObject.Find ("WinText");
						uit.SetActive (false);

				}

				if (einde == 2) {
						GameObject uit = GameObject.Find ("LoseText");
						uit.SetActive (false);
				}
		}
	
	// Update is called once per frame
	void Update () {
	
				time -= Time.deltaTime;


				if (time < 0.0) {
			Application.LoadLevel("StartMenu");
				}
		}
}
