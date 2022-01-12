using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    float m_score;
    [SerializeField] Text scoreText;
    [SerializeField] DeathMenu deathMenu;

    int difficultyLevel = 1;
    int maxDifficultyLevel = 10;
    int scoreToNextLevel = 10;
    PlayerLogic playerLogic;
    bool isDead = false;
    void Start()
    {
        playerLogic = GetComponentInParent<PlayerLogic>();
    }
    void Update()
    {
        if (isDead)
        {
            return;
        }
        if (m_score >= scoreToNextLevel)
        {
            LevelUp();
        }
        m_score += Time.deltaTime*difficultyLevel;
        scoreText.text = ((int)m_score).ToString();
    }

    void LevelUp()
    {
        if(difficultyLevel==maxDifficultyLevel)
        {
            return;
        }
        scoreToNextLevel *= 2;
        difficultyLevel++;
        if(playerLogic)
        {
            playerLogic.SetSpeed(difficultyLevel);
        }
    }

    public void Death()
    {
        isDead = true;
        if(PlayerPrefs.GetFloat("HighScore")<m_score)
        {
            PlayerPrefs.SetFloat("HighScore", m_score);
        }
        deathMenu.ToggleEndMenu(m_score);
    }
}
