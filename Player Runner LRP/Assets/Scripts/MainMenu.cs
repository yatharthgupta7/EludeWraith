using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] Text highScoreText;
    [SerializeField] Text potionText;
    [SerializeField] GameObject loadingScreen;
    [SerializeField] Slider slider;
    [SerializeField] Text progressText;
    UnityAds unityAd;
    void Start()
    {
        highScoreText.text = "HIGHSCORE - " + ((int)PlayerPrefs.GetFloat("HighScore")).ToString();
        potionText.text = "- " + ((int)PlayerPrefs.GetFloat("Potion")).ToString();
        unityAd = FindObjectOfType<UnityAds>().GetComponent<UnityAds>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ToGame()
    {
        StartCoroutine(LoadAsynchronously("Game"));
    }
    IEnumerator LoadAsynchronously(string scene)
    {
     loadingScreen.SetActive(true);
        AsyncOperation operation = SceneManager.LoadSceneAsync(scene);
        while (!operation.isDone)
        {
            float progress = (operation.progress /.9f);
            slider.value = progress;
            progressText.text = ((int)progress * 100f) + " %";
            yield return null;
        }
    }

    public void PlayerSelection()
    {
        SceneManager.LoadScene("PlayerSelection");
    }
}
