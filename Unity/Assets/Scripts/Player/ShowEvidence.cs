using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;


public class ShowEvidence : MonoBehaviour
{
    private List<GameObject> evidenceList = new List<GameObject>();
    private int index = 0;
    private float X;
    private float Y;

    // Use this for initialization
    void Start()
    {

    }

    void ViewList()
    {
        if (Input.GetKey(KeyCode.E))
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

    void getScreenSize()
    {
        X = Screen.width;
        Y = Screen.height;
    }

    void FillList()
    {
        if (index < evidenceList.Count)
        {

            GameObject textElement = Instantiate(Resources.Load("Prefabs/EvidenceElement")) as GameObject;
            textElement.transform.SetParent(transform, false);
            textElement.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 481 - ((float)index * 108));
            textElement.name = "EvidenceText " + index;
  
            Text evidenceName = textElement.transform.Find("EvidenceName").GetComponent<Text>();
            Text evidenceDiscription = textElement.transform.Find("EvidenceDescription").GetComponent<Text>();
            evidenceName.text = evidenceList[index].GetComponent<EvidenceBehviour>().name;
            evidenceDiscription.text = evidenceList[index].GetComponent<EvidenceBehviour>().description;
            index++;
        }
    }

    // Update is called once per frame
    void Update()
    {
        getScreenSize();
        ViewList();
        GetEvidence();
        FillList();
    }
}
