using UnityEngine;
using System.Collections;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class GameControl : MonoBehaviour {

    public static GameControl control;

    public int playerLevel;
    public int playerExp;
    public int playerStr;
    public int playerAgl;
    public int playerDex;
    public int playerInt;
    public int playerVit;
    public int currentGameLevel;
    public int abilityPoints;
    public int experienceRequired;
    public int backgroundVolume = 100;
    public int soundBitsVolume = 100;
    public int colourMode;
    public PlayerData playerData;
    public GameObject lvlup;


    //Save code on enable and disable if you want auto saving.

	// Use this for initialization
	void Awake () {
        if (control == null)
        {
            DontDestroyOnLoad(gameObject);
            control = this;
        }
        else if (control != this)
        {
            Destroy(gameObject);
        }
        
	}

    void Start()
    {
        experienceRequired = 15;
    }

    void OnEnable()
    {
        setupLoad();
    }

    void OnApplicationPause(bool pauseState)
    {
        setupSave();
    }

    void OnDisable()
    {
        setupSave();
    }

    public void setupSave()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/playerInfo.dat");

        Save();

        bf.Serialize(file, playerData);

        file.Close();
    }

    public void Save()
    {
        playerData.playerLevel = playerLevel;
        playerData.playerExp = playerExp;
        playerData.playerStr = playerStr;
        playerData.playerAgl = playerAgl;
        playerData.playerDex = playerDex;
        playerData.playerInt = playerInt;
        playerData.playerVit = playerVit;
        playerData.currentGameLevel = currentGameLevel;
        playerData.abilityPoints = abilityPoints;
        playerData.backgroundVolume = backgroundVolume;
        playerData.soundBitsVolume = soundBitsVolume;
        playerData.colourMode = colourMode;
    }

    public void setupLoad()
    {
        if (File.Exists(Application.persistentDataPath + "/playerInfo.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/playerInfo.dat", FileMode.Open);
            playerData = (PlayerData) bf.Deserialize(file);
            Load();
            file.Close();
        }
    }

    public void Load()
    {
        playerLevel = playerData.playerLevel;
        playerExp = playerData.playerExp;
        playerStr = playerData.playerStr;
        playerAgl = playerData.playerAgl;
        playerDex = playerData.playerDex;
        playerInt = playerData.playerInt;
        playerVit = playerData.playerVit;
        currentGameLevel = playerData.currentGameLevel;
        abilityPoints = playerData.abilityPoints;
        backgroundVolume = playerData.backgroundVolume;
        soundBitsVolume = playerData.soundBitsVolume;
        colourMode = playerData.colourMode;
    }

    public void giveExperience(int experience)
    {
        playerExp += experience;
        checkExperience();
    }

    public void checkExperience()
    {
        if (playerExp >= experienceRequired)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            Instantiate(lvlup, player.transform.position, player.transform.rotation);
        }
    }

    public void levelAndCarryOver()
    {
        int experienceCarryOver = playerExp - experienceRequired;
        playerExp = experienceCarryOver;
        playerLevel++;
        abilityPoints++;
        experienceRequired = experienceRequired * 2;
    }
}

[Serializable]
public class PlayerData
{
    public int playerLevel;
    public int playerExp;
    public int playerStr;
    public int playerAgl;
    public int playerDex;
    public int playerInt;
    public int playerVit;
    public int currentGameLevel;
    public int abilityPoints;
    public int backgroundVolume;
    public int soundBitsVolume;
    public int colourMode;
}
