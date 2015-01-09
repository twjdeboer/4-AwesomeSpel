using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class Methods {

    /**
     * Sets Opacity for a material.
     * */
    public static void SetAlpha(GameObject go, float alpha)
    {
        Color color = go.renderer.material.color;
        color.a = alpha;
        go.renderer.material.color = color;
    }


    /**
     * Checks whether a object is in a certain radius of another object.
     * */
    public static bool ReachedPosWithBuffer(Vector3 pos, Vector3 targetPos, Vector3 bufferRadius)
   {
       return (pos.x < targetPos.x + bufferRadius.x && pos.x > targetPos.x - bufferRadius.x
           && pos.y < targetPos.y + bufferRadius.y && pos.y > targetPos.y - bufferRadius.y
           && pos.z < targetPos.z + bufferRadius.z && pos.z > targetPos.z - bufferRadius.z);

   }

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
