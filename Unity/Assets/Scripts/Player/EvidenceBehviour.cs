using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class EvidenceBehviour : MonoBehaviour {

    public string description;
    public string name;

    private GameObject canvas;
    private GameObject evidenceInterface;
    private Text descriptionText;
    private Text evidenceNameText;
    private Quaternion intRotation;
    private Vector3 intPosition;
    private Vector3 intScale;
    private bool mouseEntered = false;
    private bool onGround = false;
    private bool rotated = false;

    void OnMouseOver()
    {
    
        SetStartRotAndPos();
        mouseEntered = true;
        ActivateAndFillUI();
        InterfacePosition();
    }

    void SetStartRotAndPos()
    {
        if(onGround && !mouseEntered)
        {
            intRotation = transform.rotation;
            intPosition = transform.position;
            transform.rotation = Quaternion.Euler(new Vector3(270, transform.rotation.y, 0));
            transform.position += new Vector3(0, 1, 0);
            rotated = true;
        }
    }

    void ScaleAndRotate()
    {
        if (mouseEntered && rotated)
        {   
            rigidbody.isKinematic = true;
            transform.localScale = intScale * 1.2f;
            transform.RotateAround(transform.position, Vector3.up, 20 * Time.deltaTime);
        }
    }

    void OnMouseExit()
    {
        collider.isTrigger = false;
        DeactivateUI();
        transform.rotation = intRotation;
        rigidbody.isKinematic = false;
        mouseEntered = false;
        transform.localScale = intScale;
        rotated = false;
    }

    void InterfacePosition()
    {
        evidenceInterface.GetComponent<RectTransform>().position = Input.mousePosition + new Vector3(0,45,0);
    }

    void ActivateAndFillUI()
    {
        canvas.SetActive(true);
        evidenceInterface.SetActive(true);
        descriptionText.text = description;
        evidenceNameText.text = name;
    }

    void DeactivateUI()
    {
        canvas.SetActive(false);
        evidenceInterface.SetActive(false);
    }

    void OnCollisionEnter(Collision other)
    {
        onGround = true;
    }

    void OnCollisionExit(Collision other)
    {
        onGround = false;
    }

    void OnMouseUpAsButton()
    {
        ResourceManager.evidenceList.Add(this.gameObject);
        Destroy(gameObject);
    }


	// Use this for initialization
	void Start () {

        canvas = GameObject.Find("Conversation Interface");
        evidenceInterface = GameObject.Find("Evidence");
        descriptionText = GameObject.Find("Description").GetComponent<Text>();
        evidenceNameText = GameObject.Find("EvidenceName").GetComponent<Text>();
        evidenceInterface.SetActive(false);
        intScale = transform.localScale;
	}
	
	// Update is called once per frame
	void Update () {

        ScaleAndRotate();
	}
}
