using UnityEngine;
using System.Collections;
using System.Linq;
using UnityEngine.UI;

public class NPCInteraction : MonoBehaviour {

    public Canvas conversationInterface;
    public Text conversationText;
    public Text nameText;

    public string NPCName;
    public float timeToWait;
    public int maxNumberOfChar;
    public int maxNumberOfLines;
    public int lineWidth;

    private int lineNumber = 1;
    private float timer = 0;
    private int index = 0;
    private string textToDisplay;
    private bool runTime = false;
    private int[] previousLineLength;

    
    void OnMouseUp()
    {
        conversationInterface.gameObject.SetActive(true);
        runTime = true;
        gameObject.GetComponent<Astar>().stopWalking = true;
    }
    
    // Use this for initialization
	void Start () 
    {
        previousLineLength = new int[4]{ 0, 0, 0, 0};
        conversationInterface = GameObject.Find("Conversation Interface").GetComponent<Canvas>();
        conversationText = GameObject.Find("Conversation Text").GetComponent<Text>();
        nameText = GameObject.Find("Name Text").GetComponent<Text>();
        nameText.text = NPCName;       
        conversationInterface.gameObject.SetActive(false);
	}





    void DisplayWordForWord(string text)
    {
        char[] splittedText = text.ToCharArray();

        if (index == 0)
        {
            textToDisplay = splittedText[0].ToString();
            index++;
        }

        if (runTime)
            timer += Time.deltaTime;

        if(timer > timeToWait && index < splittedText.Length )
        {
            WriteNextWord(textToDisplay, maxNumberOfChar, splittedText[index].ToString());
            index++;
            timer = 0;
        }

        conversationText.text = textToDisplay;
            
    }

    void WriteNextWord(string currentText, int maxNumberOfChar, string nextWord )
    {
        string temp = textToDisplay;
        textToDisplay += "" + nextWord;
    }

	
	// Update is called once per frame
	void Update () {

        DisplayWordForWord("Hello World! This is a great test. We can see if the text is wrapped automatically. Does it works? Apparently it works."+
            "Now we have to work on line numbering. If the max number of lines is reached the box have to be cleared and te text should go on when we press te mouse or space. Does it work?");
	
	}
}
