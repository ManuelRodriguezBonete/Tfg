using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorMechanismScript : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject doorClosed;
    [SerializeField] private GameObject doorOpened;

    [SerializeField] private GameObject keyON;
    [SerializeField] private GameObject keyOFF;

    [SerializeField] private bool closed;
    [SerializeField] private Collider2D colDoor;

    private bool keydown;
    private bool col = false;
    
    void Update()
    {
        keydown = Input.GetKeyDown(KeyCode.E);
        
        if (keydown && col) {
            if (closed)
            {
                OpenMechanism();
            }
            else
            {
                CloseMechanism();
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            keyON.SetActive(true);
            keyOFF.SetActive(true);
            col = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            keyON.SetActive(false);
            keyOFF.SetActive(false);
            col = false;
        }
        
    }
    private void OpenMechanism()
    {
        doorClosed.SetActive(false);
        doorOpened.SetActive(true);
        colDoor.enabled = false;
        closed = false;
    }
    private void CloseMechanism()
    {
        doorClosed.SetActive(true);
        doorOpened.SetActive(false);
        colDoor.enabled = true;
        closed = true;
    }
}
