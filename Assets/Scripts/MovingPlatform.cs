using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] private List<Transform> posiciones;
    [SerializeField] private float speed;
    Vector2 target;
    private int currentPos = 0;

    void Start()
    {
        target = posiciones[currentPos].position;
    }

    void Update()
    {
        CheckPos();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && this.CompareTag("Platform"))
        {
            collision.transform.SetParent(this.transform);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && this.CompareTag("Platform"))
        {
            collision.transform.SetParent(null);
        }
    }
    private void CheckPos()
    {
        if (Vector2.Distance(transform.position, posiciones[currentPos].position) < 0.1f)
        {
            currentPos++;
            if (currentPos >= posiciones.Count) currentPos = 0;
            target = posiciones[currentPos].position;
            if (this.CompareTag("Slime"))
            {
                transform.Rotate(new Vector3(0, 180f));
            }
        }
        transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);
    }
}
