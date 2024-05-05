using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViajeRapidoScript : MonoBehaviour
{
    // Start is called before the first frame update
     private InGameIU iu;
    [SerializeField] private GameObject dark;
    [SerializeField] private GameObject color;
    [SerializeField] private GameObject tecla;
    [SerializeField] private string key;
    [SerializeField] private string value;
    private Inventory inv;
    private GameObject cont;
    private bool descubierto;
    private bool near;
    void Start()
    {
        cont = GameObject.FindGameObjectWithTag("GameController");
        iu = cont.GetComponent<InGameIU>();
        inv = cont.GetComponent<Inventory>();

        tecla.SetActive(false);
        if (inv.teleportDict.ContainsKey(key))
        {
            dark.SetActive(false);
            descubierto = true;
        }
        else
        {
            color.SetActive(false);
            descubierto = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (near && Input.GetButtonDown("Interactuar"))
        {
            iu.OnViajeRapido();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (!descubierto)
            {
                inv.teleportDict.Add(key, value);
                inv.UpdateTeleports();
                color.SetActive(true);
                dark.SetActive(false);
                descubierto = true;
            }
            near = true;
            tecla.SetActive(true);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            near = false;
            tecla.SetActive(false);
        }
    }
}
