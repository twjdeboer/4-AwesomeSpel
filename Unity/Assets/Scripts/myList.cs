using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class myList : List<GameObject[]> {

    public List<GameObject[]> list = new List<GameObject[]>();

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

    public List<GameObject> getColom(int colomNumber)
    {
        List<GameObject> colom = new List<GameObject>();
        for(int i = 0; i < list.Count ; i++)
        {
            colom.Add(this.list[i][colomNumber]);
        }
        return colom;
    }



}
