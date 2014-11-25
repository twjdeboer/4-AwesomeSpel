using UnityEngine;
using System.Collections;

public class Node : MonoBehaviour{

    public bool accesable = true;
    public int xPos;
    public int yPos;
    public GameObject parentNode;
    public int G;
    public int H;
    public int F;
    public bool startNode = false;
    public bool endNode = false;


    void OnTriggerEnter(Collider other)
    {
        if(!other.gameObject.tag.Contains("Walk"))
        {
            this.accesable = false;

        }
    }




}
