using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InGameIU : MonoBehaviour
{
    [SerializeField] DeathControllerScript deathController;
    [SerializeField] InGameController inGameController;

    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject inventarioMenu;
    [SerializeField] GameObject mapMenu;
    [SerializeField] GameObject ajustesMenu;
    [SerializeField] GameObject exitMenu;

    [SerializeField] Button inventarioButton;
    [SerializeField] Button mapButton;
    [SerializeField] Button ajustesButton;
    [SerializeField] Button exitButton;

    private bool onPause = false;
    private Color fadeColor;

    void Start()
    {
        onPause = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && onPause == false)
        {
            OnPauseMenu();
        }
        else if(Input.GetKeyDown(KeyCode.Escape) && onPause == true)
        {
            OffPauseMenu();
        }
    }
    public void GoInitialMenu()
    {
        deathController.WriteDeaths();
        Time.timeScale = 1;
        SceneManager.LoadScene("InitialMenu");
    }
    public void OnPauseMenu()
    {
        pauseMenu.SetActive(true);
        inventarioMenu.SetActive(true);
        mapMenu.SetActive(false);
        ajustesMenu.SetActive(false);
        exitMenu.SetActive(false);
        inventarioButton.Select();
        inGameController.StopGame();
        onPause = true;
    }

    public void OffPauseMenu()
    {
        pauseMenu.SetActive(false);
        inGameController.ReanudeGame();
        onPause = false;
    }
    public void OnInventaryMenu()
    {
        inventarioMenu.SetActive(true);
        mapMenu.SetActive(false);
        ajustesMenu.SetActive(false);
        exitMenu.SetActive(false);
    }
    public void OnMapMenu()
    {
        inventarioMenu.SetActive(false);
        mapMenu.SetActive(true);
        ajustesMenu.SetActive(false);
        exitMenu.SetActive(false);
    }
    public void OnAjustesMenu()
    {
        inventarioMenu.SetActive(false);
        mapMenu.SetActive(false);
        ajustesMenu.SetActive(true);
        exitMenu.SetActive(false);
    }
    public void OnExitMenu()
    {
        inventarioMenu.SetActive(false);
        mapMenu.SetActive(false);
        ajustesMenu.SetActive(false);
        exitMenu.SetActive(true);
    }
}
