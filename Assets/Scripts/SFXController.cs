using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXController : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioSource sfxSource;
    public AudioClip death1;
    public AudioClip death2;

    public void DeathSound()
    {
        int n = Random.Range(0, 10);
        if (n>=5)
        {
            sfxSource.clip = death1;
            sfxSource.Play();
        }
        else
        {
            sfxSource.clip = death2;
            sfxSource.Play();
        }
    }
    public void ClipRep(AudioClip clip)
    {
        sfxSource.clip = clip;
        sfxSource.Play();
    }
    public void StopRep() { sfxSource.Stop(); }
}
