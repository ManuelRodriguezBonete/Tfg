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
        if (player.GetComponent<PlayerMovement>().UnlockedBreakItems)
        {
            bool facingRight = player.GetComponent<PlayerMovement>().facingRight;
            Vector3 hitPosition = collision.transform.position;
            Vector3 hitCorner = collision.transform.position;
            Vector3 topPosition = collision.transform.position + new Vector3(0f, 0.8f, 0f);
            Vector3 botPosition = collision.transform.position + new Vector3(0f, -0.8f, 0f);
            Debug.Log(hitPosition);

            if (facingRight)
            {
                hitPosition = collision.transform.position + new Vector3(0.8f, 0f, 0f);
                hitCorner = collision.transform.position + new Vector3(0.8f, 0.5f, 0f);
            }
            else
            {
                hitPosition = collision.transform.position + new Vector3(-0.8f, 0f, 0f);
                hitCorner = collision.transform.position + new Vector3(0.8f, -0.5f, 0f);

            }


            if (collision.gameObject == player)
            {
                tilemap.SetTile(tilemap.WorldToCell(hitPosition), null);
                tilemap.SetTile(tilemap.WorldToCell(hitCorner), null);
                tilemap.SetTile(tilemap.WorldToCell(topPosition), null);
                tilemap.SetTile(tilemap.WorldToCell(botPosition), null);
            }
        }
        


    }
}
