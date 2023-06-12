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
    private float timer = 0.5f;
    public bool death;

    private void Start()
    {
        ReadDeaths();

    }
    private void Update()
    {
        if (Input.GetButtonDown("Suicide")) 
        {
            KillPlayer("Suicide");
        }
        if (player.GetComponent<PlayerMovement>().controlsOK == false)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                timer = 0.5f;
                player.GetComponent<PlayerMovement>().controlsOK = true;
                player.GetComponent<PlayerMovement>().extraDashValue = 0;
                death = false;
            }
        }
        
    }
    public void KillPlayer(string key)
    {
        death = true;
        player.transform.position = spawnPoint;
        player.GetComponent<PlayerMovement>().controlsOK = false;
        player.GetComponent<Rigidbody2D>().velocity= Vector3.zero;
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
        spawnPoint.x = lastPosition.x;
        spawnPoint.y = lastPosition.y+0.5f;
    }
    private void ReadDeaths()
    {
        deathDict = ioController.ReadDeaths();
    }
    public  void WriteDeaths()
    {
        ioController.WriteDeaths(deathDict);   
    }
    public Vector3 GetSpawnPoint()
    {
        return spawnPoint;
    }
}
