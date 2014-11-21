using UnityEngine;
using System.Collections;

public class ProceduralHouse{

	public GameObject empty;
	private GameObject House;
	private GameObject roof;
	private GameObject newhouse;
	private GameObject door;
	private GameObject[] window;

	private Vector3 scale;
	private Vector3 pos;
	private Quaternion rot;

	private static int staticnumber=0; //eigenlijk niet meer nodig maar wel handig
	private int mynumber;

	private int omlijsting;	//door/windows

	private float doorzpos;
	private float doorypos;
	private float doorzscale;
	private float dooryscale;

	private	int[] xposa;
	private float[] zpos; // windows
	private float zscale;
	private float[] ypos;
	private float yscale;

	public ProceduralHouse(GameObject House){
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
		MakeWindows ();
		MonoBehaviour.Destroy(House);

	}

	private void MakeBlok(){
		Vector3 temp = pos;
		newhouse = MonoBehaviour.Instantiate(Resources.Load ("Prefabs/House2", typeof(GameObject))) as GameObject;
		newhouse.transform.parent = empty.transform;
		temp.y += (float)0.5*scale.y;	//0.5 anders in de grond
		newhouse.transform.position = temp;
		newhouse.transform.localScale = scale;

		randomMaterial(newhouse,"House",1,5);
		//newhouse.renderer.material =(Material)Resources.Load ("Materials/House/"+Random.Range (0, 5), typeof(Material));
		}

	private void MakeRoof(){
		int roofnr = Random.Range (1, 2);	//nummer of roofs
		Vector3 temp = pos;
		roof = MonoBehaviour.Instantiate(Resources.Load ("Prefabs/Roof"+roofnr, typeof(GameObject))) as GameObject;
		roof.transform.parent = empty.transform;
		temp.y += (float)1*scale.y;		//1 zodat boven het huisblokje

		roof.transform.position = temp;


		GameObject roofchild=GameObject.Find ("/HouseBox"+mynumber+"/Roof"+roofnr+"(Clone)/Roof");

		int a = Random.Range(0,2);
		roof.transform.rotation = Quaternion.Euler(0, 90*a, 0);
		Vector3 tempscale = scale;
		if (a == 1) {
			float tempp=tempscale.x;
			tempscale.x=tempscale.z;
			tempscale.z=(float)tempp;
			}
		roof.transform.localScale = tempscale;

		randomMaterial(roofchild,"Roof",1,4);
		}

	public void MakeDoor(){
		door = MonoBehaviour.Instantiate(Resources.Load ("Prefabs/Door", typeof(GameObject))) as GameObject;
		door.transform.parent = empty.transform;

		GameObject doorWindow=GameObject.Find ("/HouseBox"+mynumber+"/Door(Clone)/Window");
		GameObject doorBottom=GameObject.Find ("/HouseBox"+mynumber+"/Door(Clone)/Bottom");
		GameObject doorTop=GameObject.Find ("/HouseBox"+mynumber+"/Door(Clone)/Top");
		GameObject doorLeft=GameObject.Find ("/HouseBox"+mynumber+"/Door(Clone)/Left");
		GameObject doorRight=GameObject.Find ("/HouseBox"+mynumber+"/Door(Clone)/Right");

		Vector3 temp = pos;
		doorzscale = doorWindow.transform.localScale.z;
		dooryscale = doorWindow.transform.localScale.y;
		temp.y += (float)0.5*dooryscale;	//grond niveau
		temp.x += (float)0.5*scale.x;		//rand blokje

		temp.z += (float)Random.Range ((float)-0.5 * scale.z + 2, (float)0.5 * scale.z - 2);
		doorypos = temp.y;
		doorzpos = temp.z;
		door.transform.position = temp;
		
		randomMaterial(doorWindow,"Door",1,5);

		randomMaterial(doorBottom,"Omlijsting",omlijsting);
		randomMaterial(doorTop,"Omlijsting",omlijsting);
		randomMaterial(doorLeft,"Omlijsting",omlijsting);
		randomMaterial(doorRight,"Omlijsting",omlijsting);
	}


	private void MakeWindows(){
		int windownr = 6;
		int windowtexture = Random.Range (1, 5);
		window = new GameObject[windownr];
		zpos = new float[windownr];
		ypos = new float[windownr];
		xposa = new int[windownr];

		for (int i=0; i<windownr; i++) {
			window [i] = MonoBehaviour.Instantiate (Resources.Load ("Prefabs/Window", typeof(GameObject))) as GameObject;
			GameObject windowWindow=GameObject.Find ("/Window(Clone)/Window");
			GameObject windowBottom=GameObject.Find ("/Window(Clone)/Bottom");
			GameObject windowTop=GameObject.Find ("/Window(Clone)/Top");
			GameObject windowLeft=GameObject.Find ("/Window(Clone)/Left");
			GameObject windowRight=GameObject.Find ("/Window(Clone)/Right");
			randomMaterial(windowWindow,"Windows",windowtexture);



			randomMaterial(windowBottom,"Omlijsting",omlijsting);
			randomMaterial(windowTop,"Omlijsting",omlijsting);
			randomMaterial(windowLeft,"Omlijsting",omlijsting);
			randomMaterial(windowRight,"Omlijsting",omlijsting);
			zscale=windowWindow.transform.localScale.z;
			yscale=windowWindow.transform.localScale.y;

			bool validplace = windowsetter(i);
			if(validplace){
				window [i].transform.parent = empty.transform;
			}
			MonoBehaviour.print("valid "+validplace);
		}
	}

	private bool windowsetter(int i){
		int a=0;
		while (a==0) {
			a=Random.Range(-1,2);
			}
		Vector3 temp = pos;
		temp.y += Random.Range((float)0.5 * dooryscale,scale.y-(float)0.5*yscale);	//grond niveau
		temp.x += (float)0.5 * scale.x*a;		//rand blokje
		temp.z += (float)Random.Range ((float)-0.5 * scale.z + 2, (float)0.5 * scale.z - 2);
		bool validplace = false;

		string zcoords=" ";
		string ycoords=" ";
		string xcoords=" ";
		for (int o =0; o<i; o++) {
			zcoords+=zpos[o];
			ycoords+=ypos[o];
			xcoords+=xposa[o];
		}

		MonoBehaviour.print(zpos+" "+temp.z);
		MonoBehaviour.print(ypos+" "+temp.y);
		MonoBehaviour.print(xposa+" "+a);

		if (a == 1) {
			if ((((temp.z + zscale) > doorzpos) & ((temp.z - zscale) < doorzpos))) {
				if ((((temp.z + zscale) > doorzpos) & ((temp.z - zscale) < doorzpos))) {
				validplace = true;
				}
			}
		}
		for (int j=0; j<i; j++) {
			if((xposa[j]!=a)){
				if(( ((temp.z+zscale)>zpos[j]) & ((temp.z-zscale)<zpos[j]) )){
					if(( ((temp.y+yscale)>ypos[j])&((temp.y-yscale)<ypos[j]) )){
						validplace=true;
					}
				}
			}
		}
		window [i].transform.position = temp;
		xposa [i] = a;
		zpos [i] = temp.z;
		ypos [i] = temp.y;
		if (!validplace) {
			xposa[i]=100;
			zpos[i]=100;
			ypos[i]=100;
			MonoBehaviour.Destroy(window[i]);
		}
		return validplace;
	}

	private void randomDim(){
		int n = 6;
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
		//self.renderer.material =(Material)Resources.Load ("Materials/"+materialname+"/"+nummer, typeof(Material));		
	}
}
