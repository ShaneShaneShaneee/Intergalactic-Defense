using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverScript : MonoBehaviour
{
    [SerializeField] Animator transition;

    [SerializeField] float LoadTime = 2f;

    public string MainMenu = "MainMenu";

    AudioManager audioManager;

    private void Start()
    {
        audioManager = FindFirstObjectByType<AudioManager>();
    }

    public void Retry()
    {
        audioManager.Play("Click");
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().name));
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
