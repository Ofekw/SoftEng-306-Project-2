using UnityEngine;
using System.Collections;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine.UI;         

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
        Load();
    }

    void OnApplicationPause(bool pauseState)
    {
        Save();
    }

    void OnDisable()
    {
        Save();
    }

    public void Save()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/playerInfo.dat");

        PlayerData playerData = new PlayerData();
        
        playerData.playerLevel = playerLevel;
        playerData.playerExp = playerExp;
        playerData.playerStr = playerStr;
        playerData.playerAgl = playerAgl;
        playerData.playerDex = playerDex;
        playerData.playerInt = playerInt;
        playerData.playerVit = playerVit;
        playerData.currentGameLevel = currentGameLevel;
        playerData.abilityPoints = abilityPoints;
        

        bf.Serialize(file, playerData);

        file.Close();
    }

    public void Load()
    {
        if (File.Exists(Application.persistentDataPath + "/playerInfo.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/playerInfo.dat", FileMode.Open);
            PlayerData data = (PlayerData) bf.Deserialize(file);
            file.Close();

            playerLevel = data.playerLevel;
            playerExp = data.playerExp;
            playerStr = data.playerStr;
            playerAgl = data.playerAgl;
            playerDex = data.playerDex;
            playerInt = data.playerInt;
            playerVit = data.playerVit;
            currentGameLevel = data.currentGameLevel;
            abilityPoints = data.abilityPoints;
        }
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
            int experienceCarryOver = playerExp - experienceRequired;
            playerExp = experienceCarryOver;
            playerLevel++;
            abilityPoints++;
            experienceRequired = experienceRequired * 2;
        }
    }
}

[Serializable]
class PlayerData
{
    //Implement getter and setters later.
    public int playerLevel;
    public int playerExp;
    public int playerStr;
    public int playerAgl;
    public int playerDex;
    public int playerInt;
    public int playerVit;
    public int currentGameLevel;
    public int abilityPoints;
}