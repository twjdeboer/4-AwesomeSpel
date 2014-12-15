using UnityEngine;
using System.Collections;
using System;

public class carAgent : MonoBehaviour {

	public Transform destination;

	private NavMeshAgent agent;

	// Use this for initialization
	void Start () {
		agent = gameObject.GetComponent<NavMeshAgent> ();

		agent.SetDestination (destination.position);

		print (agent.GetLayerCost (13));
		print (agent.GetLayerCost (12));
		print (agent.GetLayerCost (17));
		print (agent.GetLayerCost (16));
		print (agent.GetLayerCost (11));
		print (agent.GetLayerCost (10));
		print ("en nu");
	}
	
	// Update is called once per frame
	void Update () {
	
	}



	void OnTriggerEnter(Collider other){

		print (other.gameObject.name);

		for (int i = 1; i < 49; i++){

			String crossing = Convert.ToString (i);

			if (other.gameObject.name == crossing){


				// check x and y number of cross road
				double temp = i/8;
				int y = (int)(Math.Floor(temp));
				int x = (int)(i%8);
		
				// set cost horizontal roads 
				for (int n = 1; n < 8; n++){
					if (n < x){
						agent.SetLayerCost(n*4, 10);
						agent.SetLayerCost(n*4 - 1, 1);
					}
					if (n >= x){
						agent.SetLayerCost(n*4, 1);
						agent.SetLayerCost(n*4 - 1, 10);
					}
				}

				// set cost vertical roads
				for (int n = 1; n < 6; n++){
					if (n < y){
						agent.SetLayerCost(n*4 + 2, 10);
						agent.SetLayerCost(n*4 + 1, 1);
					}
					if (n >= y){
						agent.SetLayerCost(n*4 + 2, 1);
						agent.SetLayerCost(n*4 + 1, 10);
					}
				}
			}
		}
		print (agent.GetLayerCost (13));
		print (agent.GetLayerCost (14));

	}
}