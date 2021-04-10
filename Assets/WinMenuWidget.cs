using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class WinMenuWidget : GameHUDWidget
{
    [SerializeField] private string MainMenuSceneName;
    [SerializeField] private string GameSceneName;

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            ReplayGame();
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

    public void ReplayGame()
    {
        SceneManager.LoadScene(GameSceneName);
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
