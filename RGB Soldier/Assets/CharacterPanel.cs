using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CharacterPanel : MonoBehaviour {

	public Text progressTxt;
	public Text currentLevel;
	public Slider xpSlider;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		int xp = GameControl.control.playerExp;
		int target = GameControl.control.experienceRequired;
		int level = GameControl.control.playerLevel;
		progressTxt.text = xp + " / " + target;
		currentLevel.text = "Level: " + level;
		xpSlider.maxValue = target;
		xpSlider.value = xp;
	}
}
