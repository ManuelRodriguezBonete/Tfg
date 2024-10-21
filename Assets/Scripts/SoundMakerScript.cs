using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundMakerScript : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private SFXController controller;
    [SerializeField] private AudioClip clip;
    [SerializeField] private bool stay;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        controller.ClipRep(clip);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (stay)
        {
            controller.StopRep();
        }
    }
}
