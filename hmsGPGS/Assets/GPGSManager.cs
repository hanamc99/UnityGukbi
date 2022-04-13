using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine.SocialPlatforms;

public class GPGSManager : MonoBehaviour
{
    public static GPGSManager instance;
    public bool isSigned = false;

    void Awake()
    {
        GPGSManager.instance = this;
    }

    public void Init()
    {
        PlayGamesPlatform.DebugLogEnabled = true;

        PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder().Build();
        PlayGamesPlatform.InitializeInstance(config);

        PlayGamesPlatform.Activate();
    }

    public void Authenticate()
    {
        // authenticate user:
        if (!isSigned)
        {
            PlayGamesPlatform.Instance.Authenticate(SignInInteractivity.CanPromptAlways, (result) =>
            {
                // handle results
                if (result == SignInStatus.Success)
                {
                    Debug.Log("Login successful!");
                    Social.ShowAchievementsUI();

                    //Social.ReportProgress("CgkIs5W699AJEAIQAQ", 100.0f, (bool success) => { });
                }
                else
                {
                    Debug.Log("Failed to sign in with Google Play Games.");
                }
            });
            isSigned = true;
        }
        else
        {
            PlayGamesPlatform.Instance.SignOut();
            Debug.Log("isSigned : " + Social.localUser.authenticated);
            isSigned = false;
        }

        // Social.localUser.Authenticate((bool success) =>
        // {
        //     if (success)
        //     {
        //         Debug.Log("Login successful!");
        //     }
        //     else
        //     {
        //         Debug.Log("Failed to sign in with Google Play Games.");
        //     }
        // });
    }

    public void IncrementAchivement()
    {
        PlayGamesPlatform.Instance.IncrementAchievement("CgkIs5W699AJEAIQAQ", 5, (bool success) => { 
                Social.ShowAchievementsUI();
        });
    }
}
