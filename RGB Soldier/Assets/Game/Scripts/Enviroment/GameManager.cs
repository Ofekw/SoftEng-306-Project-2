using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public int orbsCollected = 0;
    public int specialCharge = 0;
    public const int SPECIAL_CHARGE_TARGET = 1000;
    public bool canSpecialAtk = false;
    public int enemiesOnScreen = 0;
    public int stage;

    public const int ORB_COUNT_TARGET = 20;

    public Text orbCountDisp;
    public Slider healthSlider;
    public Slider chargeBar;

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
        orbsCollected = 0;
        specialCharge = 0;
        enemiesOnScreen = 0;
        orbCountDisp.text = "0 / " + ORB_COUNT_TARGET.ToString();
        healthSlider.value = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().currentHealth;
        canSpecialAtk = false;
        chargeBar.maxValue = SPECIAL_CHARGE_TARGET; // set max value of attack charge slider
    }
    void Update()
    {
        countEnemies();
        Player player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        healthSlider.value = player.currentHealth; //update health bar
        orbCountDisp.text = orbsCollected.ToString() + " / " + ORB_COUNT_TARGET.ToString(); //update orb counter text
        chargeBar.value = specialCharge; // set value of special attack slider
        if (player.currentHealth <= 0)
        {
            gameOver(); //NOTE: This seems redundant since Player already checks for health.
        }
        if (orbsCollected >= ORB_COUNT_TARGET)
        {
            levelCleared();
        }
        canSpecialAtk = specialCharge >= SPECIAL_CHARGE_TARGET ? true : false; //set boolean true if player can special attack
        if (!canSpecialAtk)
        {
            incrementSpecialAtkCounter();
        }
    }

    private void countEnemies()
    {
        enemiesOnScreen = GameObject.FindGameObjectsWithTag("Enemy").Length;
    }

    public void incrementSpecialAtkCounter()
    {
        specialCharge++;
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
        print("You died lol");
    }
}