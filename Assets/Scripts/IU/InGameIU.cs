using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InGameIU : MonoBehaviour
{
    [SerializeField] DeathControllerScript deathController;

    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void GoInitialMenu()
    {
        deathController.WriteDeaths();
        SceneManager.LoadScene(1);
    }
}
