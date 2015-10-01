using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public int orbsCollected = 0;
    public float specialCharge = 0;
    public const int SPECIAL_CHARGE_TARGET = 1000;
    public bool canSpecialAtk = false;
    public int enemiesOnScreen = 0;
    public int level = 1;

    private Text stageText;
    private GameObject stageImage;



    public const int ORB_COUNT_TARGET = 20;

    public Text orbCountDisp;
    public Slider chargeBar;
	public Text healthDisp;

 


    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject); //this enforces Singleton pattern
        }
        DontDestroyOnLoad(gameObject);
        InitGame();
    }

    void InitGame()
    {

        stageImage = GameObject.Find("StageImage");
        stageText = GameObject.Find("StageText").GetComponent<Text>();
        stageText.text = "Stage " + level;
        stageImage.SetActive(true);
        Invoke("HideStageImage", 2);
        orbsCollected = 0;
        specialCharge = 0;
        enemiesOnScreen = 0;
        orbCountDisp.text = "0 / " + ORB_COUNT_TARGET.ToString();
        canSpecialAtk = false;
        chargeBar.maxValue = SPECIAL_CHARGE_TARGET; // set max value of attack charge slider

    }

    void HideStageImage()
    {
        stageImage.SetActive(false);
    }


    void Update()
    {
        countEnemies();
        Player player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        //update health counter
		healthDisp.text = "x " + player.currentHealth;
		orbCountDisp.text = orbsCollected.ToString() + " / " + ORB_COUNT_TARGET.ToString(); //update orb counter text
        chargeBar.value = specialCharge; // set value of special attack slider
        if (player.currentHealth <= 0)
        {
            gameOver();
        }
        if (orbsCollected >= ORB_COUNT_TARGET)
        {
            levelCleared();
        }
        canSpecialAtk = specialCharge >= SPECIAL_CHARGE_TARGET ? true : false; //set boolean true if player can special attack
        if (!canSpecialAtk)
        {
            incrementSpecialAtkCounter(player);
        }
    }



    private void countEnemies()
    {
        enemiesOnScreen = GameObject.FindGameObjectsWithTag("Enemy").Length;
    }

    public void incrementSpecialAtkCounter(Player player)
    {
		specialCharge += (float)(player.intelligence) / 20;
    }

    public void resetSpecialAtkCounter()
    {
        specialCharge = 0;
    }

    void levelCleared()
    {
        print("Level has been cleared!");
    }

    void gameOver()
    {
        //TODO: Game over stuff
    }
}