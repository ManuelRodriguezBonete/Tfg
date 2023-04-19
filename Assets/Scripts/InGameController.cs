using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void StopGame()
    {
        Time.timeScale = 0;
    }
    public void ReanudeGame()
    {
        Time.timeScale = 1;
    }
}
