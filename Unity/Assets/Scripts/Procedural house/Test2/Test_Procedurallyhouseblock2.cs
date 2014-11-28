using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class Test_Procedurallyhouseblock2 : MonoBehaviour {
	public GameObject world;
	private GameObject[] Houseblok;
	private ProceduralHouseblok2 newHouseBlok;
	private GameObject devidehouse;
	
	private ProceduralHouse2[] newHouse;
	private GameObject[] Houses;
	private GameObject[] GrassFloor;
	
	void Start() {
		createPrebuilding ("Huisblok","Prebuilding",false);
		createPrebuilding ("Huisblokback","Prebuildingback",true);


		createHouses ("Prebuildingl",false,"l");
		createHouses ("Prebuildingr",false,"r");
		createHouses ("Prebuildinglback",true,"l");
		createHouses ("Prebuildingrback",true,"r");
		createHouses ("Prebuilding",false," ");
		createHouses ("Prebuilding",false," ");
		createHouses ("Prebuildingback",true," ");
		createHouses ("Prebuildingback",true," ");

	}

	public void createPrebuilding(string search,string settag,bool back){
		Houseblok = GameObject.FindGameObjectsWithTag (search);
		GrassFloor = new GameObject[Houseblok.Length];
		
		//print (Houseblok.Length);
		for(int j=0;j<Houseblok.Length;j++){
			
			createGrassfloor(j);
			
			newHouseBlok = new ProceduralHouseblok2 (Houseblok[j]);
			
			if(newHouseBlok.devidefirst (back)){
				Houseblok[j].transform.tag="Huisblok2";
			}


			devidehouse = GameObject.FindGameObjectWithTag ("Huisblok2");
			int i = 0;
			while (devidehouse!=null) {
				newHouseBlok = new ProceduralHouseblok2 (devidehouse);
				if(newHouseBlok.devide (settag)){
					devidehouse.transform.tag = "Huisblok";
					Destroy (devidehouse);
				}
				
				devidehouse = GameObject.FindGameObjectWithTag ("Huisblok2");
				i++;
				if (i > 1000) {
					break;
				}
				
			}
			print("Done " + i);
			
		}
		
		//createHouses (settag,back," ");


	}

	public void createHouses(string searchtag,bool back,string leftright){
		Houses = GameObject.FindGameObjectsWithTag (searchtag);
		
		
		newHouse = new ProceduralHouse2[Houses.Length];
		
		for (int i=0; i < Houses.Length; i++) {
			newHouse[i]= new ProceduralHouse2(Houses[i],back,leftright);
		}
		for (int i=0; i < Houses.Length; i++) {
			newHouse[i].BuildrandomHouse();
			print ("Next house ");
			//newHouse[i].BuildrandomHouse();
			newHouse[i].empty.transform.parent =world.transform;
		}
	}
	public void createGrassfloor(int j){
		GrassFloor[j] = MonoBehaviour.Instantiate(Resources.Load ("Prefabs/GrassFloor", typeof(GameObject))) as GameObject;
		GrassFloor[j].transform.position=Houseblok[j].transform.position;
		Vector3 temp=GrassFloor[j].transform.localScale;
		temp.x=Houseblok[j].transform.localScale.x;
		temp.z=Houseblok[j].transform.localScale.z;
		GrassFloor[j].transform.localScale=temp;
		GrassFloor[j].transform.rotation=Houseblok[j].transform.rotation;
		GrassFloor[j].transform.parent = world.transform;
		
	}
	
}