using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour
{
    [SerializeField] Animator transition;

    [SerializeField] float LoadTime = 2f;

    public string MainMenu = "MainMenu";

    [SerializeField] string nextLevel = "Level02";
    [SerializeField] public int levelToUnlock = 0;
    public static int unlocked;
    [SerializeField] MainMenu _LevelLoader;

    AudioManager audioManager;

    private void Start()
    {
        audioManager = FindFirstObjectByType<AudioManager>();
        unlocked = levelToUnlock;
    }

    public void Continue()
    {
        audioManager.Play("Click");
        //PlayerPrefs.SetInt("levelReached", levelToUnlock);
        _LevelLoader.LoadLevelSelected(nextLevel);
    }

    public void Menu()
    {
        audioManager.Play("Click");
        Time.timeScale = 1f;
        StartCoroutine(LoadLevel(MainMenu));
    }

    IEnumerator LoadLevel(string LevelName)
    {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(LoadTime);
        SceneManager.LoadScene(LevelName);
    }
}
