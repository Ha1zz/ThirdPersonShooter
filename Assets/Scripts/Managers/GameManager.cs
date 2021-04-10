using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class GameManager : MonoBehaviour
{
    //public GameObject winPanel;
    //public GameObject losePanel;

    //public int time = 20;
    //public TMP_Text countDownText;

    public static GameManager Instance { get; private set; }

    public bool CursorActive { get; private set; } = true;

    public string SelectedSaveName { get; set; } = "";

    private void Awake()
    {
        //if (winPanel) winPanel.SetActive(false);
        //if (losePanel) losePanel.SetActive(false);
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
        AppEvents.Invoke_OnMouseCursorEnable(false);
    }

    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Alpha9))
        //{
        //    PauseManager.Instance.PauseGame();
        //    losePanel.SetActive(true);
        //}
        //if (time <= 0)
        //{
        //    Win();
        //}

    }

    public void Start()
    {
        //StartCoroutine(CountDownOne());
    }

    private void EnableCursor(bool enable)
    {
        if (enable)
        {
            CursorActive = true;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            CursorActive = false;
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    private void OnEnable()
    {
        AppEvents.MouseCursorEnabled += EnableCursor;
    }

    private void OnDisable()
    {
        AppEvents.MouseCursorEnabled -= EnableCursor;
    }

    public void SetActiveSave(string saveName)
    {
        if (string.IsNullOrEmpty(saveName)) return;

        SelectedSaveName = saveName;
    }

    //IEnumerator CountDownOne()
    //{
    //    time--;
    //    countDownText.text = time.ToString();
    //    yield return new WaitForSeconds(1.0f);
    //    StartCoroutine(CountDownOne());
    //}

    //public void Win()
    //{
    //    PauseManager.Instance.PauseGame();
    //    winPanel.SetActive(true);
    //}
    //public void Lose()
    //{
    //    PauseManager.Instance.PauseGame();
    //    losePanel.SetActive(true);
    //}
}
