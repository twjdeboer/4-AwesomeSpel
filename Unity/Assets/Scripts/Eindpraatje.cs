using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections;
using System.Text.RegularExpressions;
using System.IO;

public class Eindpraatje : MonoBehaviour {

	public int einde;
	private double time = 5;


	// Use this for initialization
	void Start () {
	
		SetEinde ();

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


	void SetEinde(){

		string filename = "cloud.save";
		bool[] items = new bool[10];

			
		string[] content = File.ReadAllLines (filename);
				
		for (int i = 0; i<10; i++) {
			items [i] = bool.Parse( content[i+4]);
		}
				
				
		

		if (items [9] == false) {
				einde = 1;
		} else {
				einde = 2;
		}
			
	

	}
}

