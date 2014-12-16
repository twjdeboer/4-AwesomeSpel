using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PickedUpEvidence : MonoBehaviour {

    public Text text;
    public bool activated;
    public float timeDisplayed;
    private float timer = 0;


	// Use this for initialization
	void Start () {

        text = GameObject.Find("PickedUpEvidence").GetComponent<Text>();
	}
	
    void Behaviour()
    {
        if (activated && timer < timeDisplayed)
        {
            timer += Time.deltaTime;
            gameObject.GetComponent<CanvasGroup>().alpha = 1;           
        }
        else
        {
            gameObject.GetComponent<CanvasGroup>().alpha = 0;
            timer = 0;
            activated = false;
        }
    }



	// Update is called once per frame
	void Update () {

        Behaviour();
        
	
	}
}
