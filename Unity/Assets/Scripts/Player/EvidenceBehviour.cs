﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class EvidenceBehviour : MonoBehaviour {

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

    /*
     * Checks if mouse over object, if so executes evidence selector.
     * */
    void IfMouseEnter()
    {
        if (CheckDistanceBetweenPlayerAndObject() && mouseEntered)
        {
            SetStartRotAndPos();
            ActivateAndFillUI();
        }
    }



    /**
     * Sets rotation and position of object if mouse is hovering over the object.
     * */
    void SetStartRotAndPos()
    {
        if(onGround)
        {
            intRotation = transform.rotation;
            intPosition = transform.position;
            transform.rotation = Quaternion.Euler(rotationIfSelected);
            transform.position = intPosition + translateIfSelected;
            rotated = true;
        }
    }

    /**
     * Scales and rotates the object when mouse is hovering over.
     * */
    void ScaleAndRotate()
    {
        if (mouseOver && rotated)
        {   
            rigidbody.isKinematic = true;
            transform.localScale = intScale * scaleFactorIfSelected;
            transform.RotateAround(transform.position, Vector3.up, rotationSpeed * Time.deltaTime);
        }
    }


    /**
     * Initializes the UI used to show evidence.
     * */
    void ActivateAndFillUI()
    {
        evidenceInterface.GetComponent<CanvasGroup>().alpha = 1;
        descriptionText.text = description;
		evidenceNameText.text = evidencename;
    }

    /**
     * Deactivates UI
     * */
    void DeactivateUI()
    {
        evidenceInterface.GetComponent<CanvasGroup>().alpha = 0;
    }

    /**
     * Check if object is on ground.
     * */
    void OnCollisionEnter(Collision other)
    {
        onGround = true;
    }

    /**
 * Check if object is on ground.
 * */
    void OnCollisionExit(Collision other)
    {
        onGround = false;
    }

    /**
     * Check if evidence is picked up.
     * */
    void IfMouseUpAsButton()
    {
        if (canBePickedUp && mouseOver && mouseClicked)
        {
            pickedUp = true;
        }

    }

    /**
     * Checks if mouse left a certain box.
     * */
    void MouseLeft()
    {

        if(mouseLeft)
       {
           collider.isTrigger = false;
           DeactivateUI();
           transform.rotation = intRotation;
           rigidbody.isKinematic = false;
           mouseEntered = false;
           transform.localScale = intScale;
           rotated = false;
           mouseLeft = false;
           gameObject.GetComponentInChildren<MouseCollider>().mouseLeft = false;
       }

    }

    /**
     * Deactivates UI and destroys evidence object and add is to the evidenceList.
     * */
    void PickUpEvidence()
    {
        if (pickedUp)
        {
            evidenceInterface.GetComponent<CanvasGroup>().alpha = 0;
            GameObject.Find("PickedUpEvidence").GetComponent<PickedUpEvidence>().activated = true;
			GameObject.Find("PickedUpEvidence").GetComponent<PickedUpEvidence>().text.text = "Picked up " + evidencename ;
            pickedUp = false;
            if (evidenceInterface.GetComponent<CanvasGroup>().alpha == 0)
            {
                GameObject.Find("EvidenceList").GetComponent<ShowEvidence>().Add(this.gameObject);
                gameObject.SetActive(false);
            }
        }
    }

    bool CheckDistanceBetweenPlayerAndObject()
    {
        return Vector3.Distance(transform.position, ResourceManager.playerPosition) < maxDistance;
    }

	// Use this for initialization
	void Start () {
        CreateMouseCollider();
        evidenceInterface = GameObject.Find("Evidence");
        descriptionText = GameObject.Find("Description").GetComponent<Text>();
        evidenceNameText = GameObject.Find("EvidenceName").GetComponent<Text>();
        evidenceInterface.GetComponent<CanvasGroup>().alpha = 0;
        intScale = transform.localScale;
	}
	
    void CheckMouseCollider()
    {
        mouseEntered = transform.GetComponentInChildren<MouseCollider>().mouseEntered;
        mouseClicked = gameObject.GetComponentInChildren<MouseCollider>().mouseClicked;
        mouseLeft = gameObject.GetComponentInChildren<MouseCollider>().mouseLeft;
        mouseOver = gameObject.GetComponentInChildren<MouseCollider>().mouseOver;
    }

    void CreateMouseCollider()
    {
        mouseCollider = Instantiate(Resources.Load("Prefabs/mouseCollider")) as GameObject;
        mouseCollider.transform.SetParent(transform, false);
    }

	// Update is called once per frame
	void Update () {
        IfMouseEnter();
        IfMouseUpAsButton();
        CheckMouseCollider();
        MouseLeft();
        ScaleAndRotate();
        PickUpEvidence();
	}
}
