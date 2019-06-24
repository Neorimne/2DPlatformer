using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    [SerializeField] private GameObject exitPanel;
    [SerializeField] private GameObject mainPanel;

    new private AudioSource audio;

    private void Awake()
    {
        audio = GetComponent<AudioSource>();
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
        audio.Play();
    }

    public void SetExitPanel()
    {
        mainPanel.SetActive(false);
        exitPanel.SetActive(true);
        audio.Play();
    }
    public void SetMainPanel()
    {
        exitPanel.SetActive(false);
        mainPanel.SetActive(true);
        audio.Play();
    }

    public void ExitGame()
    {
        Application.Quit();
        audio.Play();

    }
}
