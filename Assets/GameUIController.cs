using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameUIController : MonoBehaviour
{
    [SerializeField] private GameHUDWidget GameCanvas;
    [SerializeField] private GameHUDWidget PauseCanvas;
    [SerializeField] private GameHUDWidget InventoryCanvas;

    public GameObject winPanel;
    public GameObject losePanel;

    public int time = 20;
    public TMP_Text countDownText;
    public static GameUIController Instance { get; private set; }


    private GameHUDWidget ActiveMenu;


    private void Awake()
    {
        if (winPanel) winPanel.SetActive(false);
        if (losePanel) losePanel.SetActive(false);
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        DisableAllMenu();
        EnableGameMenu();
        StartCoroutine(CountDownOne());
    }

    // Update is called once per frame
    void Update()
    {
        if (time <= 0)
        {
            Win();
        }
    }

    public void EnablePauseMenu()
    {
        if (ActiveMenu) ActiveMenu.DisableWidget();

        ActiveMenu = PauseCanvas;
        ActiveMenu.EnableWidget();
    }

    public void EnableGameMenu()
    {
        if (ActiveMenu) ActiveMenu.DisableWidget();

        ActiveMenu = GameCanvas;
        ActiveMenu.EnableWidget();
    }

    public void EnableInventoryMenu()
    {
        if (ActiveMenu) ActiveMenu.DisableWidget();

        ActiveMenu = InventoryCanvas;
        ActiveMenu.EnableWidget();
    }


    public void DisableAllMenu()
    {
        //GameCanvas.DisableWidget();
        PauseCanvas.DisableWidget();
        InventoryCanvas.DisableWidget();
    }

    IEnumerator CountDownOne()
    {
        time--;
        countDownText.text = time.ToString();
        yield return new WaitForSeconds(1.0f);
        StartCoroutine(CountDownOne());
    }

    public void Win()
    {
        PauseManager.Instance.PauseGame();
        winPanel.SetActive(true);
    }
    public void Lose()
    {
        PauseManager.Instance.PauseGame();
        losePanel.SetActive(true);
    }

}
