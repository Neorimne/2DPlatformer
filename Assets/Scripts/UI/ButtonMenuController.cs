using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonMenuController : MonoBehaviour
{
    [SerializeField] MenuController controller;
    private new AudioSource audio;
    private void Awake()
    {
        audio = GetComponent<AudioSource>();
    }

    public void ResumeGame()
    {
        audio.Play();
        controller.IsPaused = false;
    }

    public void RestartGame()
    {
        audio.Play();
        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
        audio.Play();
        SceneManager.LoadScene(0);
    }
}
