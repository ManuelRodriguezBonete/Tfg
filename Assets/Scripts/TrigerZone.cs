using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrigerZone : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject spike;
    [SerializeField] private int fallingSpeed;
    [SerializeField] private Vector3 initialSpikePos;
    void Start()
    {
        Initialize();
    }

    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            //Trigger
            this.gameObject.SetActive(false);
            spike.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        }
    }
    private void Initialize()
    {
        this.gameObject.SetActive(true);
        initialSpikePos = spike.transform.position; 
        spike.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        spike.GetComponent<Rigidbody2D>().mass = fallingSpeed;
    }
}
