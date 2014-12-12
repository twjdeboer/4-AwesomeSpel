using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class EvidenceBehviour : MonoBehaviour {

    public string description;
    public string name;

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
    }

    void SetStartRotAndPos()
    {
        if(onGround && !mouseEntered)
        {
            intRotation = transform.rotation;
            intPosition = transform.position;
            transform.rotation = Quaternion.Euler(new Vector3(270, transform.rotation.y, 0));
            transform.position = intPosition + new Vector3(0, 1, 0);
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



    void ActivateAndFillUI()
    {
        evidenceInterface.SetActive(true);
        descriptionText.text = description;
        evidenceNameText.text = name;
    }

    void DeactivateUI()
    {
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
        evidenceInterface.SetActive(false);
    }

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



	// Use this for initialization
	void Start () {

        evidenceInterface = GameObject.Find("Evidence");
        descriptionText = GameObject.Find("Description").GetComponent<Text>();
        evidenceNameText = GameObject.Find("EvidenceName").GetComponent<Text>();
        evidenceInterface.SetActive(false);
        intScale = transform.localScale;
	}
	
	// Update is called once per frame
	void Update () {

        MouseLeft();
        ScaleAndRotate();
	}
}
