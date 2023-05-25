using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InGameIU : MonoBehaviour
{
    [SerializeField] GameObject player;

    [SerializeField] DeathControllerScript deathController;
    [SerializeField] InGameController inGameController;
    [SerializeField] Inventory inventory;

    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject inventarioMenu;
    [SerializeField] GameObject mapMenu;
    [SerializeField] GameObject ajustesMenu;
    [SerializeField] GameObject exitMenu;
    [SerializeField] GameObject FoundTextMenu;

    [SerializeField] Button inventarioButton;
    [SerializeField] Button mapButton;
    [SerializeField] Button ajustesButton;
    [SerializeField] Button exitButton;

    [SerializeField] Text notaSecretaNumero;
    [SerializeField] Text contenidoNota;

    private bool onPause = false;
    private Color fadeColor;

    void Start()
    {
        onPause = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Pausa") && onPause == false)
        {
            OnPauseMenu();
        }
        else if(Input.GetButtonDown("Pausa") && onPause == true)
        {
            OffPauseMenu();
        }
    }
    public void GoInitialMenu()
    {
        deathController.WriteDeaths();
        player.GetComponent<PlayerMovement>().WriteSkills();
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
    public void OnTextFound(string texto, int num)
    {
        contenidoNota.text = texto;
        notaSecretaNumero.text = num.ToString();
        inGameController.StopGame();
        FoundTextMenu.SetActive(true);
        onPause = true;
    }
    public void OnTextFound(int key)
    {
        
        if(inventory.notasSecretasDict.TryGetValue(key, out string value))
        {
            FoundTextMenu.SetActive(true);
            contenidoNota.text = value;
            notaSecretaNumero.text = key.ToString();
        }
        
    }
    public void OffTextFound()
    {
        inGameController.ReanudeGame();
        FoundTextMenu.SetActive(false);
        onPause = false;
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
