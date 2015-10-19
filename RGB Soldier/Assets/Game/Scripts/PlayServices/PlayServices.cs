using UnityEngine;
using System.Collections;
using GooglePlayGames.BasicApi;
using GooglePlayGames;
using GooglePlayGames.BasicApi.SavedGame;

public class PlayServices : MonoBehaviour {

    /*
     * Create the Play Games Client and configure it to enable saved game.
     * Authenticate the player's Google account.
     * */
    void Awake()
    {
        PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder().EnableSavedGames().Build();

        PlayGamesPlatform.InitializeInstance(config);

        PlayGamesPlatform.Activate();

        Social.localUser.Authenticate((bool success) =>
        {
        });
    }

    /*
     * Shows the achievements interface.
     * */
    public void showAchievements()
    {
        Social.ShowAchievementsUI();
    }

    /*
     * Shows the saved game interface.
     * */
    public void showSavedGame()
    {
        uint maxNumToDisplay = 5;
        bool allowCreateNew = false;
        bool allowDelete = true;

        ISavedGameClient savedGameClient = PlayGamesPlatform.Instance.SavedGame;
        savedGameClient.ShowSelectSavedGameUI("Select Saved Game", maxNumToDisplay, allowCreateNew, allowDelete, OnSavedGameSelected);
    }

    /*
     * Cloud loads the selected saved game.
     * */
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
