using UnityEngine;
using System.Collections;

public class CreateNodeNetwork : MonoBehaviour {

    public NodeNetwork nodes;

    public Vector3 pos;
    public Vector2 numberOfNodes;

	// Use this for initialization
	void Start () {

        nodes = new NodeNetwork(pos, numberOfNodes);      
        ResourceManager.numberOfChilds = GameObject.Find("NPCs").transform.childCount;
        NPCNumbering();
	}

    void NPCNumbering()
    {
        for(int i = 0; i < ResourceManager.numberOfChilds; i++)
        {
            GameObject.Find("NPCs").transform.GetChild(i).gameObject.GetComponent<Astar>().NPCNumber = i;
        }
    }

    void Update()
    {
        if(!nodes.Equals(null))
        {
            ResourceManager.networkReady = true;
        }
    }


}
