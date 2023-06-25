using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Unity.Mathematics;
//using UnityEditor.Rendering.LookDev;
using UnityEngine;

public class IOController : MonoBehaviour
{
    [SerializeField] private bool exe = false;
    public Dictionary<string, int> ReadDeaths()
    {
        string path;
        if (exe)
        {
            path = "Tfg_Data/Resources/Text/Deaths.txt";
        }
        else
        {
            path = "Assets/Resources/Deaths.txt";
        }
        
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
        string path;
        if (exe)
        {
            path = "Tfg_Data/Resources/Text/Deaths.txt";
        }
        else
        {
            path = "Assets/Resources/Deaths.txt";
        }
        File.WriteAllText(path, "");
        foreach (var death in deathList)
        {
            File.AppendAllText(path, death.Key + "-" + death.Value +"\n");
        }
    }
    public List<string> ReadSkills()
    {
        string path;
        if (exe)
        {
            path = "Tfg_Data/Resources/Text/Skills.txt";
        }
        else
        {
            path = "Assets/Resources/Skills.txt";
        }
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
        string path;
        if (exe)
        {
            path = "Tfg_Data/Resources/Text/Skills.txt";
        }
        else
        {
            path = "Assets/Resources/Skills.txt";
        }
        File.WriteAllText(path, "");
        foreach (var skill in list)
        {
            File.AppendAllText(path, skill + "\n");
        }
    }
    public Dictionary<int, string> ReadNotasSecretas()
    {
        string path;
        if (exe)
        {
            path = "Tfg_Data/Resources/Text/NotasSecretas.txt";
        }
        else
        {
            path = "Assets/Resources/NotasSecretas.txt";
        }
        Dictionary<int, string> notas = new Dictionary<int, string>();

        string[] content = File.ReadAllLines(path);
        for (int i = 0; i < content.Length; i++)
        {
            string[] line = content[i].Split('-');
            notas.Add(Convert.ToInt32(line[0]), line[1]);
        }

        return notas;
    }
    public Dictionary<string, bool> ReadColeccionables()
    {
        string path;
        if (exe)
        {
            path = "Tfg_Data/Resources/Text/Coleccionables.txt";
        }
        else
        {
            path = "Assets/Resources/Coleccionables.txt";
        }
        Dictionary<string, bool> colec = new Dictionary<string, bool>();

        string[] content = File.ReadAllLines(path);
        for (int i = 0; i < content.Length; i++)
        {
            string[] line = content[i].Split('-');
            colec.Add(line[0],Convert.ToBoolean(line[1]) );
        }
        return colec;
    }

    public Dictionary<string, string> ReadTeleports()
    {
        string path;
        if (exe)
        {
            path = "Tfg_Data/Resources/Text/ViajeRapido.txt";
        }
        else
        {
            path = "Assets/Resources/ViajeRapido.txt";
        }
        Dictionary<string, string> teleports = new Dictionary<string, string>();

        string[] content = File.ReadAllLines(path);
        for (int i = 0; i < content.Length; i++)
        {
            string[] line = content[i].Split('*');
            teleports.Add(line[0], line[1]);
        }

        return teleports;
    }
    public void WriteNotas(Dictionary<int, string> notasList)
    {
        string path;
        if (exe)
        {
            path = "Tfg_Data/Resources/Text/NotasSecretas.txt";
        }
        else
        {
            path = "Assets/Resources/NotasSecretas.txt";
        }
        File.WriteAllText(path, "");
        foreach (var nota in notasList)
        {
            File.AppendAllText(path, nota.Key + "-" + nota.Value + "\n");
        }
    }
    public void WriteCollectables(Dictionary<string , bool> colecDic)
    {
        string path;
        if (exe)
        {
            path = "Tfg_Data/Resources/Text/Coleccionables.txt";
        }
        else
        {
            path = "Assets/Resources/Coleccionables.txt";
        }
        File.WriteAllText(path, "");
        foreach (var colec in colecDic)
        {
            File.AppendAllText(path, colec.Key + "-" + colec.Value + "\n");
        }

    }
    public void WriteTeleports(Dictionary<string, string> teleportsDict)
    {
        string path;
        if (exe)
        {
            path = "Tfg_Data/Resources/Text/ViajeRapido.txt";
        }
        else
        {
            path = "Assets/Resources/ViajeRapido.txt";
        }
        File.WriteAllText(path, "");
        foreach (var colec in teleportsDict)
        {
            File.AppendAllText(path, colec.Key + "*" + colec.Value + "\n");
        }
    }
    public void DeleteAll()
    {
        string path;
        if (exe)
        {
            path = "Tfg_Data/Resources/Text/ViajeRapido.txt";
        }
        else
        {
            path = "Assets/Resources/ViajeRapido.txt";
        }
        File.WriteAllText(path, "");

        if (exe)
        {
            path = "Tfg_Data/Resources/Text/Coleccionables.txt";
        }
        else
        {
            path = "Assets/Resources/Coleccionables.txt";
        }
        File.WriteAllText(path, "");

        if (exe)
        {
            path = "Tfg_Data/Resources/Text/NotasSecretas.txt";
        }
        else
        {
            path = "Assets/Resources/NotasSecretas.txt";
        }
        File.WriteAllText(path, "");

        if (exe)
        {
            path = "Tfg_Data/Resources/Text/Skills.txt";
        }
        else
        {
            path = "Assets/Resources/Skills.txt";
        }
        File.WriteAllText(path, "");

        if (exe)
        {
            path = "Tfg_Data/Resources/Text/Deaths.txt";
        }
        else
        {
            path = "Assets/Resources/Deaths.txt";
        }
        File.WriteAllText(path, "");
    }
}
