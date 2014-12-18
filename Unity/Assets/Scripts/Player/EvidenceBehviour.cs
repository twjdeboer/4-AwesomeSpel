using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class EvidenceBehviour : MonoBehaviour {

    public string description;
    public string name;
    public Vector3 rotationIfSelected;
    public Vector3 translateIfSelected;
    public float scaleFactorIfSelected;
    public float rotationSpeed;
    public bool canBePickedUp = true;
    public float maxDistance;

    private GameObject evidenceInterface;
    private Text descriptionText;
    private Text evidenceNameText;
    private Quaternion intRotation;
    private Vector3 intPosition;
    private Vector3 intScale;
    private bool mouseEntered = false;
    private bool onGround = false;
    private bool rotated = false;
    private bool pickedUp = false;

    /*
     * Checks if mouse over object, if so executes evidence selector.
     * */
    void OnMouseOver()
    {
        if (CheckDistanceBetweenPlayerAndObject())
        {
            SetStartRotAndPos();
            mouseEntered = true;
            ActivateAndFillUI();
        }
    }

    /**
     * Sets rotation and position of object if mouse is hovering over the object.
     * */
    void SetStartRotAndPos()
    {
        if(onGround && !mouseEntered)
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
        if (mouseEntered && rotated)
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
        evidenceNameText.text = name;
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
    void OnMouseUpAsButton()
    {
        if (canBePickedUp && mouseEntered)
         pickedUp = true;    

    }

    /**
     * Checks if mouse left a certain box.
     * */
    void MouseLeft()
    {
        float minX = ResourceManager.cam.camera.WorldToScreenPoint(renderer.bounds.min).x * 0.9f;
        float maxX = ResourceManager.cam.camera.WorldToScreenPoint(renderer.bounds.max).x * 1.1f;
        float minY = ResourceManager.cam.camera.WorldToScreenPoint(renderer.bounds.min).y * 0.9f;
        float maxY = ResourceManager.cam.camera.WorldToScreenPoint(renderer.bounds.max).y * 1.1f;

       if(( Input.mousePosition.x < minX ||
           Input.mousePosition.x > maxX ||
           Input.mousePosition.y < minY ||
           Input.mousePosition.y > maxY) && mouseEntered)
       {
           collider.isTrigger = false;
           DeactivateUI();
           transform.rotation = intRotation;
           rigidbody.isKinematic = false;
           mouseEntered = false;
           transform.localScale = intScale;
           rotated = false;
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
            GameObject.Find("PickedUpEvidence").GetComponent<PickedUpEvidence>().text.text = "Picked up " + name ;
            if (evidenceInterface.GetComponent<CanvasGroup>().alpha == 0)
            {
                ResourceManager.evidenceList.Add(this.gameObject);
                Destroy(gameObject);
            }
        }
    }

    bool CheckDistanceBetweenPlayerAndObject()
    {
        return Vector3.Distance(transform.position, ResourceManager.playerPosition) < maxDistance;
    }

	// Use this for initialization
	void Start () {

        evidenceInterface = GameObject.Find("Evidence");
        descriptionText = GameObject.Find("Description").GetComponent<Text>();
        evidenceNameText = GameObject.Find("EvidenceName").GetComponent<Text>();
        evidenceInterface.GetComponent<CanvasGroup>().alpha = 0;
        intScale = transform.localScale;
	}
	


	// Update is called once per frame
	void Update () {

        MouseLeft();
        ScaleAndRotate();
        PickUpEvidence();
	}
}
