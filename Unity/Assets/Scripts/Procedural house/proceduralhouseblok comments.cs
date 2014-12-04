using UnityEngine;
using System.Collections;

public class proceduralhouseblokcomments {

	private GameObject houseblok;
	private Vector3 scale;
	private Vector3 pos;
	private Quaternion rot;
	private GameObject newlefthouse;
	private GameObject newrighthouse;
	private bool rotation;
	private bool blockback;

	/**
	 * sets object to gameobjects scale pos rot
	 * 
	 **/
	public proceduralhouseblokcomments(GameObject Houseblok){
		this.houseblok = Houseblok;
		scale = houseblok.transform.localScale;
		pos = houseblok.transform.position;
		rot = houseblok.transform.rotation;
		if ( ((rot.eulerAngles.y > 0f)&(rot.eulerAngles.y<179f)) || ((rot.eulerAngles.y > 181f)&(rot.eulerAngles.y<359f)) ) {	//if 90 or 270 degrees
			rotation = true;
		} else {
			rotation = false;
		}		
	}

	/**
	 * first divide of a house block
	 * 
	 * sets outer houses
	 * 
	 * @param bool back (if house at back)
	 * @return bool (if it devided)
	 **/
	public bool devidefirst(bool back){	
		bool create = false;
		string addtag = "";
		if (back) {
			addtag="back";	
		}
		if (scale.z > 18 ){	//only happens if scale is big enough
			Vector3 stemp1 = scale;
			Vector3 stemp2 = scale;
			Vector3 ptemp1 = pos;
			Vector3 ptemp2 = pos;
			int rand = (int)Random.Range(4,8);
			newlefthouse = MonoBehaviour.Instantiate(Resources.Load ("Prefabs/House", typeof(GameObject))) as GameObject;
			newrighthouse = MonoBehaviour.Instantiate(Resources.Load ("Prefabs/House", typeof(GameObject))) as GameObject;
			if(rot.eulerAngles.y<179){	//change outer building tags if rotated more than 180 degr.
				newlefthouse.transform.tag="Prebuildingl"+addtag;
				newrighthouse.transform.tag="Prebuildingr"+addtag;
			}else if(rot.eulerAngles.y>179){
				newlefthouse.transform.tag="Prebuildingr"+addtag;
				newrighthouse.transform.tag="Prebuildingl"+addtag;
			}
			
			stemp1.z = (float)rand;
			stemp2.z = (float)rand;
			if(rotation==false){	//change position if rotated 90 or 180 degr.
				ptemp1.z = pos.z + (scale.z/2f - (float)rand/2f)*-1;
				ptemp2.z = pos.z + (scale.z/2f - (float)rand/2f);
			}else{
				ptemp1.x = pos.x + (scale.z/2f - (float)rand/2f)*-1;
				ptemp2.x = pos.x + (scale.z/2f - (float)rand/2f);	
				MonoBehaviour.print("rotatie");
			}
			scale.z-=(float)rand*2;
			
			newlefthouse.transform.localScale = stemp1;
			newlefthouse.transform.position = ptemp1;
			newlefthouse.transform.rotation = rot;
			newrighthouse.transform.localScale = stemp2;
			newrighthouse.transform.position = ptemp2;
			newrighthouse.transform.rotation = rot;
			houseblok.transform.localScale=scale;
			create =true;	//set creat true
		}
		else{
			houseblok.transform.tag="Prebuilding"+addtag;	//house block to small to devide so make it a house
			create=false;
		}
		return create;
	}

	/*
	 * devides a cube into to cubes
	 * 
	 * @param string settag (tag what kind of house going to be)
	 * @return bool (if it devided)
	 * 
	 */ 
	public bool devide(string settag){
		bool create = false;
		if(scale.z>12){
			Vector3 stemp1 = scale;
			Vector3 stemp2 = scale;
			Vector3 ptemp1 = pos;
			Vector3 ptemp2 = pos;
			
			int rand2 = (int)Random.Range (-scale.z/2f,scale.z/2f);
			int rand = (int)Random.Range(0,Mathf.Floor(scale.z/6f));
			int rand3 = (int)Random.Range(0,3);
			
			newlefthouse = MonoBehaviour.Instantiate(Resources.Load ("Prefabs/House", typeof(GameObject))) as GameObject;	
			newrighthouse = MonoBehaviour.Instantiate(Resources.Load ("Prefabs/House", typeof(GameObject))) as GameObject;
			
			
			stemp1.z = scale.z / 2.0f + (float)rand;
			stemp2.z = scale.z / 2.0f - (float)rand;
			if(rotation==false){		//change pos if rotated
				ptemp1.z = pos.z + (scale.z/4f - (float)rand/2f)*-1;
				ptemp2.z = pos.z + (scale.z/4f + (float)rand/2f);
			}else{
				ptemp1.x = pos.x + (scale.z/4f - (float)rand/2f)*-1;
				ptemp2.x = pos.x + (scale.z/4f + (float)rand/2f);	
			}
			
			newlefthouse.transform.localScale = stemp1;
			newlefthouse.transform.position = ptemp1;
			newlefthouse.transform.rotation = rot;
			newrighthouse.transform.localScale = stemp2;
			newrighthouse.transform.position = ptemp2;
			newrighthouse.transform.rotation = rot;
			houseblok.transform.localScale = scale;
			create =true;
			
		}
		else{
			houseblok.transform.tag=settag;	//if object to small to devide make house.
			create=false;
		}
		return create;
		
	}
}