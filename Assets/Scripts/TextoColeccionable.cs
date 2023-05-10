using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextoColeccionable : MonoBehaviour
{
    [SerializeField] GameObject ayuda;
    [SerializeField] InGameIU iucontroller;
    [SerializeField] string contenido;
    [SerializeField] int numerotexto;
    private bool active = false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1") && active)
        {
            //Pausar juego
            iucontroller.OnTextFound(contenido, numerotexto);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        ayuda.SetActive(true);
        active = true;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        ayuda.SetActive(false);
        active = false;
    }
}
