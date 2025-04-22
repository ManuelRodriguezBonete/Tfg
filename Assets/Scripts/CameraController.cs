using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEngine.GraphicsBuffer;

public class CameraController : MonoBehaviour
{
    [Header("Metodos de seguimiento - Only Bosses")]
    [SerializeField] public bool bossFight;
    [SerializeField] Transform targetFollow;
    [SerializeField] Transform targetMIN;
    [SerializeField] Transform targetMAX;
    [SerializeField] public Vector3 posOffset;
    [SerializeField] float smooth = 1;
    public float yLock;
    public float xLock;
    public bool vertical;
    [Header("Cámara normal")]
    [SerializeField] new Camera camera;
    private List<GameObject> listaPuntosCamara = new List<GameObject>();
    [SerializeField] private GameObject listaPC;
    //[SerializeField] private float speed = 15;
    private bool animacion = true;
    private Vector3 target;
    //private int currentPos = 0;
    /*[SerializeField] */private int numTarget = 0;
    /*[SerializeField]*/ private float numSize;
    private float size;
    private int currentPoint;

   
    //Solo se usaría en el método 2
    //Vector3 velocity;
    private void Start()
    {
        posOffset = new Vector3(0, 0, -10);
        yLock = transform.position.y;
        xLock = transform.position.y;
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
            if (bossFight)
            {
                camera.orthographicSize = numSize;
            }
            else if (PlayerPrefs.HasKey("CameraPoint"))
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
        Debug.Log(currentCam.name);
        //for (int i = 0; i < listaPuntosCamara.Count; i++)
        //{
        //    listaPuntosCamara[i].GetComponent<EnterZoneScript>().RestartItems();
        //}
        currentCam.GetComponent<EnterZoneScript>().RestartItems();
    }

    private void LateUpdate()
    {
        //Metodo 1
        if (bossFight) CameraBoss();
        else MoveCamera();
       

        //Metodo 2
        //transform.position = Vector3.SmoothDamp(transform.position, target.position + posOffset, ref velocity, smooth);

        
    }
    public void CameraBoss()
    {
        if (!vertical)
        {
            if (!(targetFollow.transform.position.x < targetMIN.transform.position.x) || !(targetMAX.transform.position.x < targetFollow.transform.position.x))
            {
                Vector3 newPos = new Vector3(targetFollow.transform.position.x + posOffset.x, yLock, -10);
                transform.position = newPos;
            }
        }
        else 
        {
            if (!(targetFollow.transform.position.y < targetMIN.transform.position.y) || !(targetMAX.transform.position.y < targetFollow.transform.position.y))
            {
                Vector3 newPos = new Vector3(target.x , targetFollow.transform.position.y + posOffset.y, -10);
                transform.position = newPos;
            }
        }
        
        
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
