using UnityEngine;
using System.Collections;

public class carAgent : MonoBehaviour {

	public Transform destination;

	private NavMeshAgent agent;

	// Use this for initialization
	void Start () {
		agent = gameObject.GetComponent<NavMeshAgent> ();

		transform.eulerAngles = new Vector3(270.0f, 90.0f, 0.0f);

		agent.SetDestination (destination.position);
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
