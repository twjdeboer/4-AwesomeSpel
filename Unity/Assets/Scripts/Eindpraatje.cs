using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections;
using System.Text.RegularExpressions;
using System.IO;

public class Eindpraatje : MonoBehaviour {

	public int einde;
	private double time = 10;


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


				if (time < 0.0 || Input.GetKeyUp(KeyCode.Escape)) {
			Application.LoadLevel("StartMenu");
				}
		          
		}


	void SetEinde(){

		string filename = "cloud.save";
		

			
		string[] content = File.ReadAllLines (filename);
				
		if (content[14] == "True")
		{
			einde = 2;
		}
		else 
		{
			einde = 1;
		}
			
	

	}


}

