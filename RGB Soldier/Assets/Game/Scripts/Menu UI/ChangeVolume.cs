using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ChangeVolume : MonoBehaviour {

    public Text sliderText;
    //get volume component

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void volumeUpdate(float textUpdateNumber)
    {
        string sliderTextString = ((int)textUpdateNumber).ToString();
        sliderText.text = sliderTextString;

        //TODO: change volume
    }
}
