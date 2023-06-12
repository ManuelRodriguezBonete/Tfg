using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Rendering;

public class Inventory : MonoBehaviour
{
    [SerializeField] private IOController ioController;
    public List<string> skillList = new List<string>();
    public List<string> collectableList= new List<string>();

    public Dictionary<string, string> teleportDict = new Dictionary<string, string>();
    public Dictionary<int, string> notasSecretasDict = new Dictionary<int, string>();
    public Dictionary<string, bool> colectablesDict = new Dictionary<string, bool>();

    public int numCollectables = 0;

    [SerializeField] List<InventoryItem> skillsInventoryItems = new List<InventoryItem>();
    [SerializeField] List<InventoryItem> notasSecretasInventoryItems = new List<InventoryItem>();
    [SerializeField] List<InventoryItem> teleportsInventoryItems = new List<InventoryItem>();
    

    void Awake()
    {
        skillList = ioController.ReadSkills();
        notasSecretasDict = ioController.ReadNotasSecretas();
        colectablesDict = ioController.ReadColeccionables();
        teleportDict = ioController.ReadTeleports();
        numCollectables = colectablesDict.Count;

        for (int i = 0; i < skillsInventoryItems.Count; i++)
        {
            skillsInventoryItems[i].UpdateInventory();
        }
        for (int i = 0; i < notasSecretasInventoryItems.Count; i++)
        {
            notasSecretasInventoryItems[i].UpdateInventory();
        }
        for (int i = 0; i < teleportsInventoryItems.Count; i++)
        {
            teleportsInventoryItems[i].UpdateInventory();
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
    public void UpdateNotas()
    {
        for (int i = 0; i < notasSecretasInventoryItems.Count; i++)
        {
            notasSecretasInventoryItems[i].UpdateInventory();
        }
    }
    public void UpdateTeleports()
    {
        for (int i = 0; i < teleportsInventoryItems.Count; i++)
        {
            teleportsInventoryItems[i].UpdateInventory();
        }
    }
    public void SaveGame()
    {
        ioController.WriteSkills(skillList);
        ioController.WriteNotas(notasSecretasDict);
        ioController.WriteCollectables(colectablesDict);
        ioController.WriteTeleports(teleportDict);
    }

    
}
