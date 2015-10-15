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
    public int experienceRequired = 15;
    public int backgroundVolume = 100;
    public int soundBitsVolume = 100;
    public int colourMode;
    public PlayerData playerData;
    public GameObject lvlup;

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

        PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder().EnableSavedGames().Build();

        PlayGamesPlatform.InitializeInstance(config);

        PlayGamesPlatform.Activate();

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

    public void SaveToCloud()
    {
        if (Social.localUser.authenticated)
        {
            doSave = true;
            CloudSync();
        }
    }

    public void DoLoadFromCloud()
    {
        doSave = false;
        CloudSync();
    }

    public void CloudSync()
    {
        ISavedGameClient savedGameClient = PlayGamesPlatform.Instance.SavedGame;
        savedGameClient.OpenWithAutomaticConflictResolution("SavedGame", DataSource.ReadCacheOrNetwork, ConflictResolutionStrategy.UseLongestPlaytime, OpenCloudSave);
    }

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
        builder = builder.WithUpdatedPlayedTime(this.timePlayed).WithUpdatedDescription("Current Level: " + playerData.currentGameLevel + "Current Player Level: " + playerData.playerLevel);

        SavedGameMetadataUpdate updatedMetadata = builder.Build();
        savedGameClient.CommitUpdate(game, updatedMetadata, data, OnSaveWritten);
    }

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
    }

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
        experienceRequired = (playerData.experienceRequired != 0) ? playerData.experienceRequired : 15;
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
            levelAndCarryOver();
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

    public void enemyKilledAchievement()
    {
        if (Social.localUser.authenticated)
        {
            PlayGamesPlatform.Instance.IncrementAchievement("CgkIpKjLyoEdEAIQCQ", 1, (bool success) =>
            {
            });
        }
    }

    

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


    //Play Service Methods
    /*
    //Opening a saved game.
    public void OpenSavedGame()
    {
        string fileName = "SavedGame";
        ISavedGameClient savedGameClient = PlayGamesPlatform.Instance.SavedGame;
        savedGameClient.OpenWithAutomaticConflictResolution(fileName, DataSource.ReadCacheOrNetwork, ConflictResolutionStrategy.UseLongestPlaytime, OnSavedGameOpened);
    }

    public void OnSavedGameOpened(SavedGameRequestStatus status, ISavedGameMetadata game)
    {
        if (status == SavedGameRequestStatus.Success)
        {
            if (this.save)
            {
                SaveGame(game, cloudData, timePlayed);
            }
            else
            {
                LoadGameData(game);
            }
        }
        else
        {

        }
    }

    //Writing a saved game.
    public void SaveGame(ISavedGameMetadata game, byte[] savedData, TimeSpan totalPlaytime)
    {
        ISavedGameClient savedGameClient = PlayGamesPlatform.Instance.SavedGame;

        SavedGameMetadataUpdate.Builder builder = new SavedGameMetadataUpdate.Builder();

        builder = builder.WithUpdatedPlayedTime(totalPlaytime).WithUpdatedDescription("Game: str: " + playerData.playerStr + " agl: " + playerData.playerAgl + " cLevel: " + playerData.currentGameLevel + " level: " + playerData.playerLevel);

        SavedGameMetadataUpdate updatedMetadata = builder.Build();
        savedGameClient.CommitUpdate(game, updatedMetadata, savedData, OnSavedGameWritten);
    }

    public void OnSavedGameWritten(SavedGameRequestStatus status, ISavedGameMetadata game)
    {
        if (status == SavedGameRequestStatus.Success)
        {

        }
        else
        {

        }
    }

    //Reading saved game.
    public void LoadGameData(ISavedGameMetadata game)
    {
        ISavedGameClient savedGameClient = PlayGamesPlatform.Instance.SavedGame;
        savedGameClient.ReadBinaryData(game, OnSavedGameDataRead);
    }

    public void OnSavedGameDataRead(SavedGameRequestStatus status, byte[] data)
    {
        if (status == SavedGameRequestStatus.Success)
        {
            BinaryFormatter bf = new BinaryFormatter();
            MemoryStream ms = new MemoryStream();
            ms.Write(data, 0, data.Length);
            ms.Seek(0, SeekOrigin.Begin);
            playerData = (PlayerData)bf.Deserialize(ms);

            GameControl.control.playerLevel = playerData.playerLevel;
            GameControl.control.playerExp = playerData.playerExp;
            GameControl.control.playerStr = playerData.playerStr;
            GameControl.control.playerAgl = playerData.playerAgl;
            GameControl.control.playerDex = playerData.playerDex;
            GameControl.control.playerInt = playerData.playerInt;
            GameControl.control.playerVit = playerData.playerVit;
            GameControl.control.currentGameLevel = playerData.currentGameLevel;
            GameControl.control.abilityPoints = playerData.abilityPoints;
            GameControl.control.backgroundVolume = playerData.backgroundVolume;
            GameControl.control.soundBitsVolume = playerData.soundBitsVolume;
            GameControl.control.colourMode = playerData.colourMode;

            ms.Close();
        }
        else
        {

        }
    }

    //Display saved games.
    public void ShowSelectUI()
    {
        uint maxNumToDisplay = 5;
        bool allowCreateNew = false;
        bool allowDelete = true;

        ISavedGameClient savedGameClient = PlayGamesPlatform.Instance.SavedGame;
        savedGameClient.ShowSelectSavedGameUI("Select Saved Game", maxNumToDisplay, allowCreateNew, allowDelete, OnSavedGameSelected);
    }

    public void OnSavedGameSelected(SelectUIStatus status, ISavedGameMetadata game)
    {
        if (status == SelectUIStatus.SavedGameSelected)
        {

        }
        else
        {

        }
    }
     * */

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
    public int experienceRequired;
}
