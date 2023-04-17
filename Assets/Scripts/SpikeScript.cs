using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeScript : MonoBehaviour
{
    [SerializeField] DeathControllerScript controller;
    public int spikeKills;
    // Start is called before the first frame update

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) 
        {
            controller.KillPlayer("StaticSpike");
            controller.WriteDeaths();
        }
    }
}
