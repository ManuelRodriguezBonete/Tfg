using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandyTeleport : MonoBehaviour
{
    private GameObject player;
    private Camera camera;
    private CameraController camController;

    [SerializeField] private GameObject targetTp;
    
    [SerializeField] private bool cambiaHabita;
    [SerializeField] private GameObject camPoint;
    [SerializeField] private float tamanioCam;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player"); 
        camera = Camera.main;
        camController = camera.GetComponent<CameraController>();
    }

    void Update()
    {
        //this.transform.rotation.z= 0;
        transform.Rotate(new Vector3(0, 0, 2));
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        player.transform.position = targetTp.transform.position;
        if (cambiaHabita)
        {
            camController.SetSize(tamanioCam);
            camController.SetTarget(camPoint.transform.position);
        }
    }

}
