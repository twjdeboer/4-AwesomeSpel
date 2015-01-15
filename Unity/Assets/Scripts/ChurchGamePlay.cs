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


		
		
	
	// Update is called once per frame
	void Update () {
	

		NPCInteraction npcinteraction = GetComponent<NPCInteraction> ();


			if (npcinteraction.textToDisplay == "schateiland"){
			Application.LoadLevel("Einde");
				}

	}
}
