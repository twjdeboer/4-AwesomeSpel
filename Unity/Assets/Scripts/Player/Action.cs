using UnityEngine;
using System.Collections;

/*
 * Represents an action what have to be done in a conversation: Npctalk, playertalk, npc ask question
 **/
public class Action{

    public string actionText;
    public int actionNumber;
    public int optionListNumber;

    /*
     * Creates a new action. This one is used to represent a simple NPCtalk or playertalk
     * */
    public Action(int actionNumber, string actionText)
    {
        this.actionNumber = actionNumber;
        this.actionText = actionText;
    }

    /*
     * Creates a new action. Is used for allowing npc to aks question.
     * */
    public Action(int actionNumber, int optionListNumber)
    {
        this.actionNumber = actionNumber;
        this.optionListNumber = optionListNumber;
    }

   /* public string ToString()
    {
        return "Action[" + actionNumber + ", " + actionText + optionListNumber + "]"; 
    }*/
}
