using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InitialMenuController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject mainMenu;
    [SerializeField] GameObject statsMenu;
    [SerializeField] GameObject creditMenu;
    [SerializeField] GameObject settingsMenu;
    [SerializeField] IOController controller;
    [SerializeField] public Dictionary<string, int> deathDict;
    [SerializeField] private TextMeshProUGUI muertesTotales;

    [SerializeField] Button stat1;
    [SerializeField] Button main1;


    private void Awake()
    {
        OnMainMenu();
        deathDict = new Dictionary<string, int>();
        deathDict = controller.ReadDeaths();
        SetMuertesTotales();
    }

    private void SetMuertesTotales()
    {
        if (deathDict.TryGetValue("Total", out var totalDeaths))
        {
            muertesTotales.text += totalDeaths.ToString();
        }
    }

    public void GoGameScene()
    {
        SceneManager.LoadScene(PlayerPrefs.GetString("Level"));
    }
    public void CloseGame()
    {
        Application.Quit();
    }
    public void OnCredits()
    {
        SceneManager.LoadScene("Creditos");
    }
    public void OnMainMenu()
    {
        main1.Select();
        mainMenu.SetActive(true);
        settingsMenu.SetActive(false);
        statsMenu.SetActive(false);
        creditMenu.SetActive(false);
    }
    public void OnStatsMenu()
    {
        stat1.Select();
        mainMenu.SetActive(false);
        settingsMenu.SetActive(false);
        statsMenu.SetActive(true);
        creditMenu.SetActive(false);
    }
    public void OnSettingsMenu()
    {
        mainMenu.SetActive(false);
        settingsMenu.SetActive(true);
        statsMenu.SetActive(false);
        creditMenu.SetActive(false);
    }
}
