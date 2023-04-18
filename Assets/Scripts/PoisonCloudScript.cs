using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class PoisonCloudScript : MonoBehaviour
{
    [SerializeField] DeathControllerScript controller;
    [SerializeField] private float initialTimer;
    [SerializeField] private List<GameObject> nubes;
    private float timer;
    private Color initialColor;
    private Color auxColor;
    // Start is called before the first frame update
    void Start()
    {
        ResetTimer();
        initialColor = nubes[0].GetComponent<SpriteRenderer>().color;
        auxColor = initialColor;
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            timer -= Time.deltaTime;
            auxColor.g += Time.deltaTime/10;
            auxColor.r += Time.deltaTime/15;
            for (int i = 0; i < nubes.Count; i++)
            {
                nubes[i].GetComponent<SpriteRenderer>().color = auxColor;
            }
            if (timer <= 0)
            {
                controller.KillPlayer("Poison Cloud");
                ResetTimer();
            }
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        ResetTimer();
        ResetColor();
    }
    private void ResetTimer()
    {
        timer = initialTimer;
    }
    private void ResetColor()
    {
        for (int i = 0; i < nubes.Count; i++)
        {
            nubes[i].GetComponent<SpriteRenderer>().color = initialColor;
        }
        auxColor = initialColor;
    }
}
