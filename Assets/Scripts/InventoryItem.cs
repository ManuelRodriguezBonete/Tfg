using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItem : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI nombreSkill;
    [SerializeField] private TextMeshProUGUI inter;
    [SerializeField] private Image icono_Skill;

    [SerializeField] string key;
    [SerializeField] string tipoObjeto;

    [SerializeField] Inventory inventory;

    
    void Start()
    {
        if (tipoObjeto == "skill")
        {
            for (int i = 0; i < inventory.skillList.Count; i++)
            {
                if (key == inventory.skillList[i])
                {
                    nombreSkill.text = key;
                    inter.gameObject.SetActive(false);
                    icono_Skill.gameObject.SetActive(true);
                }
            }
        }
        else if (tipoObjeto == "NotaSecreta")
        {
            int keyInt = Int32.Parse(key);
            if (inventory.notasSecretasDict.TryGetValue(keyInt, out var texto))
            {
                nombreSkill.text = "Nota nº: " + keyInt;
                inter.gameObject.SetActive(false);
                icono_Skill.gameObject.SetActive(true);
            }
        }
        
    }

}
