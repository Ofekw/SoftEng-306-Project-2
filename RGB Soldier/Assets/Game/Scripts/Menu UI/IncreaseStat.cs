using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class IncreaseStat : MonoBehaviour {

    public Text pointsText;
    //Update with player points
    int points = 5;

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
                    break;
                case "Agl":
                    break;
                case "Dex":
                    break;
                case "Int":
                    break;
                case "Vit":
                    break;
            }
            // Update the character points
            points--;
            pointsText.text = "Points: " + points;
        }

    }
}
