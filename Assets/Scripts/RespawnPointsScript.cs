using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnPointsScript : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] DeathControllerScript controller;
    void Start()
    {
        //controller = GetComponent<DeathControllerScript>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Pos guardada -> x: " + collision.transform.position.x +" y: "+ collision.transform.position.y);
            controller.SavePoint(collision.transform.position);
        }
    }
}
