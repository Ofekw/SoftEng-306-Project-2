using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class InfoScript : MonoBehaviour {
    public static bool showInfo;
    public GameObject infoScreen;
    public GameObject statPanel;
    // Use this for initialization
    void Start()
    {
        showInfo = false;
        infoScreen.SetActive(false);
        statPanel.SetActive(true);
    }

    public void ToggleInfo()
    {
        showInfo = !showInfo;
        if (showInfo)
        {
            onInfo();
        }
        else
        {
            offInfo();
        }

    }

    public void onInfo()
    {
        infoScreen.SetActive(true);
        statPanel.SetActive(false);
    }

    public void offInfo()
    {
        infoScreen.SetActive(false);
        statPanel.SetActive(true);
    }
}
