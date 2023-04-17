using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ControllerScript : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("Player")]
    [SerializeField] private GameObject player;
    private int numeroMuertes = 0;

    [Header("Tilemaps")]
    [SerializeField] Tilemap respawnPointsLayer;
    [SerializeField] Tilemap spikesLayer;

    private Vector3 lastSavedPoint;
  
    public void KillPlayer()
    {
        player.transform.position = lastSavedPoint;
        numeroMuertes++;
    }

    public void SavePoint(Vector3 lastPosition)
    {
        lastSavedPoint = lastPosition;
    }
}
