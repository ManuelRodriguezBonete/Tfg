using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class ParpadearPlatform : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private float timeOn = 3f;
    [SerializeField] private float timeOff = 3f;
    [SerializeField] private List<GameObject> sprites;
    [SerializeField] private bool itsOn = true;
    [SerializeField] private float timer = 0f;

    // Update is called once per frame
    void Update()
    {
        if (itsOn && timer >=timeOn)
        {
            //Se apaga
            itsOn = false;
            ResetTimer();
            for (int i = 0; i < sprites.Count; i++)
            {
                sprites[i].GetComponent<Renderer>().enabled = false;
            }
            GetComponent<BoxCollider2D>().enabled = false;
        }
        if (!itsOn && timer >= timeOff)
        {
            //Se enciende
            itsOn = true;
            ResetTimer();
            for (int i = 0; i < sprites.Count; i++)
            {
                sprites[i].GetComponent<Renderer>().enabled = true;
            }
            GetComponent<BoxCollider2D>().enabled = true;
        }
        timer += Time.deltaTime;
    }
    private void ResetTimer()
    {
        timer = 0;
    }
}
