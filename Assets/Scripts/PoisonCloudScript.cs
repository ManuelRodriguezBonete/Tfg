using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class PoisonCloudScript : MonoBehaviour
{
    [SerializeField] ControllerScript controller;
    [SerializeField] private float initialTimer;
    private float timer;
    // Start is called before the first frame update
    void Start()
    {
        ResetTimer();
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                controller.KillPlayer();
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        ResetTimer();   
    }
    private void ResetTimer()
    {
        timer = initialTimer;
    }
}
