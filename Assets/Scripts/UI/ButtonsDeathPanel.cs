using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonsDeathPanel : MonoBehaviour
{
    private new AudioSource audio;

    private void Awake()
    {
        audio = GetComponent<AudioSource>();
    }

    public void RestartButton()
    {
        audio.Play();
        SceneManager.LoadScene(1);

    }

    public void ExitGame()
    {
        audio.Play();
        SceneManager.LoadScene(0);
    }
    
}
