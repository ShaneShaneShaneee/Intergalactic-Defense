using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public Animator transition;

    public string LevelSelect = "LevelSelect";
    public float LoadTime = 2f;

    [SerializeField] GameObject newGameUI;

    AudioManager audioManager;

    private void Start()
    {
        audioManager = FindFirstObjectByType<AudioManager>();
    }

    // Start is called before the first frame update
    public void Play()
    {
        audioManager.Play("Click");
        LoadNextLevel();
    }

    public void NewGame()
    {
        audioManager.Play("Click");
        newGameUI.SetActive(true);
    }

    public void Yes()
    {
        audioManager.Play("Click");
        PlayerPrefs.SetInt("levelReached", 1);
        LoadNextLevel();
        newGameUI.SetActive(false);
    }

    public void No()
    {
        audioManager.Play("Click");
        newGameUI.SetActive(false);
    }

    // Update is called once per frame
    public void Quit()
    {
        audioManager.Play("Click");
        Application.Quit();
    }

    void LoadNextLevel()
    {
        StartCoroutine(LoadLevel(LevelSelect));
    }

    public void LoadLevelSelected(string level)
    {
        StartCoroutine(LoadLevel(level));
    }

    IEnumerator LoadLevel(string LevelName)
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(LoadTime);

        SceneManager.LoadScene(LevelName);
    }
}
