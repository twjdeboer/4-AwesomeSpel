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
	private float Timer=0;
	private GameObject[] housboxes;
	private string currentload;
	private GameObject loader;
	
	void Awake()
	{
		//loadScene = GameObject.Find("Loaded");
		//loadScene.SetActive(false);
	}
	
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
					//print (Houses.Length+" Houses.length");
					//newHouse = new ProceduralHouse[Houses.Length];
				}
				
			}
			if((Houses.Length!=0)&preHouses){
				createHouses (boolean[n],leftrightstring[n],j);	
				loadprogress++;
			}
		}
		//Timer += Time.deltaTime;
		print(loadprogress+" "+loadsize);
		//print (Time.deltaTime);
		j++;
		text.text = currentload + ": " + loadprogress + " of "+loadsize;
		//print (!preHouses+"  "+!Housebloks[0]+"  "+!Housebloks[1]);
		if(!preHouses& !Housebloks[0] & !Housebloks[1] ){
			text.text = "Generating done";
			//loadScene.SetActive(true);
			ResourceManager.World = GameObject.Find("HouseContainer");

			GameObject[] destroying = GameObject.FindGameObjectsWithTag("Destroy");

			for(int i=0;i<destroying.Length;i++){
				Destroy(destroying[i]);
			}
			DontDestroyOnLoad(HouseContainer.transform.gameObject);
		
			setactive2 ();
			print ("Done");
			text.text = "World visability done";
			Destroy(GameObject.Find("Worldloader"));
			Application.LoadLevel(nextscene);

		}
	}

	//.............................................................................end update






	
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
	
	public void createHouses(bool back,string leftright,int i){
		
		newHouse.UpdateInternal(Houses[i],back,leftright);
		newHouse.BuildrandomHouse();
		newHouse.empty.transform.parent =HouseContainer.transform;
	}
	
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
	private void getloadsize(){
		loadsize= GameObject.FindGameObjectsWithTag ("Prebuilding").Length + GameObject.FindGameObjectsWithTag ("Prebuildingl").Length
			+GameObject.FindGameObjectsWithTag ("Prebuildingr").Length+GameObject.FindGameObjectsWithTag ("Prebuildingback").Length+
				GameObject.FindGameObjectsWithTag ("Prebuildinglback").Length+GameObject.FindGameObjectsWithTag ("Prebuildingrback").Length;
		//print (loadsize+" Loadsize2");
		loadprogress = 0;
	}
	
	public void createarray(){
		boolean=new bool[]{false,true,false,false,true,true};
		searchtag=new string[]{"Prebuilding","Prebuildingback","Prebuildingr","Prebuildingl","Prebuildinglback","Prebuildingrback"};
		leftrightstring=new string[]{" "," ","r","l","l","r"};
	}

	public void setactive2(){
		HouseContainer.SetActive (true);
	}

	public void setactive(){
		housboxes = GameObject.FindGameObjectsWithTag("Setactive");
		print(housboxes.Length);
		for(int i=0;i<housboxes.Length;i++){
			housboxes[i].SetActive(true);
			housboxes[i].transform.tag="Untagged";
		}
	}
	
}