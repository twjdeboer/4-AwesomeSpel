using UnityEngine;
using System.Collections;

public class BuildingFader : MonoBehaviour {

	
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Building")
           Methods.SetAlpha(other.gameObject, 0.5f);
    }

    void OnTriggerExit(Collider other)
    {

        if (other.gameObject.tag == "Building")
            Methods.SetAlpha(other.gameObject, 1);
    }
    
    // Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
