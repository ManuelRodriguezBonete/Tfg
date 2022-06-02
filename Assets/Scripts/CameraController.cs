using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;
    public Vector3 posOffset;
    public float smooth = 1;

    //Solo se usaría en el método 2
    Vector3 velocity;
    private void Awake()
    {
        transform.position = target.transform.position;
    }
    private void LateUpdate()
    {
        //Metodo 1
        transform.position = Vector3.Lerp(transform.position, target.position + posOffset, smooth * Time.deltaTime);

        //Metodo 2
        //transform.position = Vector3.SmoothDamp(transform.position, target.position + posOffset, ref velocity, smooth);
    }
}
