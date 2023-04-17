using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DeathControllerScript : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("Other Controllers")]
    [SerializeField] private IOController ioController;

    [Header("Player")]
    [SerializeField] private GameObject player;

    [Header("Deaths")]
    private List<int> deathList;
    private Dictionary<string, int> deathDict;
 
    [Header("Tilemaps")]
    [SerializeField] private Tilemap respawnPointsLayer;
    [SerializeField] private Tilemap spikesLayer;

    

    private Vector3 lastSavedPoint;

    private void Start()
    {
        ReadDeaths();

    }
    public void KillPlayer(string key)
    {
        player.transform.position = lastSavedPoint;
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
        lastSavedPoint = lastPosition;
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
