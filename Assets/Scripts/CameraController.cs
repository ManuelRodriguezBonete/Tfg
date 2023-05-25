using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class CameraController : MonoBehaviour
{
    //public Transform target;
    //public Vector3 posOffset;
    //public float smooth = 1;

    [SerializeField] Camera camera;
    [SerializeField] private List<GameObject> listaPuntosCamara = new List<GameObject>();
    [SerializeField] private float speed = 15;
    private Vector3 target;
    private int currentPos = 0;
    public int numTarget = 0;
    private int size;

    //Solo se usar�a en el m�todo 2
    Vector3 velocity;
    private void Awake()
    {
        //transform.position = target.transform.position;
        target= transform.position;
    
    }
    private void LateUpdate()
    {
        //Metodo 1
        //transform.position = Vector3.Lerp(transform.position, target.position + posOffset, smooth * Time.deltaTime);

        //Metodo 2
        //transform.position = Vector3.SmoothDamp(transform.position, target.position + posOffset, ref velocity, smooth);

        MoveCamera();
    }

    public void MoveCamera()
    {
        //target = listaPuntosCamara[numTarget].transform.position;
        if (Vector2.Distance(transform.position, target) > 0.1f) {
            transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
        }
        //if (Vector2.Distance(transform.position, target) < 0.1f)
        //{
        //    camera.orthographicSize = size;
        //}
        

    }
    public void SetTarget(Vector3 vec)
    {
        target = vec;
    }
    public void SetSize(int num)
    {
        size = num;
    }
}
