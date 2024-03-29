using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeRoom : MonoBehaviour
{
    [SerializeField] private CameraController controller;
    [SerializeField] private GameObject salaAnterior;
    [SerializeField] private GameObject salaPosterior;
    [SerializeField] private bool horizontal;
    [SerializeField] private float sizeAnterior = 7;
    [SerializeField] private float sizePosterior = 7;
    [SerializeField] private bool animacion;
    
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            controller.SetAnimacion(animacion);
            if (horizontal)
            {
                float difX = collision.gameObject.transform.position.x - this.transform.position.x;
                if (difX > 0)
                {
                    controller.SetTarget(salaPosterior.transform.position);
                    controller.SetSize(sizePosterior);
                    
                }
                else
                {
                    controller.SetTarget(salaAnterior.transform.position);
                    controller.SetSize(sizeAnterior);
                }
            }
            else
            {
                float difY = collision.gameObject.transform.position.y - this.transform.position.y;
                if (difY > 0)
                {
                    controller.SetTarget(salaPosterior.transform.position);
                    controller.SetSize(sizePosterior);
                }
                else
                {
                    controller.SetTarget(salaAnterior.transform.position);
                    controller.SetSize(sizeAnterior);
                }
            }
            int aux = controller.GetCameraPoint();
            Debug.Log(aux);
            
            
        }
    }
}
