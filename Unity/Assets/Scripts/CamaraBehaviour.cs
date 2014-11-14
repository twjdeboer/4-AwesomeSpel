using UnityEngine;
using System.Collections;

public class CamaraBehaviour : MonoBehaviour {

    private Vector3 offset;


	// Use this for initialization
	void Start () {

        this.offset = transform.position;
	}

    void FollowPlayer()
    {
        transform.position = ResourceManager.playerPosition + this.offset;
    }
	
	// Update is called once per frame
	void FixedUpdate () {

        FollowPlayer();
	
	}
}
