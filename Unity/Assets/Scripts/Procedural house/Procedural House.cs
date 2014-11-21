using UnityEngine;
using System.Collections;

public class ProceduralHouse{
	private GameObject House;
	private GameObject roof;
	public GameObject empty;
	private GameObject newhouse;
	private GameObject door;

	private Vector3 scale;
	private Vector3 pos;
	private Quaternion rot;

	private static int staticnumber=0; //eigenlijk niet meer nodig maar wel handig
	private int mynumber;

	private int omlijsting;
	private GameObject world;

	public ProceduralHouse(GameObject House,GameObject world){
		empty = new GameObject ("HouseBox"+staticnumber);
		mynumber = staticnumber;
		staticnumber++;
		this.House = House;
		scale = House.transform.localScale;
		pos = House.transform.position;
		rot = House.transform.rotation;
		empty.transform.position = pos;
		omlijsting = Random.Range (1, 5);
		}


	public void BuildrandomHouse(){
		randomDim ();
		BuildHouse();
	}

	public void BuildHouse(){
		Build ();
		empty.transform.rotation = rot;
		//empty.transform.parent = world.transform;
	}
		
	// Update is called once per frame
	private void Build(){

		//maakt blokje huis aan
		MakeBlok ();

		//maakt dak aan
		MakeRoof ();

		//maakt deur aan
		MakeDoor ();

		//maakt ramen aan
		//MakeWindows();
		MonoBehaviour.Destroy(House);

	}

	private void MakeBlok(){
		Vector3 temp = pos;
		newhouse = MonoBehaviour.Instantiate(Resources.Load ("Prefabs/House2", typeof(GameObject))) as GameObject;
		newhouse.transform.parent = empty.transform;
		temp.y += (float)0.5*scale.y;	//0.5 anders in de grond
		newhouse.transform.position = temp;
		newhouse.transform.localScale = scale;

		randomMaterial(newhouse,"House",1,4);
		//newhouse.renderer.material =(Material)Resources.Load ("Materials/House/"+Random.Range (0, 5), typeof(Material));
		}

	private void MakeRoof(){
		Vector3 temp = pos;
		roof = MonoBehaviour.Instantiate(Resources.Load ("Prefabs/Roof2", typeof(GameObject))) as GameObject;
		roof.transform.parent = empty.transform;
		temp.y += (float)1*scale.y;		//1 zodat boven het huisblokje

		roof.transform.position = temp;
		roof.transform.localScale = scale;

		GameObject roofchild=GameObject.Find ("/HouseBox"+mynumber+"/Roof2(Clone)/Roof");

		int a = Random.Range(-1,2);
		roof.transform.rotation = Quaternion.Euler(0, 90*a, 0);
	
		randomMaterial(roofchild,"Roof",1,4);
		}

	public void MakeDoor(){
		Vector3 temp = pos;
		door = MonoBehaviour.Instantiate(Resources.Load ("Prefabs/Door", typeof(GameObject))) as GameObject;
		door.transform.parent = empty.transform;

		GameObject doorWindow=GameObject.Find ("/HouseBox"+mynumber+"/Door(Clone)/Window");
		GameObject doorBottom=GameObject.Find ("/HouseBox"+mynumber+"/Door(Clone)/Bottom");
		GameObject doorTop=GameObject.Find ("/HouseBox"+mynumber+"/Door(Clone)/Top");
		GameObject doorLeft=GameObject.Find ("/HouseBox"+mynumber+"/Door(Clone)/Left");
		GameObject doorRight=GameObject.Find ("/HouseBox"+mynumber+"/Door(Clone)/Right");

		temp.y += (float)0.5*doorWindow.transform.localScale.y;	//grond niveau
		temp.x += (float)0.5*scale.x;		//rand blokje
		temp.z += (float)Random.Range ((float)-0.5*scale.x+2,(float)0.5*scale.x-2);

		door.transform.position = temp;
		
		randomMaterial(doorWindow,"Door",1,4);


		randomMaterial(doorBottom,"Omlijsting",omlijsting);
		randomMaterial(doorTop,"Omlijsting",omlijsting);
		randomMaterial(doorLeft,"Omlijsting",omlijsting);
		randomMaterial(doorRight,"Omlijsting",omlijsting);
		}
	public void MakeWindows(){


		}

	private void randomDim(){
		int n = 5;
		int x = (int)scale.x + Random.Range (-n,n);
		int y = (int)scale.y + Random.Range (5, 20);
		int z = (int)scale.z + Random.Range (-n,n);
		//MonoBehaviour.print (x+ " " + y +" "+ z+" Dimenties");
		House.transform.localScale = new Vector3(x,y,z);
		scale = new Vector3 (x, y, z);
	}

	private void randomMaterial(GameObject self,string materialname,int min,int max){
		self.renderer.material =(Material)Resources.Load ("Materials/"+materialname+"/"+Random.Range (min, max+1), typeof(Material));
		}

	private void randomMaterial(GameObject self,string materialname,int nummer){
		self.renderer.material =(Material)Resources.Load ("Materials/"+materialname+"/"+nummer, typeof(Material));		
	}
}
