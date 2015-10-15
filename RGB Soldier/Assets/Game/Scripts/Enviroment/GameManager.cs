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
    public Text powerupCountdown;
    public string nextScene;
    public LoadSceneAsync lsa;
    public int currentLevel;
    public GameObject player;
    public SkinnedMeshRenderer skin;
    public Material[] materials;
    public Texture[] textures;

    private Text stageText;
    private GameObject stageImage;

    private const float POWERUP_TIME = 5f;

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
            Destroy(gameObject);  //this enforces Singleton pattern
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
        powerupCountdown.text = "";
        canSpecialAtk = false;
        chargeBar.maxValue = SPECIAL_CHARGE_TARGET;  // set max value of attack charge slider
		state = State.Running;

        var i = GameControl.control.playerSprite;
        player = GameObject.Find("Player");
        skin = player.transform.FindChild("p_sotai").GetComponent<SkinnedMeshRenderer>();
        materials = skin.materials;
        Debug.Log(materials);
        Debug.Log(i);
        if (i == 1) {
            for (i = 0; i < 4; i++) {
                materials[i].mainTexture = Resources.Load("ch034", typeof(Texture2D)) as Texture2D;

            }

            materials[0].mainTexture = Resources.Load("ch034", typeof(Texture2D)) as Texture2D;
            materials[1].mainTexture = Resources.Load("ch034", typeof(Texture2D)) as Texture2D;
            materials[2].mainTexture = Resources.Load("ch034", typeof(Texture2D)) as Texture2D;
            materials[3].mainTexture = Resources.Load("ch034", typeof(Texture2D)) as Texture2D;



        }
        else if (i == 2)
        {
            for (i = 0; i < 4; i++)
            {
                materials[i].mainTexture = Resources.Load("ch032", typeof(Texture2D)) as Texture2D;
            }
            //materials[0] = Resources.Load("ch032") as Material;
            //materials[1] = Resources.Load("ch032") as Material;
            //materials[2] = Resources.Load("ch032") as Material;
            //materials[3] = Resources.Load("ch032") as Material;
        }
        else if (i == 3)
        {
            for (i = 0; i < 4; i++)
            {
                materials[i].mainTexture = Resources.Load("ch029", typeof(Texture2D)) as Texture2D;

            }
           //materials[0] = Resources.Load("ch029") as Material;
            //materials[1] = Resources.Load("ch029") as Material;
            //materials[2] = Resources.Load("ch029") as Material;
            //materials[3] = Resources.Load("ch029") as Material;
        }


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
        chargeBar.value = specialCharge;  // set value of special attack slider
        //health check
        if (player.currentHealth <= 0)
        {
            gameOver();
        }
        //orb check
        if (orbsCollected >= ORB_COUNT_TARGET)
        {
            levelCleared();
        }
        //special attack check
        canSpecialAtk = specialCharge >= SPECIAL_CHARGE_TARGET ? true : false;  //set boolean true if player can special attack
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
	}

    IEnumerator StartBulletTime() {
		isBulletTime = true;
        // go through countdown timer
        for (int i = (int)POWERUP_TIME; i > 0; i--)
        {
            powerupCountdown.text = "" + i;
            yield return new WaitForSeconds(1f);
        }
        powerupCountdown.text = "";  // rest countdown timer text
		isBulletTime = false;
	}
}