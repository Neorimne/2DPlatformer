using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioDeathBreath : MonoBehaviour
{
    private new AudioSource audio;

    private void Awake()
    {
        audio = GetComponent<AudioSource>();
    }

    public void Play()
    {
        audio.Play();
    }
}
