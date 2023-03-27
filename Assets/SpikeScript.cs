using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) 
        {
            //Implementar a futuro el metodo para que el player reaparezca en el inicio de la sala que esté
            //Quitar 1 vida, ver el numero de vidas que le quedan y si es 0 volver a X sitio
            Debug.Log("Hit");
        }
    }
}
