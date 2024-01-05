using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseGameScript : MonoBehaviour
{
    [SerializeField] GameObject ui;
    [SerializeField] Animator transition;

    [SerializeField] float LoadTime = 2f;
    public string MainMenu = "MainMenu";

    AudioManager audioManager;

    private void Start()
    {
        audioManager = FindFirstObjectByType<AudioManager>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
        {
            Toggle();
        }
    }

    public void Toggle()
    {
        ui.SetActive(!ui.activeSelf);
        audioManager.Play("Click");
        if (ui.activeSelf)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
        }
    }
    public void retry()
    {
        Toggle();
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().name));
    }

    public void menu()
    {
        Time.timeScale = 1f;
        Toggle();
        StartCoroutine(LoadLevel(MainMenu));
    
    }

    IEnumerator LoadLevel(string LevelName)
    {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(LoadTime);
        SceneManager.LoadScene(LevelName);
    }
}
