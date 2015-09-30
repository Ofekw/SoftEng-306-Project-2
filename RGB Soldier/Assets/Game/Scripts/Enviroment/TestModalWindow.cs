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
        { "Our story is a simple one. You are a magical spartan trained in the art of sword fighting recovering from a drinking problem. ",
            "Simply put, you have been sent from another universe to bring back the colour of this world.",
            "What has happened to the colour you may be wondering?",
            "Well, it has been stolen by an evil mage, set out to turn all universes and dimensions into colourless black holes. What a turd!",
            "A word of caution, your quest will not be easy and on this quest you will fail. However you shall always return, more powerful and knowledgeable than you were before. You see, every time you die, the story resumes from another parallel universe where you have not yet failed. Simple right?"
        }));
    }
}