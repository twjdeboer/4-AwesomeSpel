using UnityEngine;
using System.Collections;

/*
 * Create 2D collider around evidence --> rotating does not affect collider and 2D collider do not interact with enviroment (trigger)
 * */
public class MouseCollider : MonoBehaviour {

    public bool mouseOver = false;
    public bool mouseEntered = false;
    public bool mouseClicked = false;
    public bool mouseLeft = false;
    private int index = 0;

    /*
     * Makes sure mouseEntered is only true in one frame. So it become different from mouseOver
     * */
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

    /*
     * Indicates mouse enters collider
     * */
    void OnMouseEnter()
    {
        mouseEntered = true;
        mouseLeft = false;
    }

    /*
     * Indicates mouse is hovering over collider
     * */
    void OnMouseOver()
    {
        mouseOver = true;
    }

    /*
     * Indicates mouse left object
     * */
    void OnMouseExit()
    {
        mouseEntered = false;
        mouseOver = false;
        mouseLeft = true;
    }

    /*
     * Indicates mousebutton is clicked.
     * */
    void OnMouseUpAsButton()
    {
        mouseClicked = true;
    }
	
	// Update is called once per frame
	void Update () {
        transform.rotation = Quaternion.identity; //Does not allow collider to rotate with parent
        CancelMouseEnter();
	}
}
