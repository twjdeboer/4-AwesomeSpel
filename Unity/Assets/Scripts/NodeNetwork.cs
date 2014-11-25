using UnityEngine;
using System.Collections;

public class NodeNetwork {

    private Vector3 pos;
    private Vector2 numberOfNodes;
    private GameObject[,] network;
    public GameObject Nodes = new GameObject("Nodes");

    public NodeNetwork(Vector3 pos, Vector2 numberOfNodes)
    {
        
        this.pos = pos;
        this.network = new GameObject[(int)numberOfNodes.x, (int)numberOfNodes.y];
        this.numberOfNodes = numberOfNodes;

        float xPos = pos.x - (numberOfNodes.x * 0.5f);
        float zPos = pos.z - (numberOfNodes.y * 0.5f);
        for (int i = 0; i < (int)numberOfNodes.x; i++)
        {
            
            for (int j = 0; j < (int)numberOfNodes.y; j++)
            {               
                network[i, j] = node(xPos,zPos, i, j);
                zPos++;                
            }
            xPos++;
            zPos = -numberOfNodes.y * 0.5f;

        }
    }

    GameObject node(float xPos, float zPos, int i, int j)
    {
        GameObject primitive = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        primitive.transform.position = new Vector3(xPos, 0, zPos);
        primitive.collider.isTrigger = true;
        primitive.gameObject.AddComponent("Rigidbody");
        primitive.rigidbody.useGravity = false;
        primitive.rigidbody.isKinematic = true;
        primitive.rigidbody.constraints = RigidbodyConstraints.FreezeAll;
        primitive.transform.localScale = new Vector3(0.75f, 0.75f, 0.75f);
        primitive.transform.parent = Nodes.transform;
        primitive.gameObject.name = "Node(" + i + "," + j + ")";
        primitive.AddComponent("Node");
        primitive.GetComponent<Node>().xPos = i;
        primitive.GetComponent<Node>().yPos = j;
        primitive.renderer.material = (Material)Resources.Load("Textures/Invisible");
        return primitive;
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

    public GameObject getNode(int i, int j)
    {
        return network[i, j];
    }

    /**
     * Converts a Vector2 to a node.
     * */
    public GameObject Vector2Node(Vector2 vector)
    {
        return this.network[(int)vector.x, (int)vector.y];
    }

    public static Vector2 Node2Vector(GameObject node)
    {
        return new Vector2(node.GetComponent<Node>().xPos, node.GetComponent<Node>().yPos);
    }

}
