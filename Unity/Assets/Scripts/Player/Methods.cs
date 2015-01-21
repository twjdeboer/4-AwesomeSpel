using UnityEngine;
using System.Collections;
using System.Collections.Generic;


/*
 * Class where methods for different purposes can be stored
 * */
public static class Methods {



    /**
     * Checks whether a object is in a certain radius of another object.
     * */
    public static bool ReachedPosWithBuffer(Vector3 pos, Vector3 targetPos, Vector3 bufferRadius)
   {
       return (pos.x < targetPos.x + bufferRadius.x && pos.x > targetPos.x - bufferRadius.x
           && pos.y < targetPos.y + bufferRadius.y && pos.y > targetPos.y - bufferRadius.y
           && pos.z < targetPos.z + bufferRadius.z && pos.z > targetPos.z - bufferRadius.z);

   }

    /**
     * Creates a string out a list
     * */
    public static string MakeStringOutArray(List<string> stringArray)
    {
        string res = "";

        for(int i = 0; i < stringArray.Count; i++)
        {
            res += stringArray[i] + " ";
        }
        return res;
    }

}
