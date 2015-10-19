using UnityEngine;
using System.Collections;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using GooglePlayGames;
using GooglePlayGames.BasicApi.SavedGame;
using GooglePlayGames.BasicApi;

public class GameControl : MonoBehaviour
{
    //Public, static reference to itself.
    public static GameControl control;

    public int playerSprite = 1;
    public int playerLevel;
    public int playerExp;
    public int playerStr;
    public int playerAgl;
    public int playerDex;
    public int playerInt;
    public int playerVit;
    public int currentGameLevel;
    public int abilityPoints;
    public int experienceRequired = 15;
    public int backgroundVolume = 100;
    public int soundBitsVolume = 100;
    public int colourMode;
    public bool vibrateOn = true;
    public PlayerData playerData;
    public GameObject lvlup;
    public int selectedCharacter = 1;

    //Play Service fields
    public DateTime loadedTime;
    public TimeSpan playingTime;

    public string autoSaveName;
    public bool doSave;
    public byte[] cloudData;
    public TimeSpan timePlayed;

    //Save code on enable and disable if you want auto saving.

    // Use this for initialization
    void Awake()
    {
        //Ensures there the singleton design pattern.
        if (control == null)
        {
            DontDestroyOnLoad(gameObject);
            control = this;
            loadedTime = DateTime.Now;
        }
        else if (control != this)
        {
            Destroy(gameObject);
        }

        //Create the Play Games Client and configure it to enable saved game.
        PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder().EnableSavedGames().Build();

        PlayGamesPlatform.InitializeInstance(config);

        PlayGamesPlatform.Activate();

        //Authenticate the player's Google account.
        Social.localUser.Authenticate((bool success) =>
        {
        });
    }

    void Start()
    {
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

    /*
     * If the user is authenticated do cloud save.
     * */
    public void SaveToCloud()
    {
        if (Social.localUser.authenticated)
        {
            doSave = true;
            CloudSync();
        }
    }

    /*
     * Do cloud load.
     * */
    public void DoLoadFromCloud()
    {
        doSave = false;
        CloudSync();
    }

    /*
     * Open the saved game. Conflict resolution is longest play time. Saved game file name is "SavedGame".
     * */
    public void CloudSync()
    {
        ISavedGameClient savedGameClient = PlayGamesPlatform.Instance.SavedGame;
        savedGameClient.OpenWithAutomaticConflictResolution("SavedGame", DataSource.ReadCacheOrNetwork, ConflictResolutionStrategy.UseLongestPlaytime, OpenCloudSave);
    }

    /*
     * Sets up local save. Creates the "playerInfo.dat" files and writes the binary formatted PlayerData object to it.
     * */
    public void setupSave()
    {
        BinaryFormatter bf = new BinaryFormatter();

        //Local save.
        FileStream file = File.Create(Application.persistentDataPath + "/playerInfo.dat");

        Save();

        //Local save.
        bf.Serialize(file, playerData);

        file.Close();
        
    }

    /*
     * Checks to see if the saved file from the cloud has been successfully opened. Then does the cloud save and load depending on
     * the operation boolean "doSave". 
     * */
    public void OpenCloudSave(SavedGameRequestStatus status, ISavedGameMetadata game)
    {
        if (status == SavedGameRequestStatus.Success)
        {
            if (doSave)
            {
                CloudSave(status, game);
            }
            else
            {
                CloudLoad(game);
            }
        }
        else
        {

        }
    }

    /*
     * Does the cloud save.
     * Converts PlayerData into a byte array. Updates play time and saved file description. Then commits.
     * */
    public void CloudSave(SavedGameRequestStatus status, ISavedGameMetadata game)
    {
        Save();

        byte[] data = ToBytes(this.playerData);
        //Calculate play time and total playtime.
        TimeSpan delta = DateTime.Now.Subtract(loadedTime);
        playingTime += delta;
        this.timePlayed = playingTime;

        ISavedGameClient savedGameClient = PlayGamesPlatform.Instance.SavedGame;
        SavedGameMetadataUpdate.Builder builder = new SavedGameMetadataUpdate.Builder();
        builder = builder.WithUpdatedPlayedTime(this.timePlayed).WithUpdatedDescription("Current Level: " + playerData.currentGameLevel + " Current Player Level: " + playerData.playerLevel);

        SavedGameMetadataUpdate updatedMetadata = builder.Build();
        savedGameClient.CommitUpdate(game, updatedMetadata, data, OnSaveWritten);
    }

    /*
     * Reads the saved game file.
     * */
    public void CloudLoad(ISavedGameMetadata game)
    {
        ISavedGameClient savedGameClient = PlayGamesPlatform.Instance.SavedGame;
        savedGameClient.ReadBinaryData(game, OnCloudLoad);
    }

    public void OnSaveWritten(SavedGameRequestStatus status, ISavedGameMetadata game)
    {
        if (status == SavedGameRequestStatus.Success)
        {

        }
        else
        {

        }
    }

    /*
     * Converts the byte array from the cloud save and converts it to a PlayerData object.
     * Loads attributes from that object.
     * */
    public void OnCloudLoad(SavedGameRequestStatus status, byte[] data)
    {
        if (status == SavedGameRequestStatus.Success)
        {
            PlayerData thePlayerData = FromBytes(data);
            this.playerData = thePlayerData;
            Load();
            setupSave();
        }
        else
        {
        }
    }

    /*
     * Updates the PlayerData object with current attributes.
     * */
    public void Save()
    {
        playerData.playerLevel = playerLevel;
        playerData.playerExp = playerExp;
        playerData.playerStr = playerStr;
        playerData.playerAgl = playerAgl;
        playerData.playerDex = playerDex;
        playerData.playerInt = playerInt;
        playerData.playerVit = playerVit;
        playerData.experienceRequired = (experienceRequired != 0) ? experienceRequired : 15;
        playerData.currentGameLevel = currentGameLevel;
        playerData.abilityPoints = abilityPoints;
        playerData.backgroundVolume = backgroundVolume;
        playerData.soundBitsVolume = soundBitsVolume;
        playerData.colourMode = colourMode;
        playerData.vibrateOn = vibrateOn;
        playerData.selectedCharacter = selectedCharacter;
    }

    /*
     * Sets up local loading.
     * Loads the "playerInfo.dat" file if it exists. Deserialises and gets the PlayerData object from the file.
     * Loads attributes from that object.
     * If file doesn't exist then it is a new game and set the experience required at level 1 at 15.
     * */
    public void setupLoad()
    {
        if (File.Exists(Application.persistentDataPath + "/playerInfo.dat"))
        {
            
            BinaryFormatter bf = new BinaryFormatter();
            //Local load.
            FileStream file = File.Open(Application.persistentDataPath + "/playerInfo.dat", FileMode.Open);

            playerData = (PlayerData)bf.Deserialize(file);
            Load();
            file.Close();
            
        }
        else
        {
            experienceRequired = 15;
        }
    }

    /*
     * Load the current game's attributes from the PlayerData object.
     * */
    public void Load()
    {
        playerLevel = playerData.playerLevel;
        playerExp = playerData.playerExp;
        playerStr = playerData.playerStr;
        playerAgl = playerData.playerAgl;
        playerDex = playerData.playerDex;
        playerInt = playerData.playerInt;
        playerVit = playerData.playerVit;
        selectedCharacter = playerData.selectedCharacter;
        currentGameLevel = playerData.currentGameLevel;
        abilityPoints = playerData.abilityPoints;
        backgroundVolume = playerData.backgroundVolume;
        soundBitsVolume = playerData.soundBitsVolume;
        colourMode = playerData.colourMode;
        experienceRequired = (playerData.experienceRequired != 0) ? playerData.experienceRequired : 15;
        vibrateOn = playerData.vibrateOn;
    }

    /*
     * Adds the parameter amount of experience to the player.
     * */
    public void giveExperience(int experience)
    {
        playerExp += experience;
        checkExperience();
    }

    /*
     * Checks to see if the player has enough experience to level up.
     * */
    public void checkExperience()
    {
        if (playerExp >= experienceRequired)
        {
            levelAndCarryOver();
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            Instantiate(lvlup, player.transform.position, player.transform.rotation);
        }
    }

    /*
     * Reset the amount of experience the player has and add 1 to the player level and ability point.
     * If the player has left over experience from the previous level add to the current amount of experience.
     * Increase the amount of experience required for the next level.
     * */
    public void levelAndCarryOver()
    {
        int experienceCarryOver = playerExp - experienceRequired;
        playerExp = experienceCarryOver;
        playerLevel++;
        abilityPoints++;
        experienceRequired = (int)(experienceRequired * 1.25);
    }

    /*
     * Checks to see if the player is logged in.
     * If they are report a progress of 1 for the 100 enemies killed achievement.
     * */
    public void enemyKilledAchievement()
    {
        if (Social.localUser.authenticated)
        {
            PlayGamesPlatform.Instance.IncrementAchievement("CgkIpKjLyoEdEAIQCQ", 1, (bool success) =>
            {
            });
        }
    }

    /*
     * Convert byte array to a PlayerData object.
     * */
    public PlayerData FromBytes(byte[] data)
    {
        BinaryFormatter bf = new BinaryFormatter();
        MemoryStream ms = new MemoryStream();
        ms.Write(data, 0, data.Length);
        ms.Seek(0, SeekOrigin.Begin);
        playerData = (PlayerData)bf.Deserialize(ms);

        ms.Close();

        return playerData;
    }

    /*
     * Convert a PlayerData object to a byte array.
     * */
    public byte[] ToBytes(PlayerData thePlayerData)
    {
        BinaryFormatter bf = new BinaryFormatter();
        MemoryStream ms = new MemoryStream();

        //Serialise playerData.
        bf.Serialize(ms, thePlayerData);
        byte[] cloudData = ms.ToArray();

        ms.Close();

        return cloudData;
    }
}

/*
 * Serialisable PlayerData class used for persisting game attributes.
 * */
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
    public int experienceRequired;
    public bool vibrateOn;
    public int selectedCharacter;
}
