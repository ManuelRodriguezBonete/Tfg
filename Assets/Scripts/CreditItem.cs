using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditItem : MonoBehaviour
{
    [SerializeField] GameObject texto;
    [SerializeField] GameObject keybind;

    public bool cerca = false;
    public bool activo = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (cerca)
        {
            texto.SetActive(true);
        }
        else if (!cerca)
        {
            texto.SetActive(false);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //texto.SetActive(true);
        cerca = true;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        //texto.SetActive(false);
        cerca = false;
    }
}
