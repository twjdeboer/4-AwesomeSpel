using UnityEngine;
using System.Collections;

public class MouseCollider : MonoBehaviour {

    public bool mouseEntered = false;
    public bool mouseClicked = false;
    public bool mouseLeft = false;

    void OnMouseOver()
    {
        mouseEntered = true;
    }

    void OnMouseExit()
    {
        mouseEntered = false;
        mouseLeft = true;
    }

    void OnMouseUpAsButton()
    {
        mouseClicked = true;
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

	
	}
}
