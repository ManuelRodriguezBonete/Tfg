using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestartGameObjecto : MonoBehaviour
{
    // Start is called before the first frame update
    Vector3 initialPos= Vector3.zero;
    void Awake()
    {
        if (transform.parent == null)
            initialPos = transform.position;
        else
            initialPos = transform.position ;
        
        
    }

    public void RestartPosition()
    {
        transform.position = initialPos;
    }
}
