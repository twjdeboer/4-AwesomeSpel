using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class OptionList : List<Option> {

    public List<Option> list;
    public string question;

    public OptionList(string question)
    {
        this.question = question;
        list = new List<Option>();
    }

}
