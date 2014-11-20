using UnityEngine;
using System.Collections;

public static class ResourceManager{

    public static Vector3 playerPosition { get; set; }
    public static Transform cam { get; set; }

    private static float intT = 0;
    public static float t { get { return intT; } set { intT = value; } }

    public static Vector3 intPos { get; set; }
}
