using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.Mathematics;
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
    public List<string> ReadSkills()
    {
        string path = "Assets/Resources/Skills.txt";
        List<string> list = new List<string>();
        string[] content = File.ReadAllLines(path);
        for (int i = 0; i < content.Length; i++)
        {
            string line = content[i];
            list.Add(line);
        }

        return list;
    }
    public void WriteSkills(List<string> list)
    {
        string path = "Assets/Resources/Skills.txt";
        File.WriteAllText(path, "");
        foreach (var skill in list)
        {
            File.AppendAllText(path, skill + "\n");
        }
    }
    public Dictionary<int, string> ReadNotasSecretas()
    {
        string path = "Assets/Resources/NotasSecretas.txt";
        Dictionary<int, string> deathDict = new Dictionary<int, string>();

        string[] content = File.ReadAllLines(path);
        for (int i = 0; i < content.Length; i++)
        {
            string[] line = content[i].Split('-');
            deathDict.Add(Convert.ToInt32(line[0]), line[1]);
        }

        return deathDict;
    }
}
