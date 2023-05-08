using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] private IOController ioController;
    public List<string> skillList = new List<string>();
    public List<string> collectableList= new List<string>();
    public int numCollectables = 0;
    
    void Awake()
    {
        skillList = ioController.ReadSkills();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
