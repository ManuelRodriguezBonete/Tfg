using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class FlyingObjectScript : MonoBehaviour
{
    private GameObject player;
    [SerializeField] bool dash;
    [SerializeField] bool jump;
    [SerializeField] bool dis;
    [SerializeField] private float initialTimer = 3;
    private float timer;
    private bool itsOn = true;
    // Start is called before the first frame update
    void Start()
    {
        ResetTimer();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (!itsOn)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                ResetTimer();
                EnableFruit();
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (dash)
        {
            player.GetComponent<PlayerMovement>().hasDashed = false;
            player.GetComponent<PlayerMovement>().extraDashValue = 1;
        }
        if (jump)
        {
            player.GetComponent<PlayerMovement>().NExtraJumps += 1;
        }

        DisableFruit();

    }
    private void ResetTimer()
    {
        timer = initialTimer;
    }
    private void EnableFruit()
    {
        itsOn = true;
        this.gameObject.GetComponent<SpriteRenderer>().enabled = true;
        this.gameObject.GetComponent<CircleCollider2D>().enabled = true;
    }
    private void DisableFruit()
    {
        itsOn = false;
        this.gameObject.GetComponent<SpriteRenderer>().enabled = false;
        this.gameObject.GetComponent<CircleCollider2D>().enabled = false;
    }
}
