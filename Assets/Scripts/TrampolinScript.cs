using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrampolinScript : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject big;
    [SerializeField] private GameObject little;
    [SerializeField] private float extraJump;
    private float initialJump;
    private float timer = 0.5f;
    private bool on = false;
    private GameObject player;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        initialJump = player.GetComponent<PlayerMovement>().JumpForce;
        little.SetActive(false);
        big.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (on)
        {
                //llamar a jump con fuerza determinada
                on = false;
                player.GetComponent<PlayerMovement>().Jump(Vector2.up, extraJump);
            
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            big.SetActive(false);
            little.SetActive(true);
            on = true;
            collision.gameObject.GetComponent<PlayerMovement>().JumpForce += extraJump;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            little.SetActive(false);
            big.SetActive(true);
            collision.gameObject.GetComponent<PlayerMovement>().JumpForce = initialJump;
        }
    }
}
