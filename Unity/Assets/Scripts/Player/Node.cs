using UnityEngine;
using System.Collections;

public class Node : MonoBehaviour{

    public bool accesable = true;
    public int xPos;
    public int yPos;
    public GameObject[] parentNode;
    public int[] G;
    public int[] H;
    public int[] F;
 


    void Start()
    {
        accesable = true;
        G = new int[ResourceManager.numberOfChilds];
        H = new int[ResourceManager.numberOfChilds];
        F = new int[ResourceManager.numberOfChilds];
        parentNode = new GameObject[ResourceManager.numberOfChilds];
    }

    void OnTriggerStay(Collider other)
    {
        if(!other.gameObject.tag.Contains("Walk"))
        {
            accesable = false;
        }
    }

    void OnTriggerExit(Collider other)
    {
        accesable = true;
    }

}
