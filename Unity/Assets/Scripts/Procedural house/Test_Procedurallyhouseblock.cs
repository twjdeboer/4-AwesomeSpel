using UnityEngine;
using System.Collections;
	using System.Collections.Generic;


public class Test_Procedurallyhouseblock : MonoBehaviour {
		public GameObject world;
		private GameObject[] Houseblok;
		private ProceduralHouseblok newHouseBlok;
		private GameObject devidehouse;
		
		private ProceduralHouse[] newHouse;
		public GameObject[] Houses;
		
		
		void Start() {
			Houseblok = GameObject.FindGameObjectsWithTag ("Huisblok");
			//print (Houseblok.Length);
			for(int j=0;j<Houseblok.Length;j++){
				
				newHouseBlok = new ProceduralHouseblok (Houseblok[j]);
				
				if(newHouseBlok.devide ()){
					Destroy (Houseblok[j]);
				}
				devidehouse = GameObject.FindGameObjectWithTag ("Huisblok2");
				int i = 0;
				
				
				
				while (devidehouse!=null) {
					newHouseBlok = new ProceduralHouseblok (devidehouse);
					if(newHouseBlok.devide ()){
						devidehouse.transform.tag = "Huisblok";
						Destroy (devidehouse);
					}
					
					devidehouse = GameObject.FindGameObjectWithTag ("Huisblok2");
					i++;
					if (i > 100) {
						break;
					}
					// ("Done " + i);
				}
				
				
			}
			
			Houses = GameObject.FindGameObjectsWithTag ("Prebuilding");
			
			
			newHouse = new ProceduralHouse[Houses.Length];
			
			for (int i=0; i < Houses.Length; i++) {
				newHouse[i]= new ProceduralHouse(Houses[i]);
			}
			for (int i=0; i < Houses.Length; i++) {
				newHouse[i].BuildrandomHouse();
				//newHouse[i].BuildrandomHouse();
				newHouse[i].empty.transform.parent =world.transform;
			}
			
			
			
		}
		
		
		
		
	}