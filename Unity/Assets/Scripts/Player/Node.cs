using UnityEngine;
using System.Collections;

public class Node : MonoBehaviour{

    public bool accesable;
    public int xPos;
    public int yPos;
    public GameObject[] parentNode;
    public int[] G;
    public int[] H;
    public int[] F;


    void Start()
    {
        accesable = false;
        G = new int[ResourceManager.numberOfChilds];
        H = new int[ResourceManager.numberOfChilds];
        F = new int[ResourceManager.numberOfChilds];
        parentNode = new GameObject[ResourceManager.numberOfChilds];
    }

    void OnTriggerStay(Collider other)
    {

        if(other.gameObject.tag.Contains("Walk"))
        {
            this.accesable = true;
        }
        else if(!other.gameObject.tag.Contains("Walk"))
        {
            accesable = false;
        }
    }

}
