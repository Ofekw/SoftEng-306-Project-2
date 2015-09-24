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
 
    //Update with player points
    int points;

    public void Awake()
    {
        points = player.abilityPoints;
        strSlider.value = player.strength;
        aglSlider.value = player.agility;
        dexSlider.value = player.dexterity;
        intSlider.value = player.intelligence;
        vitSlider.value = player.vitality;
        pointsText.text = "Points: " + points;
    }

    public void clickAdd(Slider statBar) {
        //TODO - check points before adding them
        if (statBar.value != 5 && points > 0)
        {
            statBar.value = statBar.value + 1;
            string statType = statBar.name.Substring(0, 3);
            // update character stat
            switch (statType)
            {
                case "Str":
                    player.strength = (int)statBar.value;
                    break;
                case "Agl":
                    player.agility = (int)statBar.value;
                    break;
                case "Dex":
                    player.dexterity = (int)statBar.value;
                    break;
                case "Int":
                    player.intelligence = (int)statBar.value;
                    break;
                case "Vit":
                    player.vitality = (int)statBar.value;
                    break;
            }
            // Update the character points
            points--;
            pointsText.text = "Points: " + points;
            player.abilityPoints = points;
            player.UpdateStats();
        }

    }
}
