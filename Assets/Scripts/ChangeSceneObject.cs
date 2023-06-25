using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeSceneObject : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private string sceneNameObjective;
    [SerializeField] private int cameraPoint;
    [SerializeField] private float cameraSize;
    [SerializeField] private float playerX;
    [SerializeField] private float playerY;
    private InGameIU iu;
    void Start()
    {
        iu = FindObjectOfType<InGameIU>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (SceneManager.GetActiveScene().name != "Creditos" && SceneManager.GetActiveScene().name != "Estadísticas")
        {
            iu.SaveData();
            PlayerPrefs.SetString("Level", sceneNameObjective);
            PlayerPrefs.SetInt("CameraPoint", cameraPoint);
            PlayerPrefs.SetFloat("CameraSize", cameraSize);
            PlayerPrefs.SetFloat("Player X", playerX);
            PlayerPrefs.SetFloat("Player Y", playerY);
            PlayerPrefs.Save();
        }
        SceneManager.LoadScene(sceneNameObjective);
    }
}
