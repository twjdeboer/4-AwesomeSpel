using UnityEngine;
using System.Collections;

public class landpost : MonoBehaviour {

	public Transform streetlight;
	public GameObject HouseContainer;
	private GameObject latern;
	private GameObject Lights;
	// Use this for initialization
	void Start () {
		Lights = new GameObject ("Lights");
		Lights.transform.parent = HouseContainer.transform;
		//horizontal landposts
		for (int x = 4; x < 236; x = x + 20){
			latern = Instantiate(streetlight, new Vector3 (x, 0, -199), Quaternion.Euler(270,0,0)) as GameObject;
			
			latern = Instantiate(streetlight, new Vector3 (x, 0, -191), Quaternion.Euler(270,0,0))as GameObject;
			
		}

		for (int x = -176; x < 236; x = x + 20){
			latern = Instantiate(streetlight, new Vector3 (x, 0, -146), Quaternion.Euler(270,0,0)) as GameObject;
			
			latern = Instantiate(streetlight, new Vector3 (x, 0, -154), Quaternion.Euler(270,0,0)) as GameObject;
			
		}

		for (int x = -256; x < 236; x = x + 20){
			latern = Instantiate(streetlight, new Vector3 (x, 0, -94), Quaternion.Euler(270,0,0)) as GameObject;
			
			latern = Instantiate(streetlight, new Vector3 (x, 0, -86), Quaternion.Euler(270,0,0)) as GameObject;
			
		}

		for (int x = -256; x < -66; x = x + 20){
			latern = Instantiate(streetlight, new Vector3 (x, 0, -4), Quaternion.Euler(270,0,0)) as GameObject;
			
			latern = Instantiate(streetlight, new Vector3 (x, 0, 4), Quaternion.Euler(270,0,0)) as GameObject;
			
		}

		for (int x = 190; x < 324; x = x + 20){
			latern = Instantiate(streetlight, new Vector3 (x, 0, -4), Quaternion.Euler(270,0,0)) as GameObject;
			
			latern = Instantiate(streetlight, new Vector3 (x, 0, 4), Quaternion.Euler(270,0,0)) as GameObject;
			
		}

		for (int x = -176; x < 176; x = x + 20){
			latern = Instantiate(streetlight, new Vector3 (x, 0, 86), Quaternion.Euler(270,0,0)) as GameObject;
			
			latern = Instantiate(streetlight, new Vector3 (x, 0, 94), Quaternion.Euler(270,0,0)) as GameObject;
			
		}

		for (int x = -216; x < 281; x = x + 20){
			latern = Instantiate(streetlight, new Vector3 (x, 0, 146), Quaternion.Euler(270,0,0)) as GameObject;
			
			latern = Instantiate(streetlight, new Vector3 (x, 0, 154), Quaternion.Euler(270,0,0)) as GameObject;
			
		}

		//vertical landposts

		latern = Instantiate(streetlight, new Vector3 (-4, 0, -172), Quaternion.Euler(270,0,0)) as GameObject;
		
		latern = Instantiate(streetlight, new Vector3 (4, 0, -172), Quaternion.Euler(270,0,0)) as GameObject;
		
			
		latern = Instantiate(streetlight, new Vector3 (236, 0, -172), Quaternion.Euler(270,0,0)) as GameObject;
		
		latern = Instantiate(streetlight, new Vector3 (244, 0, -172), Quaternion.Euler(270,0,0)) as GameObject;
		

		for (int z = 24; z < 130; z = z + 20){
			latern = Instantiate(streetlight, new Vector3 (-229, 0, z), Quaternion.Euler(270,0,0)) as GameObject;
			
			latern = Instantiate(streetlight, new Vector3 (-221, 0, z), Quaternion.Euler(270,0,0)) as GameObject;
			

			latern = Instantiate(streetlight, new Vector3 (236, 0, z), Quaternion.Euler(270,0,0)) as GameObject;
			
			latern = Instantiate(streetlight, new Vector3 (244, 0, z), Quaternion.Euler(270,0,0)) as GameObject;
			

			latern = Instantiate(streetlight, new Vector3 (281, 0, z), Quaternion.Euler(270,0,0)) as GameObject;
			
			latern = Instantiate(streetlight, new Vector3 (289, 0, z), Quaternion.Euler(270,0,0)) as GameObject;
			
		}

		for (int z = -132; z < -100; z = z + 20){
			latern = Instantiate(streetlight, new Vector3 (-184, 0, z), Quaternion.Euler(270,0,0)) as GameObject;
			
			latern = Instantiate(streetlight, new Vector3 (-176, 0, z), Quaternion.Euler(270,0,0)) as GameObject;
			

			latern = Instantiate(streetlight, new Vector3 (-4, 0, z), Quaternion.Euler(270,0,0)) as GameObject;
			
			latern = Instantiate(streetlight, new Vector3 (4, 0, z), Quaternion.Euler(270,0,0)) as GameObject;
			

			latern = Instantiate(streetlight, new Vector3 (86, 0, z), Quaternion.Euler(270,0,0)) as GameObject;
			
			latern = Instantiate(streetlight, new Vector3 (94, 0, z), Quaternion.Euler(270,0,0)) as GameObject;
			

			latern = Instantiate(streetlight, new Vector3 (236, 0, z), Quaternion.Euler(270,0,0)) as GameObject;
			
			latern = Instantiate(streetlight, new Vector3 (244, 0, z), Quaternion.Euler(270,0,0)) as GameObject;
			
		}

		for (int z = -65; z < -20; z = z + 20){
			latern = Instantiate(streetlight, new Vector3 (-184, 0, z), Quaternion.Euler(270,0,0)) as GameObject;
			
			latern = Instantiate(streetlight, new Vector3 (-176, 0, z), Quaternion.Euler(270,0,0)) as GameObject;
			
			
			latern = Instantiate(streetlight, new Vector3 (-124, 0, z), Quaternion.Euler(270,0,0)) as GameObject;
			
			latern = Instantiate(streetlight, new Vector3 (-116, 0, z), Quaternion.Euler(270,0,0)) as GameObject;
			
			
			latern = Instantiate(streetlight, new Vector3 (176, 0, z), Quaternion.Euler(270,0,0)) as GameObject;
			
			latern = Instantiate(streetlight, new Vector3 (184, 0, z), Quaternion.Euler(270,0,0)) as GameObject;
			
			
			latern = Instantiate(streetlight, new Vector3 (236, 0, z), Quaternion.Euler(270,0,0)) as GameObject;
			
			latern = Instantiate(streetlight, new Vector3 (244, 0, z), Quaternion.Euler(270,0,0)) as GameObject;
			
		}

		for (int z = 25; z < 70; z = z + 20){
			latern = Instantiate(streetlight, new Vector3 (-184, 0, z), Quaternion.Euler(270,0,0)) as GameObject;
			
			latern = Instantiate(streetlight, new Vector3 (-176, 0, z), Quaternion.Euler(270,0,0)) as GameObject;
			
			
			latern = Instantiate(streetlight, new Vector3 (-124, 0, z), Quaternion.Euler(270,0,0)) as GameObject;
			
			latern = Instantiate(streetlight, new Vector3 (-116, 0, z), Quaternion.Euler(270,0,0)) as GameObject;
			
			
			latern = Instantiate(streetlight, new Vector3 (176, 0, z), Quaternion.Euler(270,0,0)) as GameObject;
			
			latern = Instantiate(streetlight, new Vector3 (184, 0, z), Quaternion.Euler(270,0,0)) as GameObject;
			
		}

		for (int z = 110; z < 140; z = z + 20){
			latern = Instantiate(streetlight, new Vector3 (-184, 0, z), Quaternion.Euler(270,0,0)) as GameObject;
			latern = Instantiate(streetlight, new Vector3 (-176, 0, z), Quaternion.Euler(270,0,0)) as GameObject;
			
			latern = Instantiate(streetlight, new Vector3 (-4, 0, z), Quaternion.Euler(270,0,0)) as GameObject;
			latern = Instantiate(streetlight, new Vector3 (4, 0, z), Quaternion.Euler(270,0,0)) as GameObject;
			
			latern = Instantiate(streetlight, new Vector3 (146, 0, z), Quaternion.Euler(270,0,0)) as GameObject;
			latern = Instantiate(streetlight, new Vector3 (154, 0, z), Quaternion.Euler(270,0,0)) as GameObject;
			latern = GameObject.Find ("/Street Light(Clone)");
		}

	}
	void Update(){
		for(int i=0;i<10;i++){
			if (latern != null) {
				latern.transform.parent = Lights.transform;
				latern = GameObject.Find ("/Street Light(Clone)");
			}
		}


	}



}
