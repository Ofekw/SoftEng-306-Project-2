using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;

/* This class contains the list of dialogs for the cutscene
 * and starts the cutscene dialog
 */
public class CutSceneStart : MonoBehaviour
{
    private DialogPanel modalPanel;
    public List<string> dialogText = new List<string>();

    void Awake()
    {
        modalPanel = DialogPanel.Instance();
        StartCoroutine(modalPanel.StartDialog(dialogText)); 
    }

    //  Sents the all the text to the script
    public void startDialog()
    {
        StartCoroutine(modalPanel.StartDialog(dialogText)); 
    }
}