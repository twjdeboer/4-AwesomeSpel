using UnityEngine;
using System.Collections;

public class landpost : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Object instance = Instantiate(Resources.Load("prefabs/street light", typeof(Object)));
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
