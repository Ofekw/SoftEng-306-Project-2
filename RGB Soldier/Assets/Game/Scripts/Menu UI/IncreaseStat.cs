using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class IncreaseStat : MonoBehaviour {

    public Text pointsText;
    public Player player;
    public Slider strSlider;
    public Slider aglSlider;
    public Slider dexSlider;
    public Slider intSlider;
    public Slider vitSlider;
    public Text strText;
    public Text aglText;
    public Text dexText;
    public Text intText;
    public Text vitText;
 
    //Update with player points
    int points;

    // Starts after everything has woken - must wait for gamecontrol
    public void Start()
    {
        points = GameControl.control.abilityPoints;
        strSlider.value = GameControl.control.playerStr;
        aglSlider.value = GameControl.control.playerAgl;
        dexSlider.value = GameControl.control.playerDex;
        intSlider.value = GameControl.control.playerInt;
        vitSlider.value = GameControl.control.playerVit;

        pointsText.text = "Points: " + points;
        setStatText();
    }

    public void clickAdd(Slider statBar) {
        if (statBar.value != 5 && points > 0)
        {
            statBar.value = statBar.value + 1;
            string statType = statBar.name.Substring(0, 3);

            // Update character stat
            switch (statType)
            {
                case "Str":
                    player.strength = (int)statBar.value;
                    GameControl.control.playerStr = player.strength;
                    break;
                case "Agl":
                    player.agility = (int)statBar.value;
                    GameControl.control.playerAgl = player.agility;
                    break;
                case "Dex":
                    player.dexterity = (int)statBar.value;
                    GameControl.control.playerDex = player.dexterity;
                    break;
                case "Int":
                    player.intelligence = (int)statBar.value;
                    GameControl.control.playerInt = player.intelligence;
                    break;
                case "Vit":
                    player.vitality = (int)statBar.value;
                    GameControl.control.playerVit = player.vitality;
                    break;
            }

            points--;
            pointsText.text = "Points: " + points;
            player.abilityPoints = points;
            GameControl.control.abilityPoints = points;
            setStatText();
        }

    }

    public void setStatText()
    {
        strText.text = "" + GameControl.control.playerStr;
        aglText.text = "" + GameControl.control.playerAgl;
        dexText.text = "" + GameControl.control.playerDex;
        intText.text = "" + GameControl.control.playerInt;
        vitText.text = "" + GameControl.control.playerVit;
    }

    // For testing purposes
    public void resetStats()
    {
        doReset();
        // resets sliders to values set above
        Start();
    }

    public void doReset()
    {
        player.strength = 1;
        GameControl.control.playerStr = 1;
        player.agility = 1;
        GameControl.control.playerAgl = 1;
        player.dexterity = 1;
        GameControl.control.playerDex = 1;
        player.intelligence = 1;
        GameControl.control.playerInt = 1;
        player.vitality = 1;
        GameControl.control.playerVit = 1;
        player.abilityPoints = 5;
        GameControl.control.abilityPoints = 5;
    }
}
