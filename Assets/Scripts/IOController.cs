using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class IOController : MonoBehaviour
{
    
    public Dictionary<string, int> ReadDeaths()
    {
        string path = "Assets/Resources/Deaths.txt";
        Dictionary<string, int> deathDict= new Dictionary<string, int>();

        string[] content = File.ReadAllLines(path);
        for (int i = 0; i < content.Length; i++)
        {
            string[] line = content[i].Split('-');
            deathDict.Add(line[0], Convert.ToInt32(line[1]));
        }
        
        return deathDict;
    }
    public void WriteDeaths(Dictionary<string, int> deathList) 
    {
        string path = "Assets/Resources/Deaths.txt";
        File.WriteAllText(path, "");
        foreach (var death in deathList)
        {
            File.AppendAllText(path, death.Key + "-" + death.Value +"\n");
        }
    }
}
