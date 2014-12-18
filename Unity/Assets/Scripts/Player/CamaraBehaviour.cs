using UnityEngine;
using System.Collections;

public class CamaraBehaviour : MonoBehaviour {

    //Attributes

    private Vector3 offset;
    private GameObject prefab;
    public RaycastHit rayinfo;
    private Ray ray;
    private Collider reset;
    private bool setOffset = true;

    //Methods

    /**
     * Makes the camera follow the player.
     * */
    void FollowPlayer()
    {
        transform.position = ResourceManager.playerPosition + this.offset;
    }


    void CheckRay()
    {   

        if(Physics.Linecast(transform.position, ResourceManager.playerPosition, out rayinfo))
        {
            Collider other = rayinfo.collider;
            if (other.gameObject.tag == "Building")
            {
                Methods.SetAlpha(other.gameObject, 0.5f);
                reset = other;
            }
            else if (other.gameObject.tag == "Building2")
            {
                reset = other;
                Methods.SetAlpha(other.gameObject, 0.1f);
            }
            else if (other.gameObject.tag.Contains("Transparent"))
            {
                Methods.SetAlpha(other.gameObject, 0.5f);
                reset = other;
            }
            else
            {
                if (reset != null)
                {
                    other = reset;
                    if (other.gameObject.tag == "Building")
                        Methods.SetAlpha(other.gameObject, 1);
                    if (other.gameObject.tag == "Building")
                        Methods.SetAlpha(other.gameObject, 1);
                    if (other.gameObject.tag.Contains("Transparent"))
                        Methods.SetAlpha(other.gameObject, 1);
                }
            }
        }


    }


    //Actions

    void Start () {

        reset = null;
        ResourceManager.cam = transform;
        prefab = Resources.Load("Prefabs/viewLine") as GameObject;

	}

    void SetOffset()
    {
        if (setOffset)
        {
            this.offset = transform.position - ResourceManager.playerPosition;
            setOffset = false;
        }
    }


	void FixedUpdate () {
        CheckRay();
        FollowPlayer();
        Debug.Log(offset);
        SetOffset();
	}
}
