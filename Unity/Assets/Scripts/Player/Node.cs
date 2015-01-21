using UnityEngine;
using System.Collections;

/*
 * Class represents a node used in the A* pathfinding algoritihm. The node keeps track of the cost for each NPC.
 * */
public class Node : MonoBehaviour{

    public bool accesable = true;
    public int xPos;
    public int yPos;
    public GameObject[] parentNode;
    public int[] G;
    public int[] H;
    public int[] F;
 

    //Initializes node
    void Start()
    {
        accesable = true;
        G = new int[ResourceManager.numberOfChilds];
        H = new int[ResourceManager.numberOfChilds];
        F = new int[ResourceManager.numberOfChilds];
        parentNode = new GameObject[ResourceManager.numberOfChilds];
    }

    //Checks if node is accessible
    void OnTriggerStay(Collider other)
    {
        if(!other.gameObject.tag.Contains("Walk"))
        {
            accesable = false;
        }
    }

    //Checks if node is accessible
    void OnTriggerExit(Collider other)
    {
        accesable = true;
    }

}
