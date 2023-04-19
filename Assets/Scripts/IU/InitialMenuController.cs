using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InitialMenuController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject mainMenu;
    [SerializeField] GameObject statsMenu;
    [SerializeField] GameObject creditMenu;
    [SerializeField] GameObject settingsMenu;

    // Update is called once per frame
    void Update()
    {
        
    }
    public void GoGameScene()
    {
        SceneManager.LoadScene(0);
    }
    public void CloseGame()
    {
        Application.Quit();
    }
    public void OnCredits()
    {
        mainMenu.SetActive(false);
        settingsMenu.SetActive(false);
        statsMenu.SetActive(false);
        creditMenu.SetActive(true);
    }
    public void OnMainMenu()
    {
        mainMenu.SetActive(true);
        settingsMenu.SetActive(false);
        statsMenu.SetActive(false);
        creditMenu.SetActive(false);
    }

}
