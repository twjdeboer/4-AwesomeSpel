using UnityEngine;
using System.Collections;

public class Worldactive : MonoBehaviour {
	
	void Start () {
		ResourceManager.World.SetActive (true);
	}

}
