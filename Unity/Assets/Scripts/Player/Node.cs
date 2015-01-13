using UnityEngine;
using System.Collections;

public class Node : MonoBehaviour{

    public bool accesable = false;
    public int xPos;
    public int yPos;
    public GameObject[] parentNode;
    public int[] G;
    public int[] H;
    public int[] F;


    void Start()
    {
        G = new int[ResourceManager.numberOfChilds];
        H = new int[ResourceManager.numberOfChilds];
        F = new int[ResourceManager.numberOfChilds];
        parentNode = new GameObject[ResourceManager.numberOfChilds];
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag.Contains("Walk"))
        {
            this.accesable = true;
        }
        if(!other.gameObject.tag.Contains("Walk"))
        {
            accesable = false;
        }
    }

}
