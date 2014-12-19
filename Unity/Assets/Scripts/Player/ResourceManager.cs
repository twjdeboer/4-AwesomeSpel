using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ResourceManager{

    public static Vector3 playerPosition { get; set; }
    public static Transform cam { get; set; }

    private static float intT = 0;
    public static float t { get { return intT; } set { intT = value; } }

    public static Vector3 intPos { get; set; }

    private static bool _networkReady = false;
    public static bool networkReady { get { return _networkReady; } set { _networkReady = value; } }

    public static int numberOfChilds { get; set; }

    public static bool stopWalking { get; set; }
    public static Transform conversationWith { get; set; }

    private static int intChoice = 0;
    public static int choiceOfPlayer { get { return intChoice; } set { intChoice = value; } }

    public static List<GameObject> evidenceList = new List<GameObject>();

    private static bool _levelLoaded = false;
    public static bool levelLoaded { get { return _levelLoaded; } set { _levelLoaded = value; } }


	public static Vector3 newplayerpos{ get; set; }
}
