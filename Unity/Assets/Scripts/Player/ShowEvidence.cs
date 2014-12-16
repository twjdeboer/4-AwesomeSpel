using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;


public class ShowEvidence : MonoBehaviour
{
    private List<GameObject> evidenceList = new List<GameObject>();
    
    // Use this for initialization
    void Start()
    {

    }

    void ViewList()
    {
        if(Input.GetKey(KeyCode.E))
        {
            gameObject.GetComponent<CanvasGroup>().alpha = 1;
        }
        else
        {
            gameObject.GetComponent<CanvasGroup>().alpha = 0;
        }
    }

    void GetEvidence()
    {
        evidenceList = ResourceManager.evidenceList;
    }

    void FillList()
    {
        for(int i = 0; i < evidenceList.Count; i++)
        {
            GameObject textElement = Instantiate(Resources.Load("/Prefabs/EvidenceElement")) as GameObject;
            textElement.name = "EvidenceText " + i;
        }
    }

    // Update is called once per frame
    void Update()
    {
        ViewList();
        GetEvidence();
    }
}
