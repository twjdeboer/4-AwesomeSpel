using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class Test_Procedurallyhouseblock : MonoBehaviour {
	public GameObject world;
	public bool buildhouse;
	private GameObject[] Houseblok;

	private ProceduralHouseblok newHouseBlok;
	private GameObject devidehouse;
	
	private ProceduralHouse[] newHouse;
	private GameObject[] Houses;
	private GameObject[] GrassFloor;
	private int j;
	private int n;
	private bool[] boolean=new bool[6];
	private string[] leftrightstring=new string[6];
	private string[] searchtag=new string[6];

	private bool[] Housebloks = new bool[2];
	private bool preHouses;

	void Start() {
		j = 0;
		n = 0;
		Housebloks [0] = true;
		preHouses = false;
		Houseblok = GameObject.FindGameObjectsWithTag ("Huisblok");
		GrassFloor = new GameObject[Houseblok.Length];
		createarray ();

		print (Houseblok.Length+" houseblok length");
		if (Houseblok.Length == 0) {
			Housebloks[0]=false;
			Housebloks[1]=true;
			Houseblok = GameObject.FindGameObjectsWithTag ("Huisblokback");
			GrassFloor = new GameObject[Houseblok.Length];
			print (Houseblok.Length+" blokback length");
			if(Houseblok.Length==0){
				Houses = GameObject.FindGameObjectsWithTag ("Prebuilding");
				preHouses=true;
				Housebloks[1]=false;
			}
		}
		/*
		createPrebuilding ("Huisblok","Prebuilding",false);
		createPrebuilding ("Huisblokback","Prebuildingback",true);
		createHouses ("Prebuilding",false," ");


		createHouses ("Prebuildingl",false,"l");
		createHouses ("Prebuildingr",false,"r");

		createHouses ("Prebuildingback",true," ");


		createHouses ("Prebuildinglback",true,"l");
		createHouses ("Prebuildingrback",true,"r");
		*/

		
	}
	void Update(){


		if(Housebloks[0]){
			createPrebuilding("Prebuilding",false,j);
			if(j==Houseblok.Length-1){
				Houseblok = GameObject.FindGameObjectsWithTag ("Huisblokback");
				if(Houseblok.Length>0){
					GrassFloor = new GameObject[Houseblok.Length];
					j=0;
					Housebloks[0]=false;
					Housebloks[1]=true;
				}else{
					Housebloks[0]=false;
					preHouses=buildhouse;
					j=0;
					Houses = GameObject.FindGameObjectsWithTag ("Prebuilding");
					newHouse = new ProceduralHouse[Houses.Length];
				}
			}
		}
		if(Housebloks[1]){
			createPrebuilding ("Prebuildingback",true,j);
			if(j==Houseblok.Length-1){
				j=0;
				Housebloks[1]=false;
				preHouses=buildhouse;
				Houses = GameObject.FindGameObjectsWithTag ("Prebuilding");
				newHouse = new ProceduralHouse[Houses.Length];
				}
		}

		if(preHouses){
			if((j==Houses.Length)|(Houses.Length==0)){

				n++;
				if(n>5){
					preHouses=false;
				}
				if(preHouses){
					j=0;
					Houses = GameObject.FindGameObjectsWithTag (searchtag[n]);
					//print (Houses.Length+" Houses.length");
					newHouse = new ProceduralHouse[Houses.Length];
				}

			}
			if((Houses.Length!=0)&preHouses){
				createHouses (boolean[n],leftrightstring[n],j);	
			}
		}
	
		//print (j+" j+n "+n);

		j++;
		if(!preHouses& !Housebloks[0] & !Housebloks[1] ){
			print ("Done");
			Component other =world.GetComponent("Test_Procedurallyhouseblock");
			GameObject[] destroying = GameObject.FindGameObjectsWithTag("Destroy");
			if(other != null)
			{
				Destroy(other);
			}
			for(int i=0;i<destroying.Length;i++){
				Destroy(destroying[i]);
			}
		}
	}


	public void createPrebuilding(string settag,bool back,int j){
		createGrassfloor(j);
		
		newHouseBlok = new ProceduralHouseblok (Houseblok[j]);
		
		if(newHouseBlok.devidefirst (back)){
			Houseblok[j].transform.tag="Huisblok2";
		}
		
		
		devidehouse = GameObject.FindGameObjectWithTag ("Huisblok2");
		int i = 0;
		while (devidehouse!=null) {
			newHouseBlok = new ProceduralHouseblok (devidehouse);
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
		//print("Done " + i);

		//createHouses (settag,back," ");
	}
	
	public void createHouses(bool back,string leftright,int i){

		newHouse[i]= new ProceduralHouse(Houses[i],back,leftright);
		newHouse[i].BuildrandomHouse();
		print ("Next house ");
		//newHouse[i].BuildrandomHouse();
		newHouse[i].empty.transform.parent =world.transform;
	}

	public void createGrassfloor(int j){
		GrassFloor[j] = MonoBehaviour.Instantiate(Resources.Load ("Prefabs/ProceduralHouse/GrassFloor", typeof(GameObject))) as GameObject;
		GrassFloor[j].transform.position=Houseblok[j].transform.position;
		Vector3 temp=GrassFloor[j].transform.localScale;
		temp.x=Houseblok[j].transform.localScale.x;
		temp.z=Houseblok[j].transform.localScale.z;
		GrassFloor[j].transform.localScale=temp;
		GrassFloor[j].transform.rotation=Houseblok[j].transform.rotation;
		GrassFloor[j].transform.parent = world.transform;
	}


	public void createarray(){
		boolean=new bool[]{false,false,false,true,true,true};
		searchtag=new string[]{"Prebuilding","Prebuildingl","Prebuildingr","Prebuildingback","Prebuildinglback","Prebuildingrback"};
		leftrightstring=new string[]{" ","l","r"," ","l","r"};
	}
	
}