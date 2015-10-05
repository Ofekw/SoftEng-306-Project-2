using UnityEngine;
using System.Collections;

public class DamageIndicators : MonoBehaviour {
    public Sprite[] dmg;


    public int CalculateRangedDamageIndicator()
    {
        int rangeDmg = GameControl.control.playerDex;
        return rangeDmg;
    }

    public int CalculateMeleedDamageIndicator()
    {
        int meleeDmg = GameControl.control.playerStr;
        return meleeDmg*1;
    }
}
