using UnityEngine;
using System.Collections;

public class Action{

    public string actionText;
    public int actionNumber;
    public int optionListNumber;

    public Action(int actionNumber, string actionText)
    {
        this.actionNumber = actionNumber;
        this.actionText = actionText;
    }

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
