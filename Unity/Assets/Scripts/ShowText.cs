using UnityEngine;
using System.Collections;
using System.IO;
using UnityEngine.UI;


public class ShowText : MonoBehaviour {

    private string[] text;
    public string fileName;
    public bool next;
    private int index;
    private bool ended = false;
	// Use this for initialization
	void Start () {
        text = ReadFile(fileName);
        next = true;
	}

    string[] ReadFile(string fileName)
    {
        if (File.Exists(System.IO.Path.Combine(Application.streamingAssetsPath, fileName)))
            return File.ReadAllLines(System.IO.Path.Combine(Application.streamingAssetsPath, fileName));
        else
            throw new FileNotFoundException();
    }
	
    void viewText()
    {
        if (next && !ended)
        {
            if (index < text.Length)
            {
                GameObject creditText = Instantiate(Resources.Load("Prefabs/CreditText")) as GameObject;
                creditText.transform.SetParent(GameObject.Find("AllText").transform, false);
                creditText.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, -600, 0);
                creditText.GetComponent<Text>().text = text[index];
                index++;
                next = false;
            }
            else
            {
                ended = true;
            }
        }
        else if(ended && GameObject.Find("AllText").transform.childCount == 0)
        {            
            Application.LoadLevel("StartMenu");
        }
    }

    void SpeedUp()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            Time.timeScale = 20;
        }
        else
            Time.timeScale = 1;
    }

    void Exit()
    {
        if(Input.GetKeyUp(KeyCode.Escape))
        {
            Application.LoadLevel("StartMenu");
        }
    }

	// Update is called once per frame
	void Update () {
        viewText();
        SpeedUp();
        Exit();
	}
}
