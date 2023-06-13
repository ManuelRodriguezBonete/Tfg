using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static UnityEditor.Experimental.GraphView.GraphView;

public class InGameIU : MonoBehaviour
{
    [SerializeField] GameObject player; 
    private Camera camera;
    private CameraController camController;

    [SerializeField] DeathControllerScript deathController;
    [SerializeField] InGameController inGameController;
    [SerializeField] Inventory inventory;

    [SerializeField] GameObject teleportMenu;

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

    [SerializeField] Button aux1;
    [SerializeField] Button auxiliarButtonTextFound;

    [SerializeField] Text notaSecretaNumero;
    [SerializeField] Text contenidoNota;

    private bool onPause = false;
    private Color fadeColor;

    void Start()
    {
        onPause = false;
        camera = Camera.main;
        camController = camera.GetComponent<CameraController>();
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
            OffTextFound();
        }
    }
    public void GoInitialMenu()
    {
        SaveData();
        Time.timeScale = 1;
        SceneManager.LoadScene("InitialMenu");
    }
    private void SaveData()
    {
        //Guardamos las muertes
        deathController.WriteDeaths();
        //Guardamos las skills
        player.GetComponent<PlayerMovement>().WriteSkills();
        //Guardamos la información del nivel actual
        if (SceneManager.GetActiveScene().name != "Creditos")
        {
            PlayerPrefs.SetString("Level", SceneManager.GetActiveScene().name);
            PlayerPrefs.SetInt("CameraPoint", camController.GetCameraPoint());
            PlayerPrefs.SetFloat("CameraSize", camController.GetSize());
            Vector3 aux = deathController.GetSpawnPoint();
            if (aux.x == 0 && aux.y == 00)
            {
                PlayerPrefs.SetFloat("Player X", player.transform.position.x);
                PlayerPrefs.SetFloat("Player Y", player.transform.position.y);
            }
            else
            {
                PlayerPrefs.SetFloat("Player X", aux.x);
                PlayerPrefs.SetFloat("Player Y", aux.y);
            }
            
            PlayerPrefs.Save();
        }
        
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
            auxiliarButtonTextFound.Select();
        }
        
    }
    public void OffTextFound()
    {
        //inGameController.ReanudeGame();
        FoundTextMenu.SetActive(false);
        onPause = false;
        inventarioButton.Select();
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

    public void OnViajeRapido(string key)
    {
        if (inventory.teleportDict.TryGetValue(key, out string value))
        {
            Debug.Log(value);
            string[] cadena = value.Split('_');
            PlayerPrefs.SetString("Level", "Level " + cadena[0]);
            PlayerPrefs.SetInt("CameraPoint", Convert.ToInt32(cadena[1]));
            PlayerPrefs.SetFloat("CameraSize", Convert.ToInt32(cadena[2]));
            PlayerPrefs.SetFloat("Player X", Convert.ToSingle(cadena[3]));
            PlayerPrefs.SetFloat("Player Y", Convert.ToSingle(cadena[4]));
            PlayerPrefs.Save();
            teleportMenu.SetActive(false);
            inGameController.ReanudeGame();
            deathController.WriteDeaths();
            player.GetComponent<PlayerMovement>().WriteSkills();
            SceneManager.LoadScene("Level " + cadena[0]);
        }
    }
    public void OnViajeRapido()
    {
        inGameController.StopGame();
        teleportMenu.SetActive(true);
        aux1.Select();
    }
    public void OffViajeRapido()
    {
        inGameController.ReanudeGame();
        teleportMenu.SetActive(false);
    }
}
