using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;

public class TestModalWindow : MonoBehaviour
{
    private DialogPanel modalPanel;
    private DisplayManager displayManager;

    void Awake()
    {
        modalPanel = DialogPanel.Instance();
        displayManager = DisplayManager.Instance();
    }

    //  Send to the Modal Panel to set up the Buttons and Functions to call
    public void TestYNC()
    {
        StartCoroutine(modalPanel.StartDialog(new List<string>()
        { "a1a2a3a4a5a6", "b7b8b9b0b10", "c11c12c13c14c15c" }));
        //      modalPanel.Choice ("Would you like a poke in the eye?\nHow about with a sharp stick?", myYesAction, myNoAction, myCancelAction);
    }
}