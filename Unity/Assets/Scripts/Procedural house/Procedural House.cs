using UnityEngine;
using System.Collections;

public class ProceduralHouse{
	private GameObject House;
	private GameObject roof;
	private GameObject empty;
	private GameObject newhouse;
	private GameObject door;
	private Vector3 scale;
	private Vector3 pos;
	private Quaternion rot;


	public ProceduralHouse(GameObject House){

		this.House = House;
		scale = House.transform.localScale;
		pos = House.transform.position;
		rot = House.transform.rotation;
		}

	public void randomHouse (){
		empty = new GameObject ("HouseBox");

		empty.transform.position = pos;
		MakeHouse ();
		empty.transform.rotation = rot;
	}
	
	// Update is called once per frame
	public void MakeHouse(){

		randomDim ();
		
		//maakt blokje huis aan
		MakeBlok ();

		//maakt dak aan
		MakeRoof ();

		//maakt windows aan of deur/specifiek voor deur
		MakeDoor ();

		MonoBehaviour.Destroy(House);

	}

	public void MakeBlok(){
		Vector3 temp = pos;
		newhouse = MonoBehaviour.Instantiate(Resources.Load ("Prefabs/House2", typeof(GameObject))) as GameObject;
		newhouse.transform.parent = empty.transform;
		temp.y += (float)0.5*scale.y;	//0.5 anders in de grond
		newhouse.transform.position = temp;
		newhouse.transform.localScale = scale;

		randomMaterial(newhouse,"House",0,4);
		//newhouse.renderer.material =(Material)Resources.Load ("Materials/House/"+Random.Range (0, 5), typeof(Material));
		}

	public void MakeRoof(){
		Vector3 temp = pos;
		roof = MonoBehaviour.Instantiate(Resources.Load ("Prefabs/Roof2", typeof(GameObject))) as GameObject;
		roof.transform.parent = empty.transform;
		temp.y += (float)1*scale.y;		//1 zodat boven het huisblokje
		roof.transform.position = temp;
		roof.transform.localScale = scale;

		randomMaterial(roof,"Roof",0,4);
		
		}
	public void MakeDoor(){
		Vector3 temp = pos;
		door = MonoBehaviour.Instantiate(Resources.Load ("Prefabs/Door", typeof(GameObject))) as GameObject;
		door.transform.parent = empty.transform;
		MonoBehaviour.print ("door make");
		GameObject doorWindow=GameObject.Find ("/HouseBox/Door(Clone)/Window");
		GameObject doorBottom=GameObject.Find ("/HouseBox/Door(Clone)/Bottom");
		GameObject doorTop=GameObject.Find ("/HouseBox/Door(Clone)/Top");
		GameObject doorLeft=GameObject.Find ("/HouseBox/Door(Clone)/Left");
		GameObject doorRight=GameObject.Find ("/HouseBox/Door(Clone)/Right");

		temp.y += (float)0.5*doorWindow.transform.localScale.y;	//grond niveau
		temp.x += (float)0.5*scale.x;		//rand blokje
		temp.z += (float)Random.Range ((float)-0.5*scale.x+2,(float)0.5*scale.x-2);
		door.transform.position = temp;
		
		randomMaterial(doorWindow,"Door",0,4);
		int a = Random.Range (0, 5);

		randomMaterial(doorBottom,"Door",a);
		randomMaterial(doorTop,"Door",a);
		randomMaterial(doorLeft,"Door",a);
		randomMaterial(doorRight,"Door",a);
		}
	
	public void randomDim(){
		int n = 5;
		int x = (int)scale.x + Random.Range (-n,n);
		int y = (int)scale.y + Random.Range (5, 20);
		int z = (int)scale.z + Random.Range (-n,n);
		MonoBehaviour.print (x+ " " + y +" "+ z);
		House.transform.localScale = new Vector3(x,y,z);
		scale = new Vector3 (x, y, z);
	}

	public void randomMaterial(GameObject self,string name,int min,int max){
		self.renderer.material =(Material)Resources.Load ("Materials/"+name+"/"+Random.Range (min, max+1), typeof(Material));
		}

	public void randomMaterial(GameObject self,string name,int nummer){
		self.renderer.material =(Material)Resources.Load ("Materials/"+name+"/"+nummer, typeof(Material));
		
	}
}
