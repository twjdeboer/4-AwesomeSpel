using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections;
using System.Text.RegularExpressions;
using System.IO;

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
			Application.LoadLevel("Einde");
				}

	}
}
