using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportScript : MonoBehaviour
{
    private Camera camera;
    private GameObject player;
    [SerializeField] GameObject target;
    [SerializeField] GameObject cameraPointTarget;
    [SerializeField] float cameraSize;

    private bool col = false;
    // Start is called before the first frame update
    void Start()
    {
        camera = Camera.main;
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (col && Input.GetButtonDown("Interactuar")) 
        {
            Teleport();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            col = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            col = false;
        }

    }
    private void Teleport()
    {
        player.transform.position = target.transform.position;
        camera.GetComponent<CameraController>().SetSize(cameraSize);
        camera.GetComponent<CameraController>().SetTarget(cameraPointTarget.transform.position);
        camera.GetComponent<CameraController>().SetAnimacion(false);
    }
}
