using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterSelection : MonoBehaviour
{

    GameObject[] characterList;
    int index;
    void Start()
    {
        index = PlayerPrefs.GetInt("CharacterSelected");
        characterList = new GameObject[transform.childCount];
        for(int x=0;x<transform.childCount;x++)
        {
            characterList[x] = transform.GetChild(x).gameObject;
        }

        foreach(GameObject go in characterList)
        {
            go.SetActive(false);
        }

        if(characterList[index])
        {
            characterList[index].SetActive(true);
        }

    }


    // Update is called once per frame
    void Update()
    {
        
    }

    public void ToggleLeft()
    {
        characterList[index].SetActive(false);

        index--;
        if(index<0)
        {
            index = characterList.Length - 1;
        }

        characterList[index].SetActive(true);
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
    }

    public void ConfirmButton()
    {
        PlayerPrefs.SetInt("CharacterSelected", index);
        SceneManager.LoadScene("Game");
    }
}
