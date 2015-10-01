using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;

public class DialogPanel : MonoBehaviour
{

    public Text displayText;
    public float letterPause = 0.2f;
    public GameObject dialogPanelObject;
    public LoadSceneAsync lsa;
    public int scene_id;
    

    private bool keyPressed;
    private bool firstLine;
    private static DialogPanel dialogPanel;

    public static DialogPanel Instance()
    {
        if (!dialogPanel)
        {
            dialogPanel = FindObjectOfType(typeof(DialogPanel)) as DialogPanel;
            if (!dialogPanel)
                Debug.LogError("There needs to be one active ModalPanel script on a GameObject in your scene.");
        }

        return dialogPanel;
    }

    // Checks input from player each player
    void Update()
    {
        if (!firstLine && (Input.anyKeyDown || Input.touchCount > 0))
        {
            keyPressed = true;
        }
    }

    // Goes through the list printing the text
    public IEnumerator StartDialog(List<string> allText)
    {
        dialogPanelObject.SetActive(true);
        firstLine = true;
        foreach (var text in allText)
        {
            yield return StartCoroutine(TypeText(text));
            yield return StartCoroutine(WaitForKeyPress());
        }
        dialogPanelObject.SetActive(false);
        lsa.ClickAsync(scene_id);
    }

    IEnumerator WaitForKeyPress()
    {
        while (!keyPressed)
        {
            yield return null;
        }
        keyPressed = false;
    }


    // Prints text one character a time
    IEnumerator TypeText(string text)
    {
        displayText.text = "";
        keyPressed = false;
        foreach (char letter in text.ToCharArray())
        {
            bool printing = true;
            if (keyPressed && printing)
            {
                displayText.text = text;
                keyPressed = false;
                break;
            }
            else
            {
                displayText.text += letter;
                yield return new WaitForSeconds(letterPause);
            }
            firstLine = false;
        }
    }

    void ClosePanel()
    {
        dialogPanelObject.SetActive(false);
    }
}