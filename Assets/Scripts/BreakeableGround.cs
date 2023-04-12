using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BreakeableGround : MonoBehaviour
{
    Tilemap tilemap;
    void Start()
    {
        tilemap = gameObject.GetComponent<Tilemap>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        bool facingRight = player.GetComponent<PlayerMovement>().facingRight;
        Vector3 hitPosition = player.transform.position;
        if (facingRight)
        {
            hitPosition = player.transform.position + new Vector3(0.5f, 0f, 0f);
        }
        else
        {
            hitPosition = player.transform.position + new Vector3(-0.5f, 0f, 0f);
        }
        

        if (collision.gameObject == player)
        {
            tilemap.SetTile(tilemap.WorldToCell(hitPosition), null);
        }


    }
}
