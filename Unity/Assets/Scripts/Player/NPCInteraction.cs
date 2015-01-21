using UnityEngine;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Xml;
using System.Text;
using System.IO;

/**
 * Component which allows the player to interact with NPC
 * */
public class NPCInteraction : MonoBehaviour {

    private Text conversationText;
    private Text nameText;
    private TextGenerator genText;
    private GameObject twoButtons;
    private GameObject threeButtons;
    private GameObject fourButtons;
    private GameObject conversation;
    private GameObject blockRayCast;

    public string NPCName;
    public float timeToWait;
    public int maxNumberOfLines;
    public bool stopWalking = false;
    public List<OptionList> options = new List<OptionList>();
    public List<Action> actionList = new List<Action>();
    public int choiceOfPlayer;
    public string fileName;

    private bool anotherQuestion = true;
    private string playerTalkName;
    private Transform target;
    private bool NPCHasToAnswer = false;
    private bool NPCAnswer = false;
    private int actionIndex = 0;
    private int lineNumber;
    private float intTime;
    private float timer = 0;
    private int index = 0;
    public string textToDisplay;
    private bool runTime = false;
    private bool Activated = false;
    private bool pauseForNextLine = false;
    private bool endOfText = false;
    private GameObject Active;
    private bool Go = false;

    /**
     * Start conversation if clicked on NPC.
     * */
    void OnMouseUp()
    {
        if (gameObject.GetComponent<Astar>() != null)
        {
            gameObject.GetComponent<Astar>().stopWalking = true;
        }
        BlockRayCast();
        target = GameObject.Find("PlayerModel").transform;
        playerTalkName = GameObject.Find("PlayerModel").GetComponent<Player>().playerName;
        ResourceManager.conversationWith = transform;
        ResourceManager.stopWalking = true;
        XMLReader(fileName);
        Go = true;
    }
    
    /*
     * Initializes interface 
     * */
	void Start () 
    {
        GameObject conversationObjects = Instantiate(Resources.Load("Prefabs/ConversationObjects")) as GameObject;
        conversationObjects.transform.SetParent(GameObject.Find("Interface").transform , false);
        conversation = GameObject.Find("Conversation");
        conversationText = GameObject.Find("Conversation Text").GetComponent<Text>();
        nameText = GameObject.Find("Name Text").GetComponent<Text>();
        twoButtons = GameObject.Find("2Buttons");
        threeButtons = GameObject.Find("3Buttons");
        fourButtons = GameObject.Find("4Buttons");
        twoButtons.SetActive(false);
        threeButtons.SetActive(false);
        fourButtons.SetActive(false);
        conversation.SetActive(false);
	}

    /*
     * Creates an object which blocks raycasting for the mouse. So it is not possible to click on a other NPC when talking to this NPC.
     * */
    void BlockRayCast()
    {
        blockRayCast = Instantiate(Resources.Load("Prefabs/BlockRayCast")) as GameObject;
        blockRayCast.transform.SetParent(GameObject.Find("TopViewCamera").transform);
        blockRayCast.transform.localPosition = new Vector3(0, 10, 10);
        blockRayCast.transform.localScale = new Vector3(100, 100, 1);
    }

    /**
     * Rotates NPC to player if interaction started.
     * */
    void RotateToPlayer()
    {
        if (Go && gameObject.GetComponent<Astar>() != null)
        {
            if (!stopWalking)
            {
                float speed = gameObject.GetComponent<Astar>().rotateSpeed;                
                Vector3 targetDir = target.position - transform.position;
                float step = speed * Time.deltaTime;
                Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, step, 0.0F);
                transform.rotation = Quaternion.LookRotation(newDir);
            }
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
        if(index == 0)
            runTime = true;

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
                index = 0;
                runTime = false;
                textToDisplay = "";
                conversationText.text = "";
                nameText.text = "";
                endOfText = false;
                if(!NPCHasToAnswer && !NPCAnswer)
                    actionIndex++;
                if (NPCHasToAnswer)
                {
                    NPCAnswer = true;
                    Active.SetActive(false);
                    anotherQuestion = true;
                }
                else if (NPCAnswer)
                {
                    ResourceManager.choiceOfPlayer = 0;
                    actionIndex++;
                    NPCAnswer = false;
                    Activated = false;
                }
            }

        }

        conversationText.text = textToDisplay;
            
    }

    /*
     * Uses interface to let NPC talk
     * */
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

    /*
     * Uses interface to let player talk
     * */
    void PlayerTalkText(string text)
    {
        conversation.gameObject.SetActive(true);
        nameText.text = playerTalkName;
        DisplayWordForWord(text);
    }

    /*
     * Adds an action to the list if necessary. This is used when another question is asked after a reaction of the player
     * */
    void AnotherQuestion(Option useOption)
    {
        if(useOption.childActions.Count != 0 && anotherQuestion)
        {
            actionList.InsertRange(actionIndex + 1, useOption.childActions);
            anotherQuestion = false;
        }
    }

    /*
     * Enables the player to choose an answer
     * */
    void PlayerTalk(OptionList list)
    {
        choiceOfPlayer = ResourceManager.choiceOfPlayer;
        if (choiceOfPlayer == 0)
        {
            ActivateUI(list);
        }

        PlayerAnswer(list);

    }

    /*
     * Controls what happends after player choose a answer
     * */
    void PlayerAnswer(OptionList list)
    {
        string playerText = null;
        string NPCText = null;
        if(choiceOfPlayer != 0)
        {
            Active.gameObject.SetActive(false);
            nameText.text = playerTalkName;
            playerText = list[choiceOfPlayer - 1].text;
        }
        if (playerText != null && !NPCAnswer)
        {
            conversation.gameObject.SetActive(true);
            NPCHasToAnswer = true;
            DisplayWordForWord(playerText);
        }
        if (NPCAnswer)
        {
            NPCHasToAnswer = false;
            Option useOption = list[choiceOfPlayer - 1];
            NPCText = useOption.reaction;            
            NPCTalk(NPCText);
            AnotherQuestion(useOption);
        }
    }

    /*
     * Activates option menu for the player
     * */
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
            conversation.SetActive(false);
            Active.SetActive(true);
            Activated = true;
            FillUI(list);
        }
    }

    /*
     * Read an option out a XML file
     * */
    void ReadOption(XmlTextReader reader, int optionListNumber, Option parentOption)
    {
        string optionText = "";
        string reactionText = "";
        Option option = null;

        while(reader.Read())
        {
            if (reader.Name.Equals("TEXT"))
            {
                optionText = reader.ReadInnerXml();
            }
            else if (reader.Name.Equals("REACTION") && reader.IsStartElement())
            {
                reactionText = reader.GetAttribute(0);
                if (reader.ReadToDescendant("SEQUEL"))
                {
                    option = new Option(optionText, reactionText);
                    while (reader.Read())
                    {
                        
                        if (reader.Name.Equals("QUESTION") && reader.IsStartElement())
                        {
                            ReadQuestion(reader, option, false, optionListNumber + 1);
                        }
                        else if (reader.Name.Equals("NPC") && reader.IsStartElement())
                        {
                            option.childActions.Add(new Action(1, reader.ReadInnerXml()));
                        }
                        else if (reader.Name.Equals("TALK") && reader.IsStartElement())
                        {
                            option.childActions.Add(new Action(3, reader.ReadInnerXml()));
                        }
                        else if (reader.Name.Equals("SEQUEL") && reader.NodeType.Equals(XmlNodeType.EndElement))
                        {
                            break;
                        }
                    }
                }


            }

            else if (reader.Name.Equals("OPTION") && reader.NodeType.Equals(XmlNodeType.EndElement))
            {
                if(option == null)
                {
                    option = new Option(optionText, reactionText);
                }
                options[optionListNumber].Add(option);
                break;
            }
        }

    }

    /*
     * Read a Question out a xml file
     * */
    void ReadQuestion(XmlTextReader reader,Option parentOption, bool firstQuestion, int optionListNumber)
    {
        string questionText = reader.GetAttribute(0);
        options.Add(new OptionList(questionText));

        while (reader.Read())
        {

            if (reader.Name.Equals("OPTION") && reader.NodeType.Equals(XmlNodeType.Element))
            {
                ReadOption(reader, optionListNumber, parentOption);
            }
            else if (reader.Name.Equals("QUESTION") && reader.NodeType.Equals(XmlNodeType.EndElement) && firstQuestion)
            { 
                actionList.Add(new Action(2, optionListNumber));
                break;
            }
            else if (reader.Name.Equals("QUESTION") && reader.NodeType.Equals(XmlNodeType.EndElement) && !firstQuestion)
            {
                parentOption.childActions.Add(new Action(2, optionListNumber));
                break;
            }

        }
        
    }

    /*
     * Reads the content of the conversation out the XML file
     * */
    void XMLReader(string fileName)
    {
		XmlTextReader reader = new XmlTextReader(System.IO.Path.Combine(Application.streamingAssetsPath, fileName));
        while (reader.Read())
        {
            switch (reader.NodeType)
            {
                case XmlNodeType.Element:
                    if (reader.Name.Equals("NPC"))
                    {
                        actionList.Add(new Action(1, reader.ReadInnerXml()));
                    }
                    if (reader.Name.Equals("PLAYER") && reader.NodeType.Equals(XmlNodeType.Element))
                    {

                        while (reader.Read())
                        {
                            if (reader.Name.Equals("QUESTION") && reader.IsStartElement())
                            {
                                ReadQuestion(reader, null, true, options.Count);
                            }
                            else if (reader.Name.Equals("TALK")&& reader.NodeType.Equals(XmlNodeType.Element))
                            {                              
                                actionList.Add(new Action(3, reader.ReadInnerXml()));
                            }
                            else if (reader.Name.Equals("PLAYER") && reader.NodeType.Equals(XmlNodeType.EndElement))
                            {
                                break;
                            }
                        }
                    }
                    break;
            }
        }
    }

    /*
     * Determines what action is made in the conversation
     * */
    void Action()
    {
        if (Go)
        {
            if (actionIndex < actionList.Count)
            {
                if (actionList[actionIndex].actionNumber == 1)
                {
                    NPCTalk(actionList[actionIndex].actionText);
                }
                else if (actionList[actionIndex].actionNumber == 2)
                {
                    PlayerTalk(options[actionList[actionIndex].optionListNumber]);
                }
                else if (actionList[actionIndex].actionNumber == 3)
                {
                    PlayerTalkText(actionList[actionIndex].actionText);
                }
            }
            else
            {
                conversation.SetActive(false);
                if (gameObject.GetComponent<Astar>() != null)
                {
                    gameObject.GetComponent<Astar>().stopWalking = false;
                }
                Destroy(blockRayCast);
                ResourceManager.stopWalking = false;
                actionList.Clear();
                Go = false;
                actionIndex = 0;
                options.Clear();
            }
        }
    }

    /*
     * Fills the option menu with text
     * */
    void FillUI(OptionList list)
    {
        GameObject.Find("Question Text").GetComponent<Text>().text = list.question;
        for (int i = 1; i < options[actionList[actionIndex].optionListNumber].Count + 1; i++)
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
