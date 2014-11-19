using UnityEngine;
using System.Collections;

public class CamaraBehaviour : MonoBehaviour {

    //Attributes

    private Vector3 offset;
    private GameObject prefab;

    //Methods

    /**
     * Makes the camera follow the player.
     * */
    void FollowPlayer()
    {
        transform.position = ResourceManager.playerPosition + this.offset;
    }
	
    /**
     * Creates object which follow the player and makes transparent buildings.
     * */
    void CreateViewLine(GameObject prefab)
    {
        GameObject viewLine = Instantiate(prefab, this.offset * 0.5f, Quaternion.Euler(0, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z)) as GameObject;
        viewLine.transform.parent = transform;
        viewLine.transform.localScale = new Vector3(5, this.offset.y,this.offset.z);
    
    }

    //Actions

    void Start () {

        this.offset = transform.position - ResourceManager.playerPosition;
        ResourceManager.cam = transform;
        prefab = Resources.Load("Prefabs/viewLine") as GameObject;
        CreateViewLine(prefab);
	}

	void FixedUpdate () {

        FollowPlayer();
	
	}
}
