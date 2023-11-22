using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonEnabled : MonoBehaviour
{
    private Button thisButton;
    private GameObject buttonStateMan;
    private ButtonStateManager stateMan;
    private GameObject currentSelected;

    void Start()
    {
        thisButton = GetComponent<Button>();
        buttonStateMan = GameObject.Find("ButtonStateManager");
        stateMan = buttonStateMan.GetComponent<ButtonStateManager>();
    }


    // need fix buttons ;
    void CheckButton(string name, bool buttonCheck)
    {
        if (!buttonCheck && thisButton.name == name && thisButton.interactable == true)
        {
            thisButton.interactable = false;
        }
    }

    private void FixedUpdate()
    {
        CheckButton("red", stateMan.redEnabled);
        CheckButton("blue", stateMan.blueEnabled);
        CheckButton("green", stateMan.greenEnabled);
        CheckButton("yellow", stateMan.yellowEnabled);
    }

    public void disableButttonStatus(string name)
    {
        if (name == "red")
        {
            stateMan.redEnabled = false;
        }
        else if (name == "blue")
        {
            stateMan.blueEnabled = false;
        }
        else if (name == "green")
        {
            stateMan.greenEnabled = false;
        }
        else if (name == "yellow")
        {
            stateMan.yellowEnabled = false;
        }
    }
}
