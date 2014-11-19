using UnityEngine;
using System.Collections;

public class Test_proceduralhouse : MonoBehaviour {
	GameObject house;
	GameObject roof;
	// Use this for initialization
	void Start () {
		house = GameObject.FindGameObjectWithTag ("Building");
		Vector3 vc = house.transform.localScale;
		int x = (int)vc.x + Random.Range (-3,3);
		int y = (int)vc.y + Random.Range (5, 20);
		int z = (int)vc.z + Random.Range (-3,3);
		print (x+ " " + y +" "+ z);

		house.transform.localScale = new Vector3(x,y,z);
		roof = MonoBehaviour.Instantiate(Resources.Load ("Prefabs/Roof", typeof(GameObject))) as GameObject;

	}
	
	// Update is called once per frame
	void Update () {
	}
}
