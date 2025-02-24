using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    public AudioSource audio;
    public static AudioController instance;

    public void AudioArmasPlay() 
    {
        audio.Play();
    }

    public void AudioArmasStop()
    {
        audio.Stop();
    }
}
