using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

[RequireComponent(typeof(LoadSceneAsync))]
public class GameManager : MonoBehaviour
{
	public enum State {
		Paused, Running
	}
    public static GameManager instance;
    public int orbsCollected = 0;
    public float specialCharge = 0;
    public const int SPECIAL_CHARGE_TARGET = 1000;
    public bool canSpecialAtk = false;
    public int enemiesOnScreen = 0;
    public int stage;
    public bool isTutorial;
	public bool isBulletTime;

    public int ORB_COUNT_TARGET = 20;

    public Text orbCountDisp;
    public Slider chargeBar;
	public Text healthDisp;
    public string nextScene;
    public LoadSceneAsync lsa;
    public int currentLevel;


    private Text stageText;
    private GameObject stageImage;

    private State state;

	public State getState() {
		return state;
	}

	public void SetState(State setState) {
		state = setState;
	}

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
        if (!isTutorial)
        {
            stageImage = GameObject.Find("StageImage");
            stageText = GameObject.Find("StageText").GetComponent<Text>();
            stageText.text = "Stage " + currentLevel;
            stageImage.SetActive(true);
            Invoke("HideStageImage", 2);
        }
        orbsCollected = 0;
        specialCharge = 0;
        enemiesOnScreen = 0;
        orbCountDisp.text = "0 / " + ORB_COUNT_TARGET.ToString();
        canSpecialAtk = false;
        chargeBar.maxValue = SPECIAL_CHARGE_TARGET; // set max value of attack charge slider
		state = State.Running;
    }
    void Update()
    {
		if (GameManager.instance.isPaused ())
			return;
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


    void HideStageImage()
    {
        stageImage.SetActive(false);
    }

    private void countEnemies()
    {
        enemiesOnScreen = GameObject.FindGameObjectsWithTag("Enemy").Length;
    }

    public void incrementSpecialAtkCounter(Player player)
    {
		specialCharge += (float)(player.intelligence) / 5;
    }

    public void resetSpecialAtkCounter()
    {
        specialCharge = 0;
    }

    void levelCleared()
    {
        // only moves up the current level if its the current 
        if (currentLevel == GameControl.control.currentGameLevel)
        {
            Debug.Log("HELLO xx" + currentLevel);
            GameControl.control.currentGameLevel = GameControl.control.currentGameLevel +1;
        }
        lsa.ClickAsync(nextScene);
    }

    void gameOver()
    {
        Application.LoadLevel("game_over_screen");
        

    }

	public bool isPaused() {
		if (GameManager.instance.getState ().Equals (GameManager.State.Paused)) {
			return true;
		}
		return false;
	}

	public void activateBulletTime() {
		if (isBulletTime)
			return;
		isBulletTime = true;
		StartCoroutine (StartBulletTime ());
		print ("Bullet time ON: " + isBulletTime);
	}

	IEnumerator StartBulletTime() {
		isBulletTime = true;
		yield return new WaitForSeconds(5);
		isBulletTime = false;
		print ("Bullet time OFF: " + isBulletTime);
	}
}