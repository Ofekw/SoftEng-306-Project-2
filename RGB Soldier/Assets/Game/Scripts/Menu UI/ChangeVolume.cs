using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ChangeVolume : MonoBehaviour {
    
    // slider's associated text
    public Text sliderText;

    public Slider backgroundSlider;
    public Slider soundBitsSlider;

    // Use this for initialization
    void Start()
    {
        // load persisted data
        int soundBit = GameControl.control.soundBitsVolume;
        backgroundSlider.value = GameControl.control.backgroundVolume;
        soundBitsSlider.value = soundBit;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void volumeUpdate(float textUpdateNumber)
    {
        // update associated text componenet
        string sliderTextString = ((int)textUpdateNumber).ToString();
        sliderText.text = sliderTextString;

        GameControl.control.backgroundVolume = (int)backgroundSlider.value;
        GameControl.control.soundBitsVolume = (int)soundBitsSlider.value;
    }

}
