using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeSceneObject : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private string sceneNameObjective;
    [SerializeField] private int cameraPoint;
    [SerializeField] private int cameraSize;
    [SerializeField] private float playerX;
    [SerializeField] private float playerY;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (SceneManager.GetActiveScene().name != "Creditos")
        {
            PlayerPrefs.SetString("Level", sceneNameObjective);
            PlayerPrefs.SetInt("CameraPoint", cameraPoint);
            PlayerPrefs.SetInt("CameraSize", cameraSize);
            PlayerPrefs.SetFloat("Player X", playerX);
            PlayerPrefs.SetFloat("Player Y", playerY);
            PlayerPrefs.Save();
        }
        SceneManager.LoadScene(sceneNameObjective);
    }
}
