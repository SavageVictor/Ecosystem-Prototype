using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class GameManager : MonoBehaviour
{
    public int score;
    public bool plantFullyGrown;
    
    [SerializeField] private GameObject gameOverScreen;

    [SerializeField] private TextMeshProUGUI scoreCounter;
    void FixedUpdate()
    {
        if (plantFullyGrown)
        {
            gameOverScreen.SetActive(true);
            Time.timeScale = 0f;
        }
        
        scoreCounter.text = "" + score;
    }
}
