using UnityEngine;
using System.Collections;

public class landpost : MonoBehaviour {

	public Transform light;

	// Use this for initialization
	void Start () {
		for (int x = 4; x < 236; x = x + 20){
			Instantiate(light, new Vector3 (x, 0, -199), Quaternion.Euler(270,0,0));
			Instantiate(light, new Vector3 (x, 0, -191), Quaternion.Euler(270,0,0));
		}

		for (int x = -176; x < 236; x = x + 20){
			Instantiate(light, new Vector3 (x, 0, -146), Quaternion.Euler(270,0,0));
			Instantiate(light, new Vector3 (x, 0, -154), Quaternion.Euler(270,0,0));
		}

		for (int x = -256; x < 236; x = x + 20){
			Instantiate(light, new Vector3 (x, 0, -94), Quaternion.Euler(270,0,0));
			Instantiate(light, new Vector3 (x, 0, -86), Quaternion.Euler(270,0,0));
		}

		for (int x = -256; x < -66; x = x + 20){
			Instantiate(light, new Vector3 (x, 0, -4), Quaternion.Euler(270,0,0));
			Instantiate(light, new Vector3 (x, 0, 4), Quaternion.Euler(270,0,0));
		}

		for (int x = 190; x < 324; x = x + 20){
			Instantiate(light, new Vector3 (x, 0, -4), Quaternion.Euler(270,0,0));
			Instantiate(light, new Vector3 (x, 0, 4), Quaternion.Euler(270,0,0));
		}

		for (int x = -176; x < 176; x = x + 20){
			Instantiate(light, new Vector3 (x, 0, 86), Quaternion.Euler(270,0,0));
			Instantiate(light, new Vector3 (x, 0, 94), Quaternion.Euler(270,0,0));
		}

		for (int x = -216; x < 281; x = x + 20){
			Instantiate(light, new Vector3 (x, 0, 146), Quaternion.Euler(270,0,0));
			Instantiate(light, new Vector3 (x, 0, 154), Quaternion.Euler(270,0,0));
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
