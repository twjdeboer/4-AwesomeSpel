using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Testblok2 : MonoBehaviour {
	public GameObject HouseContainer;
	public bool buildhouse;
	public string nextscene;
	public Text text;

	private GameObject[] Houseblok;
	
	private ProceduralHouseblok newHouseBlok;
	private GameObject devidehouse;
	
	private GameObject loadScene;
	private ProceduralHouse newHouse;
	private GameObject[] Houses;
	private GameObject[] GrassFloor;
	private int j;
	private int n;
	private bool[] boolean=new bool[6];
	private string[] leftrightstring=new string[6];
	private string[] searchtag=new string[6];
	
	private bool[] Housebloks = new bool[2];
	private bool preHouses;
	private int loadsize;
	private int loadprogress;
	private GameObject[] housboxes;
	private string currentload;
	private GameObject loader;
	

	/*
	 *sets housecontainer inactive
	 *creates 
	 * 
	 */
	void Start() {
		HouseContainer.SetActive (false);
		newHouse = new ProceduralHouse();
		j = 0;
		n = 0;
		Housebloks [0] = true;
		preHouses = false;
		Houseblok = GameObject.FindGameObjectsWithTag ("Huisblok");
		GrassFloor = new GameObject[Houseblok.Length];
		createarray ();
		loadsize = GameObject.FindGameObjectsWithTag ("Huisblok").Length + GameObject.FindGameObjectsWithTag ("Huisblokback").Length;
		loadprogress = 0;
		currentload = "Generating Cubes";
		if (Houseblok.Length == 0) {
			Housebloks[0]=false;
			Housebloks[1]=true;
			Houseblok = GameObject.FindGameObjectsWithTag ("Huisblokback");
			GrassFloor = new GameObject[Houseblok.Length];
			
			if(Houseblok.Length==0){
				currentload = "Generating Buildings";
				Houses = GameObject.FindGameObjectsWithTag ("Prebuilding");
				preHouses=true;
				Housebloks[1]=false;
			}
		}
		
	}

	/**
	 * exist out of 4 parts
	 * part 1 and 2 are sent to function create prebuilding to create smaller cubes from housebloks
	 * part 3 creates houses from small cubes
	 * part 4 runs when all houses are created and forwards to next scene.
	 */
	void FixedUpdate(){
		
		if(Housebloks[0]){
			createPrebuilding("Prebuilding",false,j);
			loadprogress++;
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
					getloadsize();
					currentload = "Generating Buildings";
					//newHouse = new ProceduralHouse[Houses.Length];
				}
			}
		}
		if(Housebloks[1]){
			createPrebuilding ("Prebuildingback",true,j);
			loadprogress++;
			if(j==Houseblok.Length-1){
				j=0;
				Housebloks[1]=false;
				preHouses=buildhouse;
				getloadsize();
				Houses = GameObject.FindGameObjectsWithTag ("Prebuilding");
				currentload = "Generating Buildings";
				//newHouse = new ProceduralHouse[Houses.Length];
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

				}
				
			}
			if((Houses.Length!=0)&preHouses){
				createHouses (boolean[n],leftrightstring[n],j);	
				loadprogress++;
			}
		}

		j++;
		text.text = currentload + ": " + loadprogress + " of "+loadsize;

		if(!preHouses& !Housebloks[0] & !Housebloks[1] ){
			text.text = "Generating done";

			GameObject[] destroying = GameObject.FindGameObjectsWithTag("Destroy");

			for(int i=0;i<destroying.Length;i++){
				Destroy(destroying[i]);
			}
			DontDestroyOnLoad(HouseContainer.transform.gameObject);
		
			setactive2 ();
			print ("Done");
			text.text = "Loading World - This could take a while";
			Destroy(GameObject.Find("Worldloader"));
			Application.LoadLevel(nextscene);

		}
	}

	//.............................................................................end update






	/**
	 * creates smaller cubes from houseblok
	 * input settag, what kind of house it is
	 * 		 back, if houuse behind the house
	 * 		 j , iteration 
	 */
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
	}

	/**
	 * creates house where houses[i] is current cube
	 * input - back, true if house behind the house
	 * 		 - leftright, string l or r if it is a end house at left or right
	 * 		 - i , iteration
	 */
	public void createHouses(bool back,string leftright,int i){
		
		newHouse.UpdateInternal(Houses[i],back,leftright);
		newHouse.BuildrandomHouse();
		newHouse.empty.transform.parent =HouseContainer.transform;
	}

	/**
	 *creates grassfloor for houseblok j with scale from houseblok
	 */
	public void createGrassfloor(int j){
		GrassFloor[j] = MonoBehaviour.Instantiate(Resources.Load ("Prefabs/ProceduralHouse/GrassFloor", typeof(GameObject))) as GameObject;
		GrassFloor[j].transform.position=Houseblok[j].transform.position;
		Vector3 temp=GrassFloor[j].transform.localScale;
		temp.x=Houseblok[j].transform.localScale.x;
		temp.z=Houseblok[j].transform.localScale.z;
		GrassFloor[j].transform.localScale=temp;
		GrassFloor[j].transform.rotation=Houseblok[j].transform.rotation;
		GrassFloor[j].transform.parent = HouseContainer.transform;
	}

	/**
	 * calculates amount of houses
	 */
	private void getloadsize(){
		loadsize= GameObject.FindGameObjectsWithTag ("Prebuilding").Length + GameObject.FindGameObjectsWithTag ("Prebuildingl").Length
			+GameObject.FindGameObjectsWithTag ("Prebuildingr").Length+GameObject.FindGameObjectsWithTag ("Prebuildingback").Length+
				GameObject.FindGameObjectsWithTag ("Prebuildinglback").Length+GameObject.FindGameObjectsWithTag ("Prebuildingrback").Length;
		//print (loadsize+" Loadsize2");
		loadprogress = 0;
	}

	/**
	 *creates 3 array used later for indicators
	 *
	 */
	public void createarray(){
		boolean=new bool[]{false,true,false,false,true,true};
		searchtag=new string[]{"Prebuilding","Prebuildingback","Prebuildingr","Prebuildingl","Prebuildinglback","Prebuildingrback"};
		leftrightstring=new string[]{" "," ","r","l","l","r"};
	}

	/*
	 *set housecontainer active
	 */
	public void setactive2(){
		HouseContainer.SetActive (true);
	}

	/*
	 *not used 
	 */
	public void setactive(){
		housboxes = GameObject.FindGameObjectsWithTag("Setactive");
		print(housboxes.Length);
		for(int i=0;i<housboxes.Length;i++){
			housboxes[i].SetActive(true);
			housboxes[i].transform.tag="Untagged";
		}
	}
	
}