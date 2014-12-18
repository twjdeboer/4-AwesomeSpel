using UnityEngine;
using System.Collections;
using System;

public class carAgent : MonoBehaviour {

	public Transform destination;

	private NavMeshAgent agent;
	private NavMeshPath path;

	// Use this for initialization
	void Start () {
		agent = gameObject.GetComponent<NavMeshAgent> ();

		agent.SetDestination (destination.position);

		print (agent.GetLayerCost (11));
		print (agent.GetLayerCost (12));

		print ("en nu");

		System.Random random = new System.Random ();
		print(random.Next(0, 6));
		print(random.Next(0, 6));
		print(random.Next(0, 6));
		print(random.Next(0, 6));
		print(random.Next(0, 6));
		print(random.Next(0, 6));
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	Vector3 pickDestination(){
		int[] xCord = {-285, -240, -150, 0, 180, 225, -180, -150, 0, 120, 180, -285, -240, -180, 120, 180, 225, -240, -180, -90, 0, 120, 180, -240, -90, 0, 180, -240, 0};
		int[] zCord = {-150, -150, -150, -150, -150, -150, -90, -90, -90, -90, -90, 0, 0, 0, 0, 0, 0, 90, 90, 90, 90, 90, 90, 150, 150, 150, 150, 195, 195};

		System.Random random = new System.Random ();

		int index = (random.Next(0, 29));

		int x = 0;	//(int) xCord.GetValue(index);
		int y = 0;
		int z = 150;	//(int) zCord.GetValue(index);

		Vector3 newDestination = new Vector3 (x, y, z);

		return newDestination;
	}



	void OnTriggerEnter(Collider other){

		print (other.gameObject.name);

		for (int i = 1; i < 49; i++){

			String crossing = Convert.ToString (i);

			if (other.gameObject.name == crossing){


				// check x and y number of cross road
				double temp = i/8;
				int y = (int)(Math.Floor(temp+1));
				int x = (int)(i%8);

				print (x);
				print (y);
		
				// set cost horizontal roads 
				for (int n = 1; n < 8; n++){
					if (n < x){
						agent.SetLayerCost(n*4, 100);
						agent.SetLayerCost(n*4 - 1, 1);
					}
					if (n >= x){
						agent.SetLayerCost(n*4, 1);
						agent.SetLayerCost(n*4 - 1, 100);
					}
				}

				// set cost vertical roads
				for (int n = 1; n < 6; n++){
					if (n <= y){
						agent.SetLayerCost(n*4 + 2, 50);
						agent.SetLayerCost(n*4 + 1, 1);
					}
					if (n > y){
						agent.SetLayerCost(n*4 + 2, 1);
						agent.SetLayerCost(n*4 + 1, 50);
					}
				}
			}
		}

		// recalculate the best path on entering crossroad
		destination.position = pickDestination();
		agent.SetDestination (destination.position);

		print (agent.GetLayerCost (11));
		print (agent.GetLayerCost (12));

	}

}