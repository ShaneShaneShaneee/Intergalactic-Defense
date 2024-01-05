using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelecter : MonoBehaviour
{
    public MainMenu _LevelLoader;

    [SerializeField] Button[] levelButtons;

    AudioManager audioManager;

    private void Start()
    {
        audioManager = FindFirstObjectByType<AudioManager>();
        int levelReached = PlayerPrefs.GetInt("levelReached", 1);
        for (int i = 0; i < levelButtons.Length; i++)
        {
            if(i + 1 > levelReached) levelButtons[i].interactable = false;
        }
    }

    public void Back()
    {
        audioManager.Play("Click");
        _LevelLoader.LoadLevelSelected("MainMenu");
    }

    public void Select(string levelName)
    {
        audioManager.Play("Click");
        _LevelLoader.LoadLevelSelected(levelName);
    }
}
