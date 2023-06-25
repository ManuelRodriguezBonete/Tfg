using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class TextoColeccionable : MonoBehaviour
{
    [SerializeField] GameObject ayuda;
    private InGameIU iucontroller;
    [SerializeField] string dirMemoria;
    [SerializeField] int numerotexto;
    private Inventory inventory;
    private GameObject cont;
    private bool active = false;
    void Start()
    {
        cont = GameObject.FindGameObjectWithTag("GameController");
        inventory = cont.GetComponent<Inventory>();
        iucontroller= cont.GetComponent<InGameIU>();
        
        dirMemoria = "Tfg_Data/Resources/Text/TextosNotas/" + dirMemoria + ".txt";
        Debug.Log(dirMemoria);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Interactuar") && active)
        {
            //Pausar juego
            string texto = File.ReadAllText(dirMemoria);
            iucontroller.OnTextFound(texto, numerotexto);
            if(!inventory.notasSecretasDict.TryGetValue(numerotexto, out string value))
            {
                inventory.notasSecretasDict.Add(numerotexto, texto);
                inventory.UpdateNotas();
            }
            
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
