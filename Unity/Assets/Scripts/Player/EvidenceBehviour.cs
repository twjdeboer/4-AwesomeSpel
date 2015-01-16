using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using System.IO;

public class EvidenceBehviour : MonoBehaviour
{

		public int evidenceID;
		public string description;
		public string evidencename;
		public Vector3 rotationIfSelected;
		public Vector3 translateIfSelected;
		public float scaleFactorIfSelected;
		public float rotationSpeed;
		public bool canBePickedUp = true;
		public float maxDistance;
		private bool mouseEntered = false;
		private bool mouseClicked = false;
		private bool mouseLeft = false;
		private bool mouseOver = false;
		private GameObject evidenceInterface;
		private GameObject mouseCollider;
		private Text descriptionText;
		private Text evidenceNameText;
		private Quaternion intRotation;
		private Vector3 intPosition;
		private Vector3 intScale;
		private bool onGround = false;
		private bool rotated = false;
		private bool pickedUp = false;
		private GameObject evidence;
		//public Transform floorcollider;


		// pick location
		Vector3 pickRandom ()
		{
		
				float[] xCord = {
						297.23f,
						316.22f,
						285.15f,
						5f,
						6f,
						1.3f,
						-2.28f,
						38.8f,
						-15.5f,
						7.21f,
						-83.61f,
						-114.6f,
						262.9f,
						253.7f,
						253.3f,
						298.0f,
						-95.06f,
						-40.55f,
						86.3f,
						46.5f,
						283.4f,
						-2.54f,
						0.53f,
						-0.64f,
						-106.5f,
						-114.8f,
						-97.7f
				};
				float[] yCord = {
						-0.02f,
						0.3f,
						0.2f,
						0.4f,
						0.4f,
						0.4f,
						-0.07f,
						0.03f,
						0.06f,
						0.19f,
						0.2f,
						0.19f,
						0.89f,
						0.09f,
						0.17f,
						0.49f,
						0.78f,
						0.72f,
						0.22f,
						0.22f,
						0.22f,
						0.43f,
						0.1f,
						0.27f,
						0.12f,
						0.12f,
						0.12f
				};
				float[] zCord = {
						-129.21f,
						-63.66f,
						-87.72f,
						7f,
						2f,
						8.5f,
						-4.14f,
						-54.14f,
						33.28f,
						-86.02f,
						-145.8f,
						82.5f,
						-183.8f,
						-198.2f,
						-164.4f,
						-131.3f,
						-216.2f,
						-3.89f,
						86f,
						153.7f,
						83.5f,
						-0.85f,
						2.761f,
						5.47f,
						-211.5f,
						-212.4f,
						-223.8f
				};
		
				System.Random random = new System.Random ();
				int i = evidenceID-1;	

				int index = (random.Next (3 * i, 2 * (i + 1) + i));

				float x = (float)xCord.GetValue (index);
				float y = (float)yCord.GetValue (index);
				float z = (float)zCord.GetValue (index);
				
				Vector3 location = new Vector3 (x, y, z);
				
				return location;
		}


		void placeEvidence ()
		{
				if ((bool)SceneManager.ReadItemList ("cloud.save").GetValue (evidenceID -1) == false) {
						transform.position = pickRandom ();
				} else
						gameObject.SetActive (false);
		}
	
		// Use this for initialization
		void Start ()
		{
				CreateMouseCollider ();
				evidenceInterface = GameObject.Find ("Evidence");
				descriptionText = GameObject.Find ("Description").GetComponent<Text> ();
				evidenceNameText = GameObject.Find ("EvidenceName").GetComponent<Text> ();
				evidenceInterface.GetComponent<CanvasGroup> ().alpha = 0;
				intScale = transform.localScale;
				placeEvidence ();
		}
	
		/*
     * Checks if mouse over object, if so executes evidence selector.
     * */
		void IfMouseEnter ()
		{
				if (CheckDistanceBetweenPlayerAndObject () && mouseEntered) {
						SetStartRotAndPos ();
						ActivateAndFillUI ();
				}
		}



		/**
     * Sets rotation and position of object if mouse is hovering over the object.
     * */
		void SetStartRotAndPos ()
		{
				if (onGround) {
						intRotation = transform.rotation;
						intPosition = transform.position;
						transform.rotation = Quaternion.Euler (rotationIfSelected);
						transform.position = intPosition + translateIfSelected;
						rotated = true;
				}
		}

		/**
     * Scales and rotates the object when mouse is hovering over.
     * */
		void ScaleAndRotate ()
		{
				if (mouseOver && rotated) {   
						rigidbody.isKinematic = true;
						transform.localScale = intScale * scaleFactorIfSelected;
						transform.RotateAround (transform.position, Vector3.up, rotationSpeed * Time.deltaTime);
				}
		}


		/**
     * Initializes the UI used to show evidence.
     * */
		void ActivateAndFillUI ()
		{
				evidenceInterface.GetComponent<CanvasGroup> ().alpha = 1;
				descriptionText.text = description;
				evidenceNameText.text = evidencename;
		}

		/**
     * Deactivates UI
     * */
		void DeactivateUI ()
		{
				evidenceInterface.GetComponent<CanvasGroup> ().alpha = 0;
		}

		/**
     * Check if object is on ground.
     * */
		void OnCollisionEnter (Collision other)
		{
				onGround = true;
		}

		/**
 * Check if object is on ground.
 * */
		void OnCollisionExit (Collision other)
		{
				onGround = false;
		}

		/**
     * Check if evidence is picked up.
     * */
		void IfMouseUpAsButton ()
		{
				if (canBePickedUp && mouseOver && mouseClicked) {
						pickedUp = true;
						//SendGetRequest();

				}

		}

		void SendGetRequest ()
		{
				string filename = "cloud.save";
				string[] content = File.ReadAllLines (filename);

				string url = "http://drproject.twi.tudelft.nl:8084/pickupitem?userId=" + content [0] + "&itemId=" + evidenceID;
				WWW www = new WWW (url);
        
		}


		/**
     * Checks if mouse left a certain box.
     * */
		void MouseLeft ()
		{

				if (mouseLeft) {
						collider.isTrigger = false;
						DeactivateUI ();
						transform.rotation = intRotation;
						rigidbody.isKinematic = false;
						mouseEntered = false;
						transform.localScale = intScale;
						rotated = false;
						mouseLeft = false;
						gameObject.GetComponentInChildren<MouseCollider> ().mouseLeft = false;
				}

		}

		/**
     * Deactivates UI and destroys evidence object and add is to the evidenceList.
     * */
		void PickUpEvidence ()
		{
				if (pickedUp) {
						evidenceInterface.GetComponent<CanvasGroup> ().alpha = 0;
						GameObject.Find ("PickedUpEvidence").GetComponent<PickedUpEvidence> ().activated = true;
						GameObject.Find ("PickedUpEvidence").GetComponent<PickedUpEvidence> ().text.text = "Picked up " + evidencename;
						pickedUp = false;
						updateSave ();
						updateServer ();
						if (evidenceInterface.GetComponent<CanvasGroup> ().alpha == 0) {
								GameObject.Find ("EvidenceList").GetComponent<ShowEvidence> ().Add (this.gameObject);
								gameObject.SetActive (false);
						}
				}

		}

		void updateSave ()
		{
				string[] content = new string[16];

				string filename = "cloud.save";

				if (File.Exists (filename)) {
						content = File.ReadAllLines (filename);
				} else {
						Debug.Log ("No Save File found");
				}

				content [evidenceID + 3] = "True";

				StreamWriter sr = File.CreateText (filename);
				for (int i = 0; i < content.Length; i++) {
						sr.WriteLine (content [i]);
				}
				sr.Close ();
		}

		void updateServer ()
		{
		string[] content = new string[15];
		
		string filename = "cloud.save";
		
		if (File.Exists (filename)) {
			content = File.ReadAllLines (filename);
		} else {
			Debug.Log ("No Save File found");
		}
		string url = "http://drproject.twi.tudelft.nl:8084/pickupitem?userId=" + content [0] + "&itemId=" + evidenceID;
		WWW www = new WWW (url);
		StartCoroutine (GETAddEvidence (www));
		}

	IEnumerator GETAddEvidence(WWW www){
		yield return www;

		if (www.error == null) {
			Debug.Log ("SUCCESS");
				} else {
			Debug.Log ("WWW Error:" + www.error	);
		}
	}

		bool CheckDistanceBetweenPlayerAndObject ()
		{
				return Vector3.Distance (transform.position, ResourceManager.playerPosition) < maxDistance;
		}

		void CheckMouseCollider ()
		{
				mouseEntered = transform.GetComponentInChildren<MouseCollider> ().mouseEntered;
				mouseClicked = gameObject.GetComponentInChildren<MouseCollider> ().mouseClicked;
				mouseLeft = gameObject.GetComponentInChildren<MouseCollider> ().mouseLeft;
				mouseOver = gameObject.GetComponentInChildren<MouseCollider> ().mouseOver;
		}

		void CreateMouseCollider ()
		{
				mouseCollider = Instantiate (Resources.Load ("Prefabs/mouseCollider")) as GameObject;
				mouseCollider.transform.SetParent (transform, false);
		}

		// Update is called once per frame
		void Update ()
		{
				IfMouseEnter ();
				IfMouseUpAsButton ();
				CheckMouseCollider ();
				MouseLeft ();
				ScaleAndRotate ();
				PickUpEvidence ();
		}
}
