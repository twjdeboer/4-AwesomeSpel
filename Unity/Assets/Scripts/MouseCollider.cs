using UnityEngine;
using System.Collections;

public class MouseCollider : MonoBehaviour {

    public bool mouseOver = false;
    public bool mouseEntered = false;
    public bool mouseClicked = false;
    public bool mouseLeft = false;
    private int index = 0;

    void CancelMouseEnter()
    {
        if(mouseEntered && index < 2)
        {
            index++;
        }
        else
        {
            index = 0;
            mouseEntered = false;
        }
    }

    void OnMouseEnter()
    {
        mouseEntered = true;
        mouseLeft = false;
    }

    void OnMouseOver()
    {
        mouseOver = true;
    }

    void OnMouseExit()
    {
        mouseEntered = false;
        mouseOver = false;
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
        transform.rotation = Quaternion.identity;
        CancelMouseEnter();
	}
}
