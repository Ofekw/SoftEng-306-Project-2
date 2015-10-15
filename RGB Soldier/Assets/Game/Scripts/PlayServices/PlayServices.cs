using UnityEngine;
using System.Collections;
using GooglePlayGames.BasicApi;
using GooglePlayGames;
using GooglePlayGames.BasicApi.SavedGame;

public class PlayServices : MonoBehaviour {

    void Awake()
    {
        PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder().EnableSavedGames().Build();

        PlayGamesPlatform.InitializeInstance(config);

        PlayGamesPlatform.Activate();

        Social.localUser.Authenticate((bool success) =>
        {
        });
    }

    public void showAchievements()
    {
        Social.ShowAchievementsUI();
    }

    public void showSavedGame()
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
            GameControl.control.DoLoadFromCloud();
        }
        else
        {
        }
    }
}
