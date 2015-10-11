using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ChangeVolume : MonoBehaviour {

    public Text sliderText;

    //get volume component

    public Slider backgroundSlider;
    public Slider soundBitsSlider;

    // Use this for initialization
    void Start()
    {
        backgroundSlider.value = GameControl.control.backgroundVolume;
        soundBitsSlider.value = GameControl.control.soundBitsVolume;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void volumeUpdate(float textUpdateNumber)
    {
        string sliderTextString = ((int)textUpdateNumber).ToString();
        sliderText.text = sliderTextString;

        GameControl.control.backgroundVolume = (int)backgroundSlider.value;
        GameControl.control.soundBitsVolume = (int)soundBitsSlider.value;
    }

}
