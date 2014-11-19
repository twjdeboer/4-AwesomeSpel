using UnityEngine;
using System.Collections;

public class Test_proceduralhouse : MonoBehaviour {
	private GameObject House;
	private GameObject roof;
	private GameObject empty;
	private GameObject newhouse;
	private GameObject window;
	// Use this for initialization
	void Start () {
		empty = new GameObject ("HouseBox");
		House = GameObject.FindGameObjectWithTag ("B1");

		Vector3 vc = House.transform.localScale;
		int x = (int)vc.x + Random.Range (-3,3);
		int y = (int)vc.y + Random.Range (5, 20);
		int z = (int)vc.z + Random.Range (-3,3);
		MonoBehaviour.print (x+ " " + y +" "+ z);
		House.transform.localScale = new Vector3(x,y,z);


		Vector3 scale = House.transform.localScale;
		Vector3 pos = House.transform.position;

		//maakt blokje huis aan
		newhouse = MonoBehaviour.Instantiate(Resources.Load ("Prefabs/House2", typeof(GameObject))) as GameObject;
		newhouse.transform.parent = empty.transform;
		pos.y += (float)0.5*scale.y;	//0.5 anders in de grond
		newhouse.transform.position = pos;
		newhouse.transform.localScale = scale;



		//maakt dak aan
		roof = MonoBehaviour.Instantiate(Resources.Load ("Prefabs/Roof", typeof(GameObject))) as GameObject;
		roof.transform.parent = empty.transform;
		pos.y += (float)1*scale.y;		//1 zodat boven het huisblokje
		roof.transform.position = pos;
		roof.transform.localScale = scale;


		//maakt windows aan of deur/specifiek voor deur

		window = MonoBehaviour.Instantiate(Resources.Load ("Prefabs/Door", typeof(GameObject))) as GameObject;
		window.transform.parent = empty.transform;
		vc=window.transform.localScale;

		pos.y -= (float)1.5*scale.y-vc.y;	//grond niveau
		pos.x += (float)0.5*scale.x;		//rand blokje
		pos.z += (float)Random.Range ((float)-0.5*scale.x+1,(float)0.5*scale.x-1);
		window.transform.position = pos;

		Destroy(House);
	}
	
	// Update is called once per frame
	void Update () {
	}




}
