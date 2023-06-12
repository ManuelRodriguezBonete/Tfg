using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeScript : MonoBehaviour
{
    [SerializeField] GameObject p1;
    [SerializeField] GameObject p2;
    private bool actualP1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Jump")) 
        { 
            if (actualP1) 
            { 
                transform.position = p2.transform.position;
                actualP1= false;
            }
            else
            {
                transform.position = p1.transform.position;
                actualP1 = true;
            }
        }
    }
}
