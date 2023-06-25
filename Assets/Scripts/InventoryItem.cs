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

    private Inventory inventory;
    private GameObject cont;
    private bool unlocked = false;
    void Start()
    {
        cont = GameObject.FindGameObjectWithTag("GameController");
        inventory = cont.GetComponent<Inventory>();
        UpdateInventory();
    }

    public void UpdateInventory()
    {
        if (inventory!=null)
        {
            if (!unlocked && tipoObjeto == "Skill")
            {
                for (int i = 0; i < inventory.skillList.Count; i++)
                {
                    if (key == inventory.skillList[i])
                    {
                        nombreSkill.text = key;
                        inter.gameObject.SetActive(false);
                        icono_Skill.gameObject.SetActive(true);
                        unlocked = true;
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
            else if (tipoObjeto == "ViajeRapido")
            {
                if (inventory.teleportDict.ContainsKey(key))
                {
                    nombreSkill.text = key;
                    inter.gameObject.SetActive(false);
                }
            }
        }
        
        
    }


}
