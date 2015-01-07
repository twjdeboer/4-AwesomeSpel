using UnityEngine;
using System.Collections;

public class NodeNetwork {
    private Vector3 pos;
    private Vector2 numberOfNodes;
    private GameObject[,] network;
 
    /**
     * Create a network of gameObjects with a node-component.
     * */
    public NodeNetwork(Vector3 pos, Vector2 numberOfNodes)
    {
        this.pos = pos;
        this.network = new GameObject[(int)numberOfNodes.x, (int)numberOfNodes.y];
        this.numberOfNodes = numberOfNodes;

        float xPos = pos.x - (numberOfNodes.x * 1.5f);
        float zPos = pos.z - (numberOfNodes.y * 1.5f);
        for (int i = 0; i < (int)numberOfNodes.x; i++)
        {
            
            for (int j = 0; j < (int)numberOfNodes.y; j++)
            {               
                network[i, j] = node(xPos,zPos, i, j);
                zPos+=3;                
            }
            xPos+=3;
			zPos = pos.z - numberOfNodes.y * 1.5f;

        }
    }

    /**
     * Method for initializing the node.
     * */
    GameObject node(float xPos, float zPos, int i, int j)
    {
		GameObject primitive = MonoBehaviour.Instantiate(Resources.Load ("Prefabs/Node", typeof(GameObject))) as GameObject;
		primitive.transform.position = new Vector3(xPos, 0, zPos);
		primitive.transform.localScale = new Vector3(0.75f, 0.75f, 0.75f);
		primitive.transform.parent = GameObject.Find("NodeNetwork").transform;
		primitive.gameObject.name = "Node(" + i + "," + j + ")";
		primitive.GetComponent<Node>().xPos = i;
		primitive.GetComponent<Node>().yPos = j;
		return primitive;
		/*
        GameObject primitive = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        primitive.transform.position = new Vector3(xPos, 0, zPos);
        primitive.collider.isTrigger = true;
        primitive.gameObject.AddComponent("Rigidbody");
        primitive.rigidbody.useGravity = false;
        primitive.rigidbody.isKinematic = true;
        primitive.rigidbody.constraints = RigidbodyConstraints.FreezeAll;
        primitive.transform.localScale = new Vector3(0.75f, 0.75f, 0.75f);
        primitive.transform.parent = GameObject.Find("NodeNetwork").transform;
        primitive.gameObject.name = "Node(" + i + "," + j + ")";
        primitive.AddComponent("Node");
        primitive.GetComponent<Node>().xPos = i;
        primitive.GetComponent<Node>().yPos = j;
        primitive.renderer.material = (Material)Resources.Load("Textures/Invisible");
        return primitive;
        */
    }

    /**
     * Find closest node to given position.
     * */
    public GameObject Closest(Vector3 pos)
    {
        Vector2 bestNode = new Vector2(0, 0);
        float bestDistance = 9999999999999999999;

         for (int i = 0; i < (int)numberOfNodes.x; i++)
        {
            for (int j = 0; j < (int)numberOfNodes.y; j++)
            {
                if (Vector3.Distance(network[i, j].transform.position, pos) < bestDistance)
                {
                    bestDistance = Vector3.Distance(network[i, j].transform.position, pos);
                    bestNode = new Vector2(i, j);
                }
            }
        }
         return Vector2Node(bestNode);
    }

    /**
     * Return node at the given index.
     * */
    public GameObject getNode(int i, int j)
    {
        return network[i, j];
    }

    /**
     * Converts a Vector2 node notation to a node GameObject.
     * */
    public GameObject Vector2Node(Vector2 vector)
    {
        return this.network[(int)vector.x, (int)vector.y];
    }


    /**
     * Converts node GameObject to its vector2 notation.
     * */
    public static Vector2 Node2Vector(GameObject node)
    {
        return new Vector2(node.GetComponent<Node>().xPos, node.GetComponent<Node>().yPos);
    }

    /**
     * Checks whether or the given position is in the NodeNetwork.
     * */
    public bool IsPosInNetwork(Vector3 pos)
    {
        float xMin = network[0, 0].transform.position.x;
        float xMax = network[(int)numberOfNodes.x-1, 0].transform.position.x;
        float yMin = network[0, 0].transform.position.y;
        float yMax = network[0, (int)numberOfNodes.y-1].transform.position.y;

        return pos.x > xMin && pos.x < xMax && pos.z > yMin && pos.z < yMax;
    }

    /**
     * Returns range of the network in a Vector4(XMIN, XMAX, YMIN, YMAX) notation.
     * */
    public Vector4 NetworkRange()
    {
        float xMin = network[0, 0].transform.position.x;
        float xMax = network[(int)numberOfNodes.x-1, 0].transform.position.x;
        float yMin = network[0, 0].transform.position.z;
        float yMax = network[0, (int)numberOfNodes.y-1].transform.position.z;

        return new Vector4(xMin, xMax, yMin, yMax);
    }

    /**
     * Checks if indicated index is in the node network.
     * */
    public bool IsNodeInNetwork(int i, int j)
    {
        return i >= 0 && i < numberOfNodes.x && j >= 0 && j < numberOfNodes.y;
    }

    /**
     * Set the costs of a certain NPC to zero.
     * **/
    public void ClearCosts(int NPCNumber)
    {
        for(int i = 0; i< numberOfNodes.x; i++)
        {
            for(int j = 0; j< numberOfNodes.y; j++)
            {
                network[i, j].GetComponent<Node>().G[NPCNumber] = 0;
                network[i, j].GetComponent<Node>().H[NPCNumber] = 0;
                network[i, j].GetComponent<Node>().F[NPCNumber] = 0;
                network[i, j].GetComponent<Node>().parentNode[NPCNumber] = null;
            }
        }
    }
}
