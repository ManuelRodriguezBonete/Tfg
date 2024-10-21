using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioSource musicSource;
    public AudioClip ambience;
    void Start()
    {
        PlayMusic();
    }
    private void PlayMusic()
    {
        musicSource.clip= ambience;
        musicSource.loop = true;
        musicSource.Play();
    }

}
