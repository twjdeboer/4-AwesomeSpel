using UnityEngine;
using System.Collections;

public class ProceduralHouse{

	private GameObject roof;
	//private static GameObject prefab;

	public ProceduralHouse(GameObject house){
		Vector3 vc = house.transform.localScale;
		//int x = (int)vc.x + Random.Range (-3,3);
		//int y = (int)vc.y + Random.Range (5, 20);
		//int z = (int)vc.z + Random.Range (-3,3);
		//MonoBehaviour.print (x+ " " + y +" "+ z);
		
		//house.transform.localScale = new Vector3(x,y,z);
		GameObject empty = new GameObject ("House");
		roof = MonoBehaviour.Instantiate(Resources.Load ("Prefabs/Roof3", typeof(GameObject))) as GameObject;
		roof.transform.parent = empty.transform;
		roof.transform.position = house.transform.position;
		roof.transform.localScale = vc;
		//roof.transform.position = house.transform.position;
		//roof.transform.position.y += y;

		}
}
