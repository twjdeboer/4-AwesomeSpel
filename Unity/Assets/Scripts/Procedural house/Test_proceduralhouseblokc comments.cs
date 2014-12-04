using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Test_proceduralhouseblokccomments : MonoBehaviour {
	public GameObject world;	//self
	public bool buildhouse;		//turn of generation of houses

	private GameObject[] Houseblok;
	
	private ProceduralHouseblok newHouseBlok;
	private GameObject devidehouse;
	
	private ProceduralHouse[] newHouse;
	private GameObject[] Houses;
	private GameObject[] GrassFloor;
	private int j;
	private int n;
	private bool[] boolean=new bool[6];				//search items createarray();
	private string[] leftrightstring=new string[6];
	private string[] searchtag=new string[6];
	
	private bool[] Housebloks = new bool[2];		
	private bool preHouses;							
	private int loadsize;					//for loadscreen
	private int loadprogress;
	
	void Start() {
		j = 0;
		n = 0;
		Housebloks [0] = true;
		preHouses = false;
		Houseblok = GameObject.FindGameObjectsWithTag ("Huisblok");
		GrassFloor = new GameObject[Houseblok.Length];
		createarray (); //set search items
		loadsize = GameObject.FindGameObjectsWithTag ("Huisblok").Length + GameObject.FindGameObjectsWithTag ("Huisblokback").Length;	//total amount of houseblocks to create
		loadprogress = 0;
		if (Houseblok.Length == 0) {//if Huisblok not found check for Houseblokback
			Housebloks[0]=false;
			Housebloks[1]=true;
			Houseblok = GameObject.FindGameObjectsWithTag ("Huisblokback");
			GrassFloor = new GameObject[Houseblok.Length];
			
			if(Houseblok.Length==0){		// if Huisblokback nof fount check for prebuilding
				Houses = GameObject.FindGameObjectsWithTag ("Prebuilding");
				preHouses=true;
				Housebloks[1]=false;
			}
		}
		
	}

	void Update(){
		
		if(Housebloks[0]){	//checked if houseblok.length>0	/.................................part1 
			createPrebuilding("Prebuilding",false,j);	//
			loadprogress++;
			if(j==Houseblok.Length-1){	//change houseblok to Huisblokback
				Houseblok = GameObject.FindGameObjectsWithTag ("Huisblokback");
				if(Houseblok.Length>0){ //if huisblockback.length>0 set bool true
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
					newHouse = new ProceduralHouse[Houses.Length];
				}
			}
		}

		if(Housebloks[1]){	// checked if houseblok.length>0 and after huisblok is done. /...........part2 after housebloks[0] is made false
			createPrebuilding ("Prebuildingback",true,j);
			loadprogress++;
			if(j==Houseblok.Length-1){
				j=0;
				Housebloks[1]=false;
				preHouses=buildhouse;
				getloadsize();
				Houses = GameObject.FindGameObjectsWithTag ("Prebuilding");
				newHouse = new ProceduralHouse[Houses.Length];
			}
		}
		
		if(preHouses){  //.........................................................    part 3 after houseblok[1] is made false again.
			if((j==Houses.Length)|(Houses.Length==0)){//goto next search item if true
				
				n++;
				if(n>5){//6 search items index 5
					preHouses=false;
				}
				if(preHouses){//check again because lines above can make it false
					j=0;
					Houses = GameObject.FindGameObjectsWithTag (searchtag[n]);

					newHouse = new ProceduralHouse[Houses.Length];
				}
				
			}
			if((Houses.Length!=0)&preHouses){
				createHouses (boolean[n],leftrightstring[n],j);	
				loadprogress++;
			}
		}

		print(loadprogress+" "+loadsize);

		j++; //index element 
		if(!preHouses& !Housebloks[0] & !Housebloks[1] ){//if no more generation remove script from world and destroy everyting with tag Destroy
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
	
	/*
	 *create cubes from big cube with tags searchtag depending on position
	 *
	 *
	 *@param string settag (tag it needs to be set)
	 *@param bool back (true if there is a block behind
	 *@param int j (index of found houseblock
	 *
	 *
	 */
	public void createPrebuilding(string settag,bool back,int j){
		createGrassfloor(j);
		
		newHouseBlok = new ProceduralHouseblok (Houseblok[j]);
		
		if(newHouseBlok.devidefirst (back)){	//true if size not to small-> created outer house cubes.
			Houseblok[j].transform.tag="Huisblok2";	//settag huisblok2 for further processing
		}
		
		
		devidehouse = GameObject.FindGameObjectWithTag ("Huisblok2");
		int i = 0;
		while (devidehouse!=null) {//while it can find a cube with tag huiblok2
			newHouseBlok = new ProceduralHouseblok (devidehouse);	
			if(newHouseBlok.devide (settag)){	//true if size not to small->split cube into 2 cubes
				devidehouse.transform.tag = "Huisblok";	//change tag because doesnt destroy inmidiately
				Destroy (devidehouse);
			}
			
			devidehouse = GameObject.FindGameObjectWithTag ("Huisblok2");	//search dividable cubes
			i++;
			if (i > 1000) { //safety
				break;
			}	
		}
	}
	/*
	 *@param bool back (if house at back)
	 *@param string leftright (searchtag string if outer house)
	 *@param int i (index gameobject house)
	 */
	public void createHouses(bool back,string leftright,int i){	
		
		newHouse[i]= new ProceduralHouse(Houses[i],back,leftright);	//set object
		newHouse[i].BuildrandomHouse();								//creat house
		newHouse[i].empty.transform.parent =world.transform;		//parent to world
	}

	/*
	 * creates grassfloor under house block
	 * @param int j (index house block)
	 */ 
	public void createGrassfloor(int j){	
		GrassFloor[j] = MonoBehaviour.Instantiate(Resources.Load ("Prefabs/ProceduralHouse/GrassFloor", typeof(GameObject))) as GameObject;
		GrassFloor[j].transform.position=Houseblok[j].transform.position;
		Vector3 temp=GrassFloor[j].transform.localScale;
		temp.x=Houseblok[j].transform.localScale.x; 	//get scale (param .y cant be used of houseblok)
		temp.z=Houseblok[j].transform.localScale.z;
		GrassFloor[j].transform.localScale=temp;		//set scale,pos,rot
		GrassFloor[j].transform.rotation=Houseblok[j].transform.rotation;
		GrassFloor[j].transform.parent = world.transform;
	}

	/*
	 * because it is some text only used 2 times
	 */
	private void getloadsize(){
		loadsize= GameObject.FindGameObjectsWithTag ("Prebuilding").Length + GameObject.FindGameObjectsWithTag ("Prebuildingl").Length
			+GameObject.FindGameObjectsWithTag ("Prebuildingr").Length+GameObject.FindGameObjectsWithTag ("Prebuildingback").Length+
				GameObject.FindGameObjectsWithTag ("Prebuildinglback").Length+GameObject.FindGameObjectsWithTag ("Prebuildingrback").Length;
		//print (loadsize+" Loadsize2");
		loadprogress = 0;
	}


	/*
	 * creates array of search items for houses
	 * boolean if house at back
	 * string search tag
	 * string if outerbuilding l or r
	 */
	public void createarray(){
		boolean=new bool[]{false,false,false,true,true,true};
		searchtag=new string[]{"Prebuilding","Prebuildingl","Prebuildingr","Prebuildingback","Prebuildinglback","Prebuildingrback"};
		leftrightstring=new string[]{" ","l","r"," ","l","r"};
	}
	
}