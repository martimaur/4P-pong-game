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



    void CheckButton(string name, bool buttonCheck)
    {
        currentSelected = EventSystem.current.currentSelectedGameObject;
        Button buttonHighlighted = currentSelected.GetComponent<Button>();
        if (buttonHighlighted.name == name)
        {
            return;
        }

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

    public void disableButttonStatus(Button button)
    {
        if (button.name == "red")
        {
            stateMan.redEnabled = false;
        }
        else if (button.name == "blue")
        {
            stateMan.blueEnabled = false;
        }
        else if (button.name == "green")
        {
            stateMan.greenEnabled = false;
        }
        else if (button.name == "yellow")
        {
            stateMan.yellowEnabled = false;
        }
    }
}
