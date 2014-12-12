using UnityEngine;
using System.Collections;

public class carAgent : MonoBehaviour {

	public Transform destination;

	private NavMeshAgent agent;

	// Use this for initialization
	void Start () {
		agent = gameObject.GetComponent<NavMeshAgent> ();

		agent.SetDestination (destination.position);
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
