using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterZoneScript : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] List<GameObject> updateablesItems= new List<GameObject>();
    private GameObject player;
    private int count = 0;
    private int aux = 0;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        
        count = updateablesItems.Count;
        for (int i = 0; i < count; i++)
        {
            updateablesItems[i].SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && count > 0 )
        {
            Debug.Log("enter");
            for (int i = 0; i < count; i++)
            {
                updateablesItems[i].SetActive(true);
                updateablesItems[i].GetComponent<RestartGameObjecto>().RestartPosition();
                
                
            }
            aux = 0;
        }


    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && count > 0)
        {
            for (int i = 0; i < count; i++)
            {
                updateablesItems[i].GetComponent<RestartGameObjecto>().RestartPosition();
                updateablesItems[i].SetActive(false);
                
            }
            aux = 0;
        }
    }


    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && aux == 0 && count > 0)
        {
            Debug.Log("enter");
            for (int i = 0; i < count; i++)
            {
                updateablesItems[i].SetActive(true);
            }
            aux = 1;
        }
    }
    public void RestartItems()
    {
        for (int i = 0; i < count; i++)
        {
            updateablesItems[i].SetActive(true);
            updateablesItems[i].GetComponent<RestartGameObjecto>().RestartPosition();
        }
    }
}
