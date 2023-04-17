using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnPointsScript : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] ControllerScript controller;
    void Start()
    {
        controller = GetComponent<ControllerScript>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Pos guardada -> x: " + collision.transform.position.x +" y: "+ collision.transform.position.y);
            controller.SavePoint(collision.transform.position);
        }
    }
}
