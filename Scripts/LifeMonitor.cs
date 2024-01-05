using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeMonitor : MonoBehaviour
{
    [HideInInspector]
    public static bool gameEnded;

    [SerializeField] GameObject gameOverUI;
    [SerializeField] GameObject nextLevelUI;
    // Start is called before the first frame update
    void Start()
    {
        gameEnded = false;  
    }

    // Update is called once per frame
    void Update()
    {
        if (gameEnded) return;

        if(PlayerStats.lives <= 0)
        {
            EndGame();
        }
    }

    void EndGame()
    {
        gameEnded = true;

        gameOverUI.SetActive(true);
    }

    public void WinLevel()
    {
        gameEnded = true;
        nextLevelUI.SetActive(true);
        PlayerPrefs.SetInt("levelReached", NextLevel.unlocked);
    }
}
