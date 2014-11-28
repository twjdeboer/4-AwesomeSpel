using UnityEngine;
using System.Collections;

public class Test_proceduralhouse : MonoBehaviour {
	public GameObject world;
	private ProceduralHouse[] newHouse;
	private GameObject[] Houses;
	void Start() {
		//GameObject House = GameObject.FindGameObjectWithTag ("Prebuilding");
		//ProceduralHouse pr = new ProceduralHouse (House);
		//pr.randomHouse ();
		
		Houses = GameObject.FindGameObjectsWithTag ("Prebuilding");
		newHouse = new ProceduralHouse[Houses.Length];
		
		for (int i=0; i < Houses.Length; i++) {
			newHouse[i]= new ProceduralHouse(Houses[i],false," ");
		}
		for (int i=0; i < Houses.Length; i++) {
			newHouse[i].BuildHouse();
			//newHouse[i].BuildrandomHouse();
			newHouse[i].empty.transform.parent =world.transform;
		}		
		
	}
	
}
