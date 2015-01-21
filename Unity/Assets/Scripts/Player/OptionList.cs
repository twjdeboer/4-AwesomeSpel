using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/*
 * Class to store options for a question.
 * */
public class OptionList : List<Option> {

    public List<Option> list;
    public string question;

    /*
     * Creates a question with options
     * */
    public OptionList(string question)
    {
        this.question = question;
        list = new List<Option>();
    }

}
