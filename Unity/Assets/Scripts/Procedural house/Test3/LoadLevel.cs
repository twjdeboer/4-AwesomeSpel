using UnityEngine;
using System.Collections;

public class LoadLevel : MonoBehaviour {

    private GameObject loadScene;
    private GameObject loadCamera;
    private GameObject loadText;

    void Awake()
    {
        loadScene = GameObject.Find("Execute");
        loadScene.SetActive(false);
        loadCamera = GameObject.Find("LoadCamera");
        loadText = GameObject.Find("Load");
    }

	
	// Update is called once per frame
	void Update () {
	
        if(ResourceManager.levelLoaded)
        {
            loadScene.SetActive(true);
            loadCamera.SetActive(false);
            loadText.SetActive(false);

        }

	}
}
