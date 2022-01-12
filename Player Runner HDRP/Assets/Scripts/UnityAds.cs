using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UnityAds : MonoBehaviour, IUnityAdsListener
{
    private string gameID = "4127723";
    [SerializeField]MainMenu mainMenu;
    private string bannerID = "Banner_Android";
    private string interstitialID ="Interstitial_Android";
    private string rewardedID = "Rewarded_Android";

    public bool TestMode;
    void Start()
    {
        Advertisement.Initialize(gameID, TestMode);
        Advertisement.AddListener(this);
        ShowBanner();
        //mainMenu = FindObjectOfType<MainMenu>().GetComponent<MainMenu>();
    }
    public void ShowInterstitial()
    {
        if(Advertisement.IsReady(interstitialID))
        {
            Advertisement.Show(interstitialID);
        }
    }

    public void HideBanner()
    {
        Advertisement.Banner.Hide();
    }

    public void ShowRewarded()
    {
        Advertisement.Show(rewardedID);
    }

    public void ShowBanner()
    {
        Advertisement.Banner.SetPosition(BannerPosition.BOTTOM_CENTER);
        Advertisement.Banner.Show(bannerID);
    }


    public void OnUnityAdsReady(string placementID)
    {
        if(placementID==bannerID)
        {
            Advertisement.Banner.SetPosition(BannerPosition.BOTTOM_CENTER);
            Advertisement.Banner.Show(bannerID);
        }
    }

    public void OnUnityAdsDidError(string message)
    {

    }

    public void OnUnityAdsDidStart(string placementID)
    {

    }

    public void OnUnityAdsDidFinish(string placementID,ShowResult showResult)
    {
        if(placementID==rewardedID)
        {
            if(showResult==ShowResult.Finished)
            {
                GameManager.instance.Death();
                SceneManager.LoadScene("Menu");
            }
            else if (showResult==ShowResult.Skipped)
            {
                SceneManager.LoadScene("Menu");
            }
            else if (showResult==ShowResult.Failed)
            {
                SceneManager.LoadScene("Menu");
            }
        }

        if(placementID==interstitialID)
        {
            if (showResult == ShowResult.Finished)
            {
                SceneManager.LoadScene("Game");
            }
            else if (showResult == ShowResult.Skipped)
            {
                SceneManager.LoadScene("Game");
            }
            else if (showResult == ShowResult.Failed)
            {
                SceneManager.LoadScene("Game");
            }
        }
    }
}
