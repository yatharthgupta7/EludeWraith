using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CharacterListLocked : MonoBehaviour
{
    [SerializeField] Text potionText;
    GameObject[] characterList;
    [SerializeField] GameObject buy;
    [SerializeField]Button locked;
    [SerializeField] Text buyText;
    [SerializeField] GameObject loadingScreen;
    [SerializeField] Slider slider;
    [SerializeField] Text progressText;
    AudioSource audio;
    [SerializeField] AudioClip pay;
    int index;
    int GrannyAmt =2500;
    void Start()
    {
        index = PlayerPrefs.GetInt("CharacterSelected");
        PlayerPrefs.SetFloat("Ninja", 1.0f);
        potionText.text = "- " + ((int)PlayerPrefs.GetFloat("Potion")).ToString();
        audio = GetComponent<AudioSource>();
        if(!PlayerPrefs.HasKey("Granny"))
        {
            PlayerPrefs.SetFloat("Granny", 0.0f);
            index = 0;
        }

        characterList = new GameObject[transform.childCount];
        for (int x = 0; x < transform.childCount; x++)
        {
            characterList[x] = transform.GetChild(x).gameObject;
        }

        foreach (GameObject go in characterList)
        {
            go.SetActive(false);
        }

        if (characterList[index])
        {
            characterList[index].SetActive(true);
        }

    }


    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator LoadAsynchronously(string scene)
    {
        loadingScreen.SetActive(true);
        AsyncOperation operation = SceneManager.LoadSceneAsync(scene);
        while (!operation.isDone)
        {
            float progress = (operation.progress / .9f);
            slider.value = progress;
            progressText.text = ((int)progress * 100f) + " %";
            yield return null;
        }
    }

    public void ToggleLeft()
    {
        characterList[index].SetActive(false);

        index--;
        if (index < 0)
        {
            index = characterList.Length - 1;
        }

        characterList[index].SetActive(true);
        if(index==1 && (PlayerPrefs.GetFloat("Granny")==0.0f))
        {
            locked.interactable = true;//Enale Button
            buy.SetActive(true);
        }
        else if(index!=1)
        {
            //Disable Button
            locked.interactable = false;
            buy.SetActive(false);
        }
    }
    public void ToggleRight()
    {
        characterList[index].SetActive(false);

        index++;
        if (index == characterList.Length)
        {
            index = 0;
        }

        characterList[index].SetActive(true);
        if (index == 1 && (PlayerPrefs.GetFloat("Granny") == 0.0f))
        {
            //Enable Button
            locked.interactable = true;
            buy.SetActive(true);
        }
        else if (index != 1)
        {
            //Disable Button
            locked.interactable = false;
            buy.SetActive(false);
        }
    }

    public void Buy()
    {
        float potion = PlayerPrefs.GetFloat("Potion");
        if(potion>=GrannyAmt)
        {
            PlayerPrefs.SetFloat("Granny", 1.0f);
            PlayerPrefs.SetFloat("Potion", potion - GrannyAmt);

            potionText.text = "- " + ((int)PlayerPrefs.GetFloat("Potion")).ToString();
            //Disable buy button
            locked.interactable = false;
            buy.SetActive(false);
            audio.PlayOneShot(pay);
        }
    }

    public void ConfirmButton()
    {
        if(index==1 && PlayerPrefs.GetFloat("Granny")==1.0f)
        {
            PlayerPrefs.SetInt("CharacterSelected", index);
            StartCoroutine(LoadAsynchronously("Game"));
        }
        else if(index==0)
        {
            PlayerPrefs.SetInt("CharacterSelected", index);
            StartCoroutine(LoadAsynchronously("Game"));
        }

    }

    public void Back()
    {
        SceneManager.LoadScene("Menu");
    }
}
