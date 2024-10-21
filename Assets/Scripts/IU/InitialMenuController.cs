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
    [SerializeField] GameObject settingsMenu;
    [SerializeField] GameObject borrarMenu;
    [SerializeField] GameObject borradoExitoso;
    [SerializeField] IOController controller;
    [SerializeField] public Dictionary<string, int> deathDict;
    [SerializeField] private TextMeshProUGUI muertesTotales;

    [SerializeField] Button stat1;
    [SerializeField] Button main1;
    [SerializeField] Button borrar1;
    [SerializeField] Button continuar;

    [SerializeField] Slider sliderSFX;
    [SerializeField] Slider sliderMusic;

    [SerializeField] AudioSource sfxSource;
    [SerializeField] AudioSource musicSource;

    private void Awake()
    {
        OnMainMenu();
        //deathDict = new Dictionary<string, int>();
        //deathDict = controller.ReadDeaths();
        //SetMuertesTotales();
    }

    //private void SetMuertesTotales()
    //{
    //    if (deathDict.TryGetValue("Total", out var totalDeaths))
    //    {
    //        muertesTotales.text += totalDeaths.ToString();
    //    }
    //}
    private void Start()
    {
        if (PlayerPrefs.HasKey("SFX_Vol")) sliderSFX.value = PlayerPrefs.GetFloat("SFX_Vol"); 
        else { sliderSFX.value = 0.5f; PlayerPrefs.SetFloat("SFX_Vol", sliderSFX.value);}
        if (PlayerPrefs.HasKey("Music_Vol")) sliderMusic.value = PlayerPrefs.GetFloat("Music_Vol");
        else { sliderMusic.value = 0.5f; PlayerPrefs.SetFloat("Music_Vol", sliderMusic.value); }
        SetVolumen();
    }
    public void DeleteALLDATA()
    {
        PlayerPrefs.DeleteAll();
        controller.DeleteAll();
        continuar.Select();
        borrarMenu.SetActive(false);
        borradoExitoso.SetActive(true);
    }
    public void GoGameScene()
    {
        if (PlayerPrefs.HasKey("Level"))
        {
            SceneManager.LoadScene(PlayerPrefs.GetString("Level"));
        }
        else
        {
            SceneManager.LoadScene("Level 1");
        }
        
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
        borrarMenu.SetActive(false);
        borradoExitoso.SetActive(false);
    }
    public void Continuar()
    {
        main1.Select();
        borradoExitoso.SetActive(false);
        mainMenu.SetActive(true);
    }
    public void OnBorrarDatos()
    {
        borrar1.Select();
        mainMenu.SetActive(false); 
        settingsMenu.SetActive(false);
        borrarMenu.SetActive(true);
    }
    public void OnStatsMenu()
    {
        SceneManager.LoadScene("Estadísticas");
    }
    public void OnSettingsMenu()
    {
        mainMenu.SetActive(false);
        settingsMenu.SetActive(true);
    }
    public void SetVolumen()
    {
        sfxSource.volume = sliderSFX.value;
        musicSource.volume = sliderMusic.value;
        if (PlayerPrefs.HasKey("Music_Vol")) PlayerPrefs.SetFloat("Music_Vol", sliderMusic.value);
        if (PlayerPrefs.HasKey("SFX_Vol")) PlayerPrefs.SetFloat("SFX_Vol", sliderSFX.value);
    }
}
