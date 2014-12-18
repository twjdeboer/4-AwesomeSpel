using UnityEngine;
using System.Collections;
using System;

public class carAgent : MonoBehaviour {

	private Vector3 destination;

	private NavMeshAgent agent;
	private NavMeshPath path;

	// Use this for initialization
	void Start () {
		agent = gameObject.GetComponent<NavMeshAgent> ();

		destination = pickDestination ();
		agent.SetDestination (destination);
	}
	
	// pick a new destination if the destination picked is too close to the position of the agent
	void Update () {
		if (agent.remainingDistance < 10){
			destination = pickDestination();

			print (agent.remainingDistance);
		}

	}

	// pick new destination (destinations are always on crossroads)
	Vector3 pickDestination(){
		int[] xCord = {-285, -240, -150, 0, 180, 225, -180, -150, 0, 120, 180, -285, -240, -180, 120, 180, 225, -240, -180, -90, 0, 120, 180, -240, -90, 0, 180, -240, 0};
		int[] zCord = {-150, -150, -150, -150, -150, -150, -90, -90, -90, -90, -90, 0, 0, 0, 0, 0, 0, 90, 90, 90, 90, 90, 90, 150, 150, 150, 150, 195, 195};

		System.Random random = new System.Random ();

		int index = (random.Next(0, 29));

		int x = (int) xCord.GetValue(index);
		int y = 0;
		int z = (int) zCord.GetValue(index);

		Vector3 newDestination = new Vector3 (x, y, z);

		return newDestination;
	}


	// Set Layercost of all roads correct according to position of agent
	void OnTriggerEnter(Collider other){

		for (int i = 1; i < 49; i++){

			String crossing = Convert.ToString (i);

			if (other.gameObject.name == crossing){


				// check x and y number of cross road
				double temp = i/8;
				int y = (int)(Math.Floor(temp+1));
				int x = (int)(i%8);
		
				// set cost horizontal roads 
				for (int n = 1; n < 8; n++){
					if (n < x){
						agent.SetLayerCost(n*4, 200);
						agent.SetLayerCost(n*4 - 1, 1);
					}
					if (n >= x){
						agent.SetLayerCost(n*4, 1);
						agent.SetLayerCost(n*4 - 1, 200);
					}
				}

				// set cost vertical roads
				for (int n = 1; n < 6; n++){
					if (n < y){
						agent.SetLayerCost(n*4 + 2, 200);
						agent.SetLayerCost(n*4 + 1, 1);
					}
					if (n >= y){
						agent.SetLayerCost(n*4 + 2, 1);
						agent.SetLayerCost(n*4 + 1, 200);
					}
				}
			}
		}
	}


	// set layercost high on road where the car was coming from so it will not turn around
	void OnCollisionExit(Collision CollisionInfo){

		for (int i = 1; i < 8; i++){
			if (CollisionInfo.transform.name == i + "-horiz-left"){
				agent.SetLayerCost (i*4, 500);
				agent.SetLayerCost (i*4 - 1, 2000);
			}
		}

		for (int i = 1; i < 8; i++){
			if (CollisionInfo.transform.name == i + "-horiz-right"){
				agent.SetLayerCost (i*4, 2000);
				agent.SetLayerCost (i*4 - 1, 500);
			}
		}

		for (int i = 1; i < 6; i++){
			if (CollisionInfo.transform.name == i + "-vert-left"){
				agent.SetLayerCost (i*4 + 2, 500);
				agent.SetLayerCost (i*4 + 1, 2000);
			}
		}

		for (int i = 1; i < 6; i++){
			if (CollisionInfo.transform.name == i + "-vert-right"){
				agent.SetLayerCost (i*4 + 2, 2000);
				agent.SetLayerCost (i*4 + 1, 500);
			}
		}
		// recalculate the best path to destination on entering crossroad
		agent.SetDestination (destination);
		print (destination);
	}
}