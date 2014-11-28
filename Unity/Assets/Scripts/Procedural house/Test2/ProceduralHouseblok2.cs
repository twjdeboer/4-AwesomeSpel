using UnityEngine;
using System.Collections;

public class ProceduralHouseblok2{
	
	private GameObject houseblok;
	private Vector3 scale;
	private Vector3 pos;
	private Quaternion rot;
	private GameObject newlefthouse;
	private GameObject newrighthouse;
	private bool rotation;
	private bool blockback;
	
	
	public ProceduralHouseblok2(GameObject Houseblok){
		this.houseblok = Houseblok;
		scale = houseblok.transform.localScale;
		pos = houseblok.transform.position;
		rot = houseblok.transform.rotation;
		if ( ((rot.eulerAngles.y > 0f)&(rot.eulerAngles.y<180f)) | ((rot.eulerAngles.y > 180f)&(rot.eulerAngles.y<360f)) ) {
			rotation = true;
		} else {
			rotation = false;
		}
		//MonoBehaviour.print (rotation);
		
	}
	
	public bool devidefirst(bool back){	//addtag /back/ empty
		bool create = false;
		string addtag = "";
		if (back) {
			addtag="back";
		}
		if (scale.z > 18 ){
			Vector3 stemp1 = scale;
			Vector3 stemp2 = scale;
			Vector3 ptemp1 = pos;
			Vector3 ptemp2 = pos;
			int rand = (int)Random.Range(4,8);
			newlefthouse = MonoBehaviour.Instantiate(Resources.Load ("Prefabs/House", typeof(GameObject))) as GameObject;
			newrighthouse = MonoBehaviour.Instantiate(Resources.Load ("Prefabs/House", typeof(GameObject))) as GameObject;
			newlefthouse.transform.tag="Prebuildingl"+addtag;
			newrighthouse.transform.tag="Prebuildingr"+addtag;


			stemp1.z = (float)rand;
			stemp2.z = (float)rand;
			if(rotation==false){
				ptemp1.z = pos.z + (scale.z/2f - (float)rand/2f)*-1;
				ptemp2.z = pos.z + (scale.z/2f - (float)rand/2f);
			}else{
				ptemp1.x = pos.x + (scale.z/2f - (float)rand/2f)*-1;
				ptemp2.x = pos.x + (scale.z/2f - (float)rand/2f);	
			}
			scale.z-=(float)rand*2;
			//MonoBehaviour.print(scale.z+" "+stemp1.z+" "+stemp2.z);
			//MonoBehaviour.print(pos.z+" "+ptemp1.z+" "+ptemp2.z);
			newlefthouse.transform.localScale = stemp1;
			newlefthouse.transform.position = ptemp1;
			newlefthouse.transform.rotation = rot;
			newrighthouse.transform.localScale = stemp2;
			newrighthouse.transform.position = ptemp2;
			newrighthouse.transform.rotation = rot;
			houseblok.transform.localScale=scale;
			create =true;
		}
		else{
			houseblok.transform.tag="Prebuilding"+addtag;
			create=false;
		}
		return create;
	}
	
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
			//MonoBehaviour.print (rand);
			//newlefthouse = new GameObject ();
			//newrighthouse = new GameObject ();
			newlefthouse = MonoBehaviour.Instantiate(Resources.Load ("Prefabs/House", typeof(GameObject))) as GameObject;
			newrighthouse = MonoBehaviour.Instantiate(Resources.Load ("Prefabs/House", typeof(GameObject))) as GameObject;
			
			
			stemp1.z = scale.z / 2.0f + (float)rand;
			stemp2.z = scale.z / 2.0f - (float)rand;
			if(rotation==false){
				ptemp1.z = pos.z + (scale.z/4f - (float)rand/2f)*-1;
				ptemp2.z = pos.z + (scale.z/4f + (float)rand/2f);
			}else{
				ptemp1.x = pos.x + (scale.z/4f - (float)rand/2f)*-1;
				ptemp2.x = pos.x + (scale.z/4f + (float)rand/2f);	
			}

			
			//MonoBehaviour.print(scale.z+" "+stemp1.z+" "+stemp2.z);
			//MonoBehaviour.print(pos.z+" "+ptemp1.z+" "+ptemp2.z);
			newlefthouse.transform.localScale = stemp1;
			newlefthouse.transform.position = ptemp1;
			newlefthouse.transform.rotation = rot;
			newrighthouse.transform.localScale = stemp2;
			newrighthouse.transform.position = ptemp2;
			newrighthouse.transform.rotation = rot;
			houseblok.transform.localScale = scale;
			create =true;
			//MonoBehaviour.Destroy(houseblok);
			
		}
		else{
			houseblok.transform.tag=settag;
			create=false;
		}
		return create;
		
	}
}
