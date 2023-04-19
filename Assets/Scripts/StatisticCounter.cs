using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StatisticCounter : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI nombre_Enemigo;
    [SerializeField] private TextMeshProUGUI numero_Muertes;
    [SerializeField] private TextMeshProUGUI inter;
    [SerializeField] private Image icono_Enemigo;

    [SerializeField] string key;
    [SerializeField] InitialMenuController controller;

    private void Start()
    {
        if (controller.deathDict.TryGetValue(key, out var auxDeath) && key!= "Total")
        {
            nombre_Enemigo.text = key;
            numero_Muertes.text = auxDeath.ToString();
            inter.gameObject.SetActive(false);
            icono_Enemigo.gameObject.SetActive(true);
        }
    }

}
