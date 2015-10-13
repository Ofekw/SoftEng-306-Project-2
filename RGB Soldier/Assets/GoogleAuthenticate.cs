using UnityEngine;
using System.Collections;
using GooglePlayGames.BasicApi;
using GooglePlayGames;

public class GoogleAuthenticate : MonoBehaviour
{

    // Use this for initialization
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
        if (Social.localUser.authenticated)
        {
            Social.ShowAchievementsUI();
        }
    }
}
