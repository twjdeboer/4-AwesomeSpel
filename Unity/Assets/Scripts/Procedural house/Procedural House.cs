﻿using UnityEngine;
using System.Collections;

public class ProceduralHouse{
	
	public GameObject empty;
	private GameObject House;
	private GameObject roof;
	private GameObject newhouse;
	private GameObject door;
	private GameObject[] window;
	private GameObject[] windowWindow;

	private int windownr = 40;
	private int windowtexture = Random.Range (1, 5);
	
	private float xscale;
	private Vector3 originscale;
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
	private float[] zscale;
	private float[] ypos;
	private float[] yscale;
	private bool houseatback;
	private string leftorright;
	
	public ProceduralHouse(GameObject House,bool Houseatback,string Leftorright){
		houseatback = Houseatback;
		leftorright = Leftorright;
		empty = new GameObject ("HouseBox"+staticnumber);
		mynumber = staticnumber;
		staticnumber++;
		this.House = House;
		scale = House.transform.localScale;
		originscale = House.transform.localScale;

		pos = House.transform.position;
		rot = House.transform.rotation;

		empty.transform.position = pos;
		omlijsting = Random.Range (1, 5);
	}
	
	
	public void BuildrandomHouse(){
		randomDim ();
		BuildHouse();
		Changepos ();
	}

	public void BuildHouse(){
		Build ();
		empty.transform.rotation = rot;
	}

	private void Build(){	//.............................................................................
		
		//maakt blokje huis aan
		MakeBlok ();
		
		//maakt dak aan7
		MakeRoof ();
		
		//maakt deur aan
		MakeDoor ();
		
		//maakt ramen aan
		MakeWindows ();
		House.transform.tag="Untagged";
		MonoBehaviour.Destroy(House);
		setwindowtexture ();
		
		
	}
	
	private void MakeBlok(){
		Vector3 temp = pos;
		newhouse = MonoBehaviour.Instantiate(Resources.Load ("Prefabs/ProceduralHouse/House2", typeof(GameObject))) as GameObject;
		newhouse.transform.parent = empty.transform;
		temp.y += (float)0.5*scale.y;	//0.5 anders in de grond
		newhouse.transform.position = temp;
		newhouse.transform.localScale = scale;

		newhouse.renderer.material =(Material)Resources.Load ("Materials/"+"House"+"/"+Random.Range (1, 5), typeof(Material));
	}
	
	private void MakeRoof(){
		int roofnr = Random.Range (1, 3);	//nummer of roofs
		Vector3 temp = pos;
		roof = MonoBehaviour.Instantiate(Resources.Load ("Prefabs/ProceduralHouse/Roof"+roofnr, typeof(GameObject))) as GameObject;
		roof.transform.parent = empty.transform;
		temp.y += (float)1*scale.y;		//1 zodat boven het huisblokje
		
		roof.transform.position = temp;

		int a = Random.Range(0,2);
		roof.transform.rotation = Quaternion.Euler(0, 90*a, 0);
		Vector3 tempscale = scale;
		if (a == 1) {
			float tempp=tempscale.x;
			tempscale.x=tempscale.z;
			tempscale.z=(float)tempp;
		}
		tempscale.y = tempscale.y / 2f;
		roof.transform.localScale = tempscale;
	
		setrooftexture ();
	}
	
	private void setrooftexture(){
		int rooftexture = (int)Random.Range (1, 5);
		GameObject[] Windowcolor = GameObject.FindGameObjectsWithTag ("Roof");
		for (int i=0; i<Windowcolor.Length; i++) {
			Windowcolor[i].renderer.material = (Material)Resources.Load ("Materials/"+"Roof"+"/"+rooftexture, typeof(Material));
			Windowcolor[i].tag="Building2";
		}
	}
	
	
	
	public void MakeDoor(){
		door = MonoBehaviour.Instantiate(Resources.Load ("Prefabs/ProceduralHouse/Door", typeof(GameObject))) as GameObject;
		door.transform.parent = empty.transform;
		
		GameObject doorWindow=GameObject.Find ("/HouseBox"+mynumber+"/Door(Clone)/Window");

		Vector3 temp = pos;
		doorzscale = doorWindow.transform.localScale.z;
		dooryscale = doorWindow.transform.localScale.y;
		temp.y += (float)0.5*dooryscale;	//grond niveau
		temp.x += (float)0.5*scale.x;		//rand blokje
		
		temp.z += (float)Random.Range ((float)-0.40 * scale.z, (float)0.40 * scale.z);
		doorypos = temp.y;
		doorzpos = temp.z;
		door.transform.position = temp;

		doorWindow.renderer.material =(Material)Resources.Load ("Materials/"+"Door"+"/"+Random.Range (1, 5), typeof(Material));
		doorWindow.transform.tag = "Building2";

	}
	
	
	private void MakeWindows(){
		
		window = new GameObject[windownr];
		zpos = new float[windownr];
		ypos = new float[windownr];
		xposa = new int[windownr];
		zscale = new float[windownr];
		yscale = new float[windownr];
		windowWindow = new GameObject[windownr];

		for (int i=0; i<windownr; i++) {
			int prefabwindow=(int)Random.Range(1,2);
			window[i] = MonoBehaviour.Instantiate (Resources.Load ("Prefabs/ProceduralHouse/Window"+prefabwindow, typeof(GameObject))) as GameObject;
			windowWindow[i]=GameObject.Find ("/Window"+prefabwindow+"(Clone)/Window");

			zscale[i]=windowWindow[i].transform.localScale.z+0.1f;
			yscale[i]=windowWindow[i].transform.localScale.y+0.1f;
			
			bool validplace = windowsetter(i);
			if(validplace){
				window [i].transform.parent = empty.transform;
				
			}
			//MonoBehaviour.print("valid "+validplace);
			
		}
		
	}
	
	private void setwindowtexture(){
		GameObject[] Omlijstingcolor = GameObject.FindGameObjectsWithTag ("Omlijsting");
		GameObject[] Windowcolor = GameObject.FindGameObjectsWithTag ("Window");
		for (int i=0; i<Omlijstingcolor.Length; i++) {
			Omlijstingcolor[i].renderer.material = (Material)Resources.Load ("Materials/"+"Omlijsting"+"/"+omlijsting, typeof(Material));
			Omlijstingcolor[i].tag="Building2";
		}
		for (int i=0; i<Windowcolor.Length; i++) {
			Windowcolor[i].renderer.material = (Material)Resources.Load ("Materials/"+"Windows"+"/"+windowtexture, typeof(Material));
			Windowcolor[i].tag="Building2";
		}
	}
	
	
	private bool windowsetter(int i){
		int a=0;
		while (a==0) {
			a=Random.Range(-1,2);
		}
		
		bool validplace = true;
		Vector3 temp = pos;
		
		temp.y += 2*(int)Random.Range(Mathf.Floor(0.5f * dooryscale),Mathf.Floor((scale.y)/2.0f)+1)-0.7f;	

		temp.x += (float)0.5 * scale.x*a;		//rand blokje

		//temp.z += (int)Random.Range (Mathf.Floor(-0.220f * scale.z), Mathf.Floor(-0.220f * scale.z)*-1)*2;
		temp.z += (int)Random.Range (Mathf.Floor(-0.199f * scale.z), Mathf.Floor(-0.199f * scale.z)*-1)*2;
		bool build = false;

		temp = sidewindow (temp,a,out build,out validplace,out a);
		if (build) {
			window [i].transform.rotation = Quaternion.Euler (0, 90, 0);
		}
		
		if ((houseatback)&(a==-1)) {
			validplace=false;
		}
		
		if ((a == 1)&(!build)) {
						
			if ((((temp.z + zscale[i]) > doorzpos) && ((temp.z - zscale[i]) < doorzpos))) {
								
				if ((((temp.y + yscale[i]) > doorypos) & ((temp.y - yscale[i]) < doorypos))) {
					validplace = false;
				}
			}
		}
		for (int j=0; j<i; j++) {
						
			if(((xposa[j]==a))&(!build)){

				if(( ((temp.z+0.5f*zscale[i])>zpos[j]-0.5f*zscale[j]) & ((temp.z-0.5f*zscale[i])<zpos[j]+0.5f*zscale[j]) )){

					if(( ((temp.y+0.5f*yscale[i])>ypos[j]-0.5f*yscale[j])&((temp.y-0.5f*yscale[i])<ypos[j]+0.5f*yscale[j]) )){
						validplace=false;
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
	
	private Vector3 sidewindow (Vector3 temp,int b,out bool build,out bool validplace,out int a){
		validplace=true;
		build = false;
		a = b;
		
		if((leftorright.Equals("l"))&&(Random.Range (0, 4)==2)){
			build=true;
			temp.z = pos.z + (float)0.5 * scale.z*-1;		//rand blokje

			temp.x =pos.x + (int)Random.Range (Mathf.Floor(-0.220f * scale.x), Mathf.Floor(-0.220f * scale.x)*-1)*2;
			a=3;
		}
		else if((leftorright.Equals("r"))&&(Random.Range (0, 4)==2)){
			build=true;
			temp.z = pos.z + (float)0.5 * scale.z*1;		//rand blokje

			temp.x = pos.x + (int)Random.Range (Mathf.Floor(-0.220f * scale.x), Mathf.Floor(-0.220f * scale.x)*-1)*2;
			a=3;
		}
		return temp;
	}
	
	
	
	
	
	private void randomDim(){
		int n = 5;
		xscale=(float)Random.Range (1,n
		                            );

		float x = (float)scale.x - xscale;
		
		float y = (float)scale.y + (float)Random.Range (-4, 6);
		float z = (float)scale.z; 

		House.transform.localScale = new Vector3(x,y,z);
		scale = new Vector3 (x, y, z);
	}

	
	private void Changepos(){

		int rotater = 1;
		if (rot.eulerAngles.y > 179) {
			rotater=-1;
		}
		if ( ((rot.eulerAngles.y > 0f)&(rot.eulerAngles.y<179f)) | ((rot.eulerAngles.y > 181f)&(rot.eulerAngles.y<360f)) ) {
			pos.z +=0.5f*xscale*rotater;
		} else {
			pos.x -=0.5f*xscale*rotater;
		}
		empty.transform.position = pos;
	}
}
