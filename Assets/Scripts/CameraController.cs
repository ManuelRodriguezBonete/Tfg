using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEngine.GraphicsBuffer;

public class CameraController : MonoBehaviour
{
    //public Transform target;
    //public Vector3 posOffset;
    //public float smooth = 1;

    [SerializeField] new Camera camera;
    private List<GameObject> listaPuntosCamara = new List<GameObject>();
    [SerializeField] private GameObject listaPC;
    [SerializeField] private float speed = 15;
    private bool animacion = true;
    private Vector3 target;
    //private int currentPos = 0;
    [SerializeField] private int numTarget = 0;
    [SerializeField] private float numSize = 7;
    private float size;
    public int currentPoint;

    //Solo se usaría en el método 2
    //Vector3 velocity;
    private void Start()
    {
        //transform.position = target.transform.position;
        //target = listaPuntosCamara[numTarget].transform.position;
        //camera.orthographicSize = numSize;
        //transform.position = listaPuntosCamara[numTarget].transform.position;
        //listaPuntosCamara = listaPC.GetComponentsInChildren<GameObject>().ToList();
        //int uax = listaPC.transform.childCount;
        listaPuntosCamara.Clear();
        for (int i = 0; i < listaPC.transform.childCount; i++)
        {
            listaPuntosCamara.Add(listaPC.transform.GetChild(i).gameObject);
        }

        SetCameraUltraWide();

        if (SceneManager.GetActiveScene().name != "Creditos" && SceneManager.GetActiveScene().name != "Estadísticas")
        {
            if (PlayerPrefs.HasKey("CameraPoint"))
            {
                target = listaPuntosCamara[PlayerPrefs.GetInt("CameraPoint")].transform.position;
                currentPoint = PlayerPrefs.GetInt("CameraPoint");
                transform.position = target;
                size = PlayerPrefs.GetFloat("CameraSize");
                if (size == 0) size = 7;
                camera.orthographicSize = size;
            }
            else
            {
                target = listaPuntosCamara[0].transform.position;
                currentPoint = 0;
                transform.position = target;    
                camera.orthographicSize = 7;
            }
        }
        else
        {
            target = listaPuntosCamara[0].transform.position;
            currentPoint = 0;
        }
            
    }
    public void SetCameraUltraWide()
    {
        double w = Screen.width;
        double h = Screen.height;
        double ratio = w / h;

        //UltraWideScreen
        Debug.Log(ratio);
        if (ratio > 2.3f)
        {
            camera.rect = new Rect(0.125f, 0, 0.75f, 1);
        }
        else
        {
            camera.rect = new Rect(0, 0, 1, 1);
        }
    }
    public void ResetPosKey()
    {
        GameObject currentCam = listaPuntosCamara[currentPoint];
        Debug.Log(currentCam.gameObject.name);
        //for (int i = 0; i < listaPuntosCamara.Count; i++)
        //{
        //    listaPuntosCamara[i].GetComponent<EnterZoneScript>().RestartItems();
        //}
        currentCam.gameObject.GetComponent<EnterZoneScript>().RestartItems();
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
        //if (animacion)
        //{
        //    if (Vector2.Distance(transform.position, target) > 0.1f)
        //    {
        //        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
        //    }
        //}
        //else
        //{
        //    transform.position = target;
        //}
        //if (Vector2.Distance(transform.position, target) < 0.1f)
        //{
        //    camera.orthographicSize = size;
        //}

        transform.position = target;

    }
    public void SetTarget(Vector3 vec)
    {
        target = vec;
    }
    public void SetSize(float num)
    {
        size = num;
        camera.orthographicSize = size;
    }
    public float GetSize()
    {
        return size;
    }
    public void SetAnimacion(bool anim)
    {
        animacion = anim;
    }
    public int GetCameraPoint()
    {
        int aux = -1;
        for (int i = 0; i < listaPuntosCamara.Count; i++)
        {
            if (target.x == listaPuntosCamara[i].transform.position.x && target.y == listaPuntosCamara[i].transform.position.y)
            {
                aux = i;
            }
        }
        currentPoint = aux;
        return aux;
    }
}
