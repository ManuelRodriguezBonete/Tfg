using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] private IOController ioController;
    public List<string> skillList = new List<string>();
    public List<string> collectableList= new List<string>();
    public int numCollectables = 0;

    [SerializeField] List<InventoryItem> skillsInventoryItems = new List<InventoryItem>();
    
    void Awake()
    {
        skillList = ioController.ReadSkills();
        for (int i = 0; i < skillsInventoryItems.Count; i++)
        {
            skillsInventoryItems[i].UpdateInventory();
        }
    }

    // Update is called once per frame
    public void UpdateSkills()
    {
        for (int i = 0; i < skillsInventoryItems.Count; i++)
        {
            skillsInventoryItems[i].UpdateInventory();
        }
    }

    public void SaveSkills()
    {
        ioController.WriteSkills(skillList);
    }
}
