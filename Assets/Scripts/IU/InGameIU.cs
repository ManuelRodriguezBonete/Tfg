using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InGameIU : MonoBehaviour
{
    [SerializeField] DeathControllerScript deathController;
    [SerializeField] InGameController inGameController;

    [SerializeField] GameObject pauseMenu;
    private bool onPause = false;

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
        SceneManager.LoadScene(1);
    }
    public void OnPauseMenu()
    {
        pauseMenu.SetActive(true);
        inGameController.StopGame();
        onPause = true;
    }
    public void OffPauseMenu()
    {
        pauseMenu.SetActive(false);
        inGameController.ReanudeGame();
        onPause = false;
    }
}
