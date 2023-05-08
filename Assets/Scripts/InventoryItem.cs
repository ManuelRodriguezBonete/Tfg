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

    [SerializeField] Inventory inventory;
    void Start()
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

    // Update is called once per frame
    void Update()
    {
        
    }
}
