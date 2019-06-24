using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishButtons : MonoBehaviour
{
    private new AudioSource audio;

    private void Awake()
    {
        audio = GetComponent<AudioSource>();
    }

    public void RestartLevel()
    {
        audio.Play();
        SceneManager.LoadScene(1);
    }

    public void Exit()
    {
        audio.Play();
        SceneManager.LoadScene(0);
    }
}
