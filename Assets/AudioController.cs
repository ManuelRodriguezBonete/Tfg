using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioSource src;
    public AudioClip death1;
    public AudioClip death2;

    public void DeathSound()
    {
        int n = Random.Range(0, 10);
        if (n>=5)
        {
            Debug.Log("aaa");
            src.clip = death1;
            src.Play();
        }
        else
        {
            Debug.Log("bbb");
            src.clip = death2;
            src.Play();
        }
    }

}
