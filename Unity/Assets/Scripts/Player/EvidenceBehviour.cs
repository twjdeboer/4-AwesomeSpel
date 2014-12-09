using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EvidenceBehviour : MonoBehaviour {

    public string description;
    public string name;

    private GameObject canvas;
    private GameObject evidenceInterface;
    private Text descriptionText;
    private Text evidenceNameText;
    private Quaternion intRotation;
    private Vector3 intPosition;

    void OnMouseOver()
    {
        intRotation = transform.rotation;
        intPosition = transform.position;
        ActivateAndFillUI();
        InterfacePosition();
        ScaleAndRotate();
    }


    void ScaleAndRotate()
    {
        rigidbody.isKinematic = true;
        transform.rotation = Quaternion.Euler(new Vector3(transform.rotation.x, transform.rotation.y, 0));

    }

    void OnMouseExit()
    {
        DeactivateUI();
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

	// Use this for initialization
	void Start () {

        canvas = GameObject.Find("Conversation Interface");
        evidenceInterface = GameObject.Find("Evidence");
        descriptionText = GameObject.Find("Description").GetComponent<Text>();
        evidenceNameText = GameObject.Find("EvidenceName").GetComponent<Text>();
        evidenceInterface.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
