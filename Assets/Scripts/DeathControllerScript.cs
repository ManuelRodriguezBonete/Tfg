using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DeathControllerScript : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("Controllers")]
    [SerializeField] private IOController ioController;

    [Header("Player")]
    [SerializeField] private GameObject player;

    private Dictionary<string, int> deathDict;
    private Vector3 spawnPoint;

    private void Start()
    {
        ReadDeaths();

    }
    public void KillPlayer(string key)
    {
        player.transform.position = spawnPoint;
        if (deathDict.TryGetValue(key, out var auxDeath))
        {
            auxDeath++;
            deathDict[key] = auxDeath;
        }
        else
        {
            deathDict.Add(key, 1);
        }

        deathDict.TryGetValue("Total", out var totalDeaths);
        totalDeaths++;
        deathDict["Total"] = totalDeaths;
        
    }

    public void SavePoint(Vector3 lastPosition)
    {
        spawnPoint = lastPosition;
    }
    private void ReadDeaths()
    {
        deathDict = ioController.ReadDeaths();
    }
    public  void WriteDeaths()
    {
        ioController.WriteDeaths(deathDict);   
    }
}
