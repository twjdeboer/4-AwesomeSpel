using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/*
 * Class respresent an option which the player can choose
 * */
public class Option  {

    public string text;
    public string reaction;
    public List<Action> childActions = new List<Action>();

    /*
     * Makes an option with reaction attached
     * */
    public Option(string text, string reaction)
    {
        this.text = text;
        this.reaction = reaction;
    }

    /*
     * makes option with raction
     * */
    public Option(string reaction)
    {
        this.reaction = reaction;
    }


}
