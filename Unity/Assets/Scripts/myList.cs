using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/**
 * Special list class extending List for extra methods. This list has to colums: first colum is a node and the second colum is its parent.
 * */
public class myList : List<GameObject[]> {

    //Attributes.
    public List<GameObject[]> list = new List<GameObject[]>();

    //Methods

    /**
     * Checks if a certain dimension of the list contains the indidacted object.
     * */
    public bool DimensionContains(int dimension, GameObject item)
    {
        for (int i = 0; i < list.Count; i++)
        {
            if (this.list[i][dimension].GetInstanceID() == item.GetInstanceID())
            {
                return true;
            }
        }

        return false;
    }

    /**
     * Get Parentnode of the node.
     * */
    public GameObject GetParentNode(GameObject node)
    {
        for (int i = 0; i < this.list.Count; i++)
        {
            if (this.list[i][0].GetInstanceID() == node.GetInstanceID())
            {
                return this.list[i][1];
            }
        }

        return null;
    }

    /**
     * Get the index of the indicated object in a certain colum.
     * */
    public int indexOfDim(int dim, GameObject node)
    {
        for (int i = 0; i < this.list.Count; i++)
        {
            if (this.list[i][dim].GetInstanceID() == node.GetInstanceID())
            {
                return i;
            }
        }
        return -1;
    }




}
