using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PauseMenuWidget : GameHUDWidget
{
    [SerializeField] private string MainMenuSceneName;

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            ResumeGame();
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            ReturnToMainMenu();
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            QuitApplication();
        }
    }

    public void Awake()
    {
        
    }

    public void ResumeGame()
    {
        PauseManager.Instance.UnPauseGame();
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene(MainMenuSceneName);
    }

    public void QuitApplication()
    {
        Application.Quit();
    }

}
