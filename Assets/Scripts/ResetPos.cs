using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetPos : MonoBehaviour
{
    // Start is called before the first frame update
    Vector3 initialPos;
    void Start()
    {
        initialPos= transform.position;
    }

    public void ResetPosition()
    {
        transform.position= initialPos;
    }
}
