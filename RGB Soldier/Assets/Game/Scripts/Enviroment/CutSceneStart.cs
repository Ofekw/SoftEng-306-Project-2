using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;

public class CutSceneStart : MonoBehaviour
{
    private DialogPanel modalPanel;
    private DisplayManager displayManager;
    public List<string> dialogText = new List<string>();

    void Awake()
    {
        modalPanel = DialogPanel.Instance();
        displayManager = DisplayManager.Instance();
        StartCoroutine(modalPanel.StartDialog(dialogText)); 
    }

    //  Sents the all the text to the script
    public void startDialog()
    {
        StartCoroutine(modalPanel.StartDialog(dialogText)); 
    }
}