using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Option  {

    public string text;
    public string reaction;
    public List<Action> childActions = new List<Action>();

    public Option(string text, string reaction)
    {
        this.text = text;
        this.reaction = reaction;
    }

    public Option(string reaction)
    {
        this.reaction = reaction;
    }


}
