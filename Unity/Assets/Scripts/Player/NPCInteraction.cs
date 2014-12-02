using UnityEngine;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine.UI;

public class NPCInteraction : MonoBehaviour {

    private Canvas conversationInterface;
    private Text conversationText;
    private Text nameText;
    private TextGenerator genText;

    public string NPCName;
    public float timeToWait;
    public int maxNumberOfLines;
    public bool stopWalking = false;


    private int lineNumber;
    private float intTime;
    private float timer = 0;
    private int index = 0;
    private string textToDisplay;
    private bool runTime = false;
    private bool pauseForNextLine = false;
    private bool endOfText = false;

    
    void OnMouseUp()
    {
        conversationInterface.gameObject.SetActive(true);
        runTime = true;
        gameObject.GetComponent<Astar>().stopWalking = true;
        ResourceManager.conversationWith = transform;
        ResourceManager.stopWalking = true;
    }
    
    // Use this for initialization
	void Start () 
    {
        conversationInterface = GameObject.Find("Conversation Interface").GetComponent<Canvas>();
        conversationText = GameObject.Find("Conversation Text").GetComponent<Text>();
        nameText = GameObject.Find("Name Text").GetComponent<Text>();
        conversationInterface.gameObject.SetActive(false);
        intTime = timeToWait;
	}

    void RotateToPlayer()
    {
        if (!stopWalking)
        {
            float speed = gameObject.GetComponent<Astar>().rotateSpeed;
            Transform target = GameObject.Find("Player").transform;
            Vector3 targetDir = target.position - transform.position;
            float step = speed * Time.deltaTime;
            Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, step, 0.0F);
            transform.rotation = Quaternion.LookRotation(newDir);
        }
    }



    void DisplayWordForWord(string text)
    {
        char[] splittedText = text.ToCharArray();
        int maxNumberOfChar = splittedText.Length;

        if (index == 0)
        {
            nameText.text = NPCName;
            textToDisplay = splittedText[0].ToString();
            index++;
        }

        if (runTime)
            timer += Time.deltaTime;

        if(timer > timeToWait && !endOfText)
        {
            WriteNextWord(textToDisplay, maxNumberOfChar, splittedText[index]);
            timer = 0;
        }

        if(endOfText)
        {
            if (Input.GetKeyUp(KeyCode.Mouse0) || Input.GetKeyUp(KeyCode.Space))
            {
                conversationInterface.gameObject.SetActive(false);
                gameObject.GetComponent<Astar>().stopWalking = false;
                ResourceManager.stopWalking = false;
                index = 0;
                runTime = false;
                textToDisplay = "";
                conversationText.text = "";
                nameText.text = "";
                endOfText = false;
            }
        }

        conversationText.text = textToDisplay;
            
    }

    void WriteNextWord(string currentText, int maxNumberOfChar, char nextLetter)
    {
        lineNumber = conversationText.cachedTextGenerator.lineCount;
        if (lineNumber > maxNumberOfLines)
        {
            BoxEnd(currentText);
            pauseForNextLine = true;            
        }
        if (pauseForNextLine)
        {
            if (Input.GetKeyUp(KeyCode.Mouse0) || Input.GetKeyUp(KeyCode.Space))
            {
                conversationText.text = "";
                textToDisplay = "";
                lineNumber = 1;
                pauseForNextLine = false;
            }
        }

        if (lineNumber <= maxNumberOfLines && !pauseForNextLine)
        {
            textToDisplay += "" + nextLetter.ToString();
            index++;
        }

        if (index >= maxNumberOfChar)
        {
            endOfText = true;
        }
    }

    void BoxEnd(string text)
    {
        if (!pauseForNextLine)
        {
            List<string> splittedText = text.Split(' ').ToList<string>();
            splittedText.RemoveAt(splittedText.Count - 1);
            string newText = Methods.MakeStringOutArray(splittedText);
            index = newText.Length;
            textToDisplay = newText;
        }
    }


    
	
	// Update is called once per frame
	void Update () {

        DisplayWordForWord("Hello World! This is a great test. We can see if the text is wrapped automatically. Does it works? Apparently it works."+
            "Now we have to work on line numbering. If the max number of lines is reached the box have to be cleared and te text should go on when we press te mouse or space. Does it work?");
        RotateToPlayer();

    }
}
