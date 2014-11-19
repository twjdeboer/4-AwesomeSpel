using UnityEngine;
using System.Collections;

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


}
