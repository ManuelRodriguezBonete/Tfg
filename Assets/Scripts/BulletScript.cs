using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    // Start is called before the first frame update
    private DeathControllerScript controller;
    private LaserScript laser;
    private float speed;
    private Vector2 dir;
    private Vector2 target;
    private float timer;
    void Start()
    {
        controller = FindObjectOfType<DeathControllerScript>();
        laser = GetComponentInParent<LaserScript>();
        speed = laser.GetBulletSpeed();
        dir = laser.GetDir();
        target = new Vector2(transform.position.x, transform.position.y);
        this.transform.parent = null;
        timer = laser.bulletTimeAlive == 0 ? 3 : laser.bulletTimeAlive;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        timer-= Time.deltaTime;
        if (timer <= 0) Destroy(this.gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            controller.KillPlayer("Laser");
        }
        Destroy(this.gameObject);
    }
    private void Move()
    {
        target.x += 1 * dir.x;
        target.y += 1 * dir.y;
        transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        this.gameObject.SetActive(false);
    }
}
