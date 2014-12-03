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
    private GameObject twoButtons;
    private GameObject threeButtons;
    private GameObject fourButtons;
    private GameObject conversation;

    public string NPCName;
    public float timeToWait;
    public int maxNumberOfLines;
    public bool stopWalking = false;
    public OptionList options;
    public List<string> names;
    public List<GameObject> objects;


    private int lineNumber;
    private float intTime;
    private float timer = 0;
    private int index = 0;
    private string textToDisplay;
    private bool runTime = false;
    private bool Activated = false;
    private bool pauseForNextLine = false;
    private bool endOfText = false;
    private GameObject Active;

    /**
     * Start conversation if clicked on NPC
     * */
    void OnMouseUp()
    {
        conversationInterface.gameObject.SetActive(true);
        nameText.text = NPCName;
        runTime = true;
        gameObject.GetComponent<Astar>().stopWalking = true;
        ResourceManager.conversationWith = transform;
        ResourceManager.stopWalking = true;
    }
    
	void Start () 
    {
        conversation = GameObject.Find("Conversation");
        conversationInterface = GameObject.Find("Conversation Interface").GetComponent<Canvas>();
        conversationText = GameObject.Find("Conversation Text").GetComponent<Text>();
        nameText = GameObject.Find("Name Text").GetComponent<Text>();
        twoButtons = GameObject.Find("2Buttons");
        threeButtons = GameObject.Find("3Buttons");
        fourButtons = GameObject.Find("4Buttons");
        twoButtons.SetActive(false);
        threeButtons.SetActive(false);
        fourButtons.SetActive(false);
        conversationInterface.gameObject.SetActive(false);
        options = new OptionList("Hallo");
        options.Add(new Option("Yolo"));
        options.Add(new Option("hi"));
        options.Add(new Option("323"));
	}

    /**
     * Rotates NPC to player if interaction started.
     * */
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

    /**
     * Makes appear the text slowly.
     * */
    void DisplayWordForWord(string text)
    {
        //makes char array with text.
        char[] splittedText = text.ToCharArray();
        int maxNumberOfChar = splittedText.Length;

        //makes timer run.
        if (runTime)
            timer += Time.deltaTime;

        //After some time write te next letter.
        if(timer > timeToWait && !endOfText)
        {
            WriteNextWord(textToDisplay, maxNumberOfChar, splittedText[index]);
            timer = 0;
        }

        // Reset everything after conversation ended.
        if(endOfText)
        {
            PlayerTalk();
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

    /**
     * Write next letter to the screen
     * */
    void WriteNextWord(string currentText, int maxNumberOfChar, char nextLetter)
    {
        //Pause if end of box is reached.
        lineNumber = conversationText.cachedTextGenerator.lineCount;
        if (lineNumber > maxNumberOfLines)
        {
            BoxEnd(currentText);
            pauseForNextLine = true;            
        }
        //To next piece of text after pressen space/left mouse
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

        //Print next lekker
        if (lineNumber <= maxNumberOfLines && !pauseForNextLine)
        {
            textToDisplay += "" + nextLetter.ToString();
            index++;
        }

        //Indicate end of text
        if (index >= maxNumberOfChar)
        {
            endOfText = true;
        }
    }

    /**
     * Corrects a bug
     * */
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

    void PlayerTalk()
    {
        ActivateUI();
        FillUI();
    }

    void ActivateUI()
    {
        if (!Activated)
        {
            int numberOfOptions = options.Count;
            if (numberOfOptions == 2)
                Active = twoButtons;
            if (numberOfOptions == 3)
                Active = threeButtons;
            if (numberOfOptions == 4)
                Active = fourButtons;

            Active.SetActive(true);
            Activated = true;
        }
    }


    void FillUI()
    {
        GameObject.Find("Question Text").GetComponent<Text>().text = options.question;
        for (int i = 1; i < options.Count +1; i++ )
        {
            GameObject.Find("Option " + i + " Text").GetComponent<Text>().text = options[i - 1].text;
        }
    }
	
	// Update is called once per frame
	void Update () {

        DisplayWordForWord("Hello World! This is a great test. We can see if the text is wrapped automatically. Does it works? Apparently it works."+
            "Now we have to work on line numbering. If the max number of lines is reached the box have to be cleared and te text should go on when we press te mouse or space. Does it work?");
        RotateToPlayer();

    }
}
