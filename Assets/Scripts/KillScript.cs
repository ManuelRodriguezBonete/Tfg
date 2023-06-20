using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillScript : MonoBehaviour
{
    private DeathControllerScript controller;
    [SerializeField] string key;
    // Start is called before the first frame update

    private void Start()
    {
        controller = FindObjectOfType<DeathControllerScript>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) 
        {
            controller.KillPlayer(key);
        }
    }
}
