using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Text.RegularExpressions;
using System.IO;
using SimpleJSON;

public class ChurchGamePlay : MonoBehaviour {

	 

	// Use this for initialization
	void Start () {
	

		string filename = "cloud.save";
		string[] content = File.ReadAllLines (filename);
		NPCInteraction npcinteraction = GetComponent<NPCInteraction> ();


		// set the right conversation with the priest
		if (content [13] == "False") 
		{
			npcinteraction.fileName = "Priest.xml";
		} 
		else 
		{
			if (content [14] == "False")
			{
				npcinteraction.fileName = "Priest1.xml";
			} 
			else 
			{
				npcinteraction.fileName = "Priest2.xml";
			}
		}
	}


		
		
	
	// Check if the conversation is ended
	void Update () {
	

		NPCInteraction npcinteraction = GetComponent<NPCInteraction> ();


			if (npcinteraction.textToDisplay == "Oh no"){

				string filename = "cloud.save";
				string[] content = File.ReadAllLines (filename);
				// reset the evidence on the server
				string reseturl = "http://drproject.twi.tudelft.nl:8084/resetSave?userId=" + content [0];
				WWW resetget = new WWW (reseturl);
				StartCoroutine (GETResetEvidence (resetget));


				Application.LoadLevel("Einde");
				}

	}

	// method for sending a get request
	IEnumerator GETResetEvidence(WWW www){
		yield return www;

		if (www.error == null) {
			Debug.Log ("SUCCESS");
				} else {
			Debug.Log ("WWW Error:" + www.error	);
		}
	}
}
