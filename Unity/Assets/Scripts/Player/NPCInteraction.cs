using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class NPCInteraction : MonoBehaviour {

    public Canvas conversationInterface;
    public Text conversationText;
    public Text name;


    public string NPCName;
    public float timeToWait;
    public int maxNumberOfChar;

    private int lineNumber;
    private float timer = 0;
    private int index = 0;
    private string textToDisplay;
    private bool runTime = false;
    private int previousLineLength = 0;

    
    void OnMouseUpAsButton()
    {
        conversationInterface.gameObject.SetActive(true);
        runTime = true;
    }
    
    // Use this for initialization
	void Start () 
    {
        name.text = NPCName;

	}



    void DisplayWordForWord(string text)
    {
        string[] splittedText = text.Split(' ');

        if (index == 0)
        {
            textToDisplay = splittedText[0];
            index++;
        }

        if (runTime)
        timer += Time.deltaTime;

        if(timer > timeToWait && index < splittedText.Length )
        {
            WriteNextWord(textToDisplay, maxNumberOfChar, splittedText[index]);
            index++;
            timer = 0;
        }

        conversationText.text = textToDisplay;
            
    }

    void WriteNextWord(string currentText, int maxNumberOfChar, string nextWord )
    {
        string temp = textToDisplay;
        textToDisplay += " " + nextWord;
        if (textToDisplay.Length - previousLineLength > maxNumberOfChar)
        {
            textToDisplay = temp + "\r\n" + nextWord;
            previousLineLength = textToDisplay.Length - previousLineLength;
        }
    }
	
	// Update is called once per frame
	void Update () {

        DisplayWordForWord("Hello World! This is a great test. We can see if the text is wrapped automatically. Does it works?");
	
	}
}
