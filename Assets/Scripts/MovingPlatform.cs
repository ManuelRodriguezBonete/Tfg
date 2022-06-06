using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] private Transform posA;
    [SerializeField] private Transform posB;
    [SerializeField] private float speed;
    Vector2 target;

    void Start()
    {
        target = posA.position;
    }

    void Update()
    {
        if (Vector2.Distance(transform.position, posA.position) < 0.1f) target = posB.position;
        if (Vector2.Distance(transform.position, posB.position) < 0.1f) target = posA.position;

        transform.position = Vector2.MoveTowards(transform.position, target, speed*Time.deltaTime);

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.transform.SetParent(this.transform);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.transform.SetParent(null);
        }
    }

}
