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
    }

    // Update is called once per frame
    void Update()
    {
        Time.timeScale = showInfo ? 0 : 1;
        GameManager.State gameState = showInfo ? GameManager.State.Paused : GameManager.State.Running;
        GameManager.instance.SetState(gameState);

    }

    public void ToggleInfo()
    {
        showInfo = !showInfo;
        GameManager.State gameState = showInfo ? GameManager.State.Paused : GameManager.State.Running;
        GameManager.instance.SetState(gameState);
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
