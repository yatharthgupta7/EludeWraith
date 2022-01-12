using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DeathMenu : MonoBehaviour
{
    [SerializeField] Text scoreText;
    [SerializeField] Image backgroudImg;
    [SerializeField] UnityAds unityAd;
    bool isShown = false;
    float transition = 0.0f;
    void Start()
    {
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (!isShown)
            return;

        transition += Time.deltaTime;
        backgroudImg.color = Color.Lerp(new Color(0, 0, 0, 0), Color.black, transition);
    }

    public void ToggleEndMenu(float score)
    {
        gameObject.SetActive(true);
        scoreText.text = ((int)score).ToString();
        isShown = true;
    }

    public void GetDoubleCoins()
    {
        unityAd.ShowRewarded();
    }

    public void Restart()
    {
        unityAd.ShowInterstitial();
        /*
        if(!PlayerPrefs.HasKey("Ads"))
        {
            PlayerPrefs.SetInt("Ads", 1);
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        else if (PlayerPrefs.GetInt("Ads")<3)
        {
            int pref = PlayerPrefs.GetInt("Ads");
            PlayerPrefs.SetInt("Ads", pref++);
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        else if(PlayerPrefs.GetInt("Ads")>=3)
        {
            PlayerPrefs.SetInt("Ads", 1);
        }*/
    }

    public void ToMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
