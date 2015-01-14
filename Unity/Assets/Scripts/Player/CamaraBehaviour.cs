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
        if(!setOffset)
        transform.position = ResourceManager.playerPosition + this.offset;
    }

    /**
 * Sets Opacity for a material.
 * */
    void SetAlpha(GameObject go, float alpha)
    {
        Material[] materialList = go.renderer.materials;
        for (int i = 0; i < materialList.Length; i++)
        {
            Color color = materialList[i].color;
            color.a = alpha;
            materialList[i].color = color;
        }
    }

    void CheckRay()
    {   
        if(Physics.Linecast(transform.position, ResourceManager.playerPosition, out rayinfo))
        {
            Collider other = rayinfo.collider;
            if (other.gameObject.tag == "Building")
            {
                SetAlpha(other.gameObject, 0.5f);
                reset = other;
            }
            else if (other.gameObject.tag == "Building2")
            {
                reset = other;
                SetAlpha(other.gameObject, 0.1f);
            }
            else if (other.gameObject.tag.Contains("Transparent"))
            {
                SetAlpha(other.gameObject, 0.5f);
                reset = other;
            }
            else
            {
                if (reset != null)
                {
                    other = reset;
                    if (other.gameObject.tag == "Building")
                        SetAlpha(other.gameObject, 1);
                    if (other.gameObject.tag == "Building")
                        SetAlpha(other.gameObject, 1);
                    if (other.gameObject.tag.Contains("Transparent"))
                        SetAlpha(other.gameObject, 1);
                }
            }
        }


    }


    //Actions

    void Start () {

        reset = null;
        ResourceManager.cam = transform;
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
        SetOffset();
	}
}
