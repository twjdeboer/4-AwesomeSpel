using UnityEngine;
using System.Collections;

/*
 * Class returns chosen answer
 * */
public class ReturnChosenOption : MonoBehaviour {

    void OptionOne()
    {
        ResourceManager.choiceOfPlayer = 1;
    }

    void OptionTwo()
    {
        ResourceManager.choiceOfPlayer = 2;
    }

    void OptionThree()
    {
        ResourceManager.choiceOfPlayer = 3;
    }

    void OptionFour()
    {
        ResourceManager.choiceOfPlayer = 4;
    }


}
