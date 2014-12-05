using UnityEngine;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Xml;
using System.Text;
using System.IO;

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
    public List<OptionList> options = new List<OptionList>();
    public List<string> names;
    public List<GameObject> objects;
    public List<Action> actionList = new List<Action>();
    public int choiceOfPlayer;
    public string fileName;

    private int actionIndex = 0;
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
    private bool Go = false;

    /**
     * Start conversation if clicked on NPC
     * */
    void OnMouseUp()
    {
        conversationInterface.gameObject.SetActive(true);
        runTime = true;
        gameObject.GetComponent<Astar>().stopWalking = true;
        ResourceManager.conversationWith = transform;
        ResourceManager.stopWalking = true;
        XMLReader(fileName);
        Go = true;
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
        conversation.SetActive(false);
        conversationInterface.gameObject.SetActive(false);
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
            if (Input.GetKeyUp(KeyCode.Mouse0) || Input.GetKeyUp(KeyCode.Space))
            {
                conversationInterface.gameObject.SetActive(false);
                index = 0;
                runTime = false;
                textToDisplay = "";
                conversationText.text = "";
                nameText.text = "";
                endOfText = false;
                actionIndex++;
            }

        }

        conversationText.text = textToDisplay;
            
    }

    void NPCTalk(string text)
    {
        conversation.gameObject.SetActive(true);
        nameText.text = NPCName;
        DisplayWordForWord(text);
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


    void PlayerTalk(OptionList list)
    {
        choiceOfPlayer = ResourceManager.choiceOfPlayer;
        conversationInterface.gameObject.SetActive(true);
        if (choiceOfPlayer == 0)
        {
            ActivateUI(list);
            FillUI(list);
        }
        PlayerAnswer(list);
    }

    void PlayerAnswer(OptionList list)
    {
        string playerText = null;
        if(choiceOfPlayer != 0)
        {
            Active.gameObject.SetActive(false);
            nameText.text = "Player";
            playerText = list[choiceOfPlayer - 1].reaction;
        }
        if (playerText != null)
        {
            conversation.gameObject.SetActive(true);
            runTime = true;
            DisplayWordForWord(playerText);
        }
    }

    void ActivateUI(OptionList list)
    {
        if (!Activated)
        {
            int numberOfOptions = list.Count;
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

    void XMLReader(string fileName)
    {
        XmlTextReader reader = new XmlTextReader("Assets\\Resources\\Text\\" + fileName);
        while (reader.Read())
        {
            switch (reader.NodeType)
            {
                case XmlNodeType.Element:
                    if (reader.Name.Equals("NPC"))
                    {
                        actionList.Add(new Action(1, reader.ReadInnerXml()));
                    }
                    if (reader.Name.Equals("PLAYER"))
                    {
                        reader.ReadToDescendant("QUESTION");
                        string listIndex = reader.GetAttribute(0);
                        options.Add(new OptionList(reader.ReadInnerXml()));
                        while(reader.ReadToFollowing("OPTION"))
                        {
                            string optionText = "";
                            string reactionText = "";
                            reader.ReadToDescendant("TEXT");
                            optionText = reader.ReadInnerXml();
                            reader.ReadToNextSibling("REACTION");
                            reactionText = reader.ReadInnerXml();
                            options[int.Parse(listIndex) - 1].Add(new Option(optionText, reactionText));                      
                        }
                        actionList.Add(new Action(2, listIndex));
                    }
                    break;
            }
        }
    }

    void Action()
    {
        if (Go)
        {
            if (actionIndex < actionList.Count)
            {
                if (actionList[actionIndex].actionNumber == 1)
                    NPCTalk(actionList[actionIndex].actionText);
                else if (actionList[actionIndex].actionNumber == 2)
                {
                    PlayerTalk(options[int.Parse(actionList[actionIndex].actionText) - 1]);
                }
            }
            else
            {
                gameObject.GetComponent<Astar>().stopWalking = false;
                ResourceManager.stopWalking = false;
                Go = false;
            }
        }
    }
    void FillUI(OptionList list)
    {
        GameObject.Find("Question Text").GetComponent<Text>().text = list.question;
        for (int i = 1; i < options.Count +1; i++ )
        {
            GameObject.Find("Option " + i + " Text").GetComponent<Text>().text = list[i - 1].text;
        }
    }
	
	// Update is called once per frame
	void Update () {
        Action();
        RotateToPlayer();

    }
}
