using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public int potion;
    [SerializeField] Text potionText;
    [SerializeField] AudioClip potionCollect;
    AudioSource audioSource;
    bool isDead = false;
    private void Awake()
    {
        if(instance==null)
        {
            instance = this;
        }
    }
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void IncrementPotion()
    {
        potion++;
        potionText.text = "Potion - " + potion;
        audioSource.PlayOneShot(potionCollect);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void Death()
    {
        isDead = true;
        float p = PlayerPrefs.GetFloat("Potion");
        p += potion;
        PlayerPrefs.SetFloat("Potion", p);
    }
}
