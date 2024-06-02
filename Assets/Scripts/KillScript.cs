using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillScript : MonoBehaviour
{
    private DeathControllerScript controller;
    private CameraController camera_controller;
    [SerializeField] string key;
    // Start is called before the first frame update

    private void Start()
    {
        controller = FindObjectOfType<DeathControllerScript>();
        camera_controller = FindObjectOfType<CameraController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) 
        {
            camera_controller.ResetPosKey();
            controller.KillPlayer(key);
        }
    }
}
