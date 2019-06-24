using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finish : MonoBehaviour
{
    [SerializeField] GameObject finishScene;
    [SerializeField] GameObject menuControl;
    [SerializeField] AudioSource audioFinish;
    [SerializeField] AudioSource backMusic;

    private bool isFinished = false;

    private void Update()
    {
        if (isFinished)
        {
            Cursor.visible = true;
            finishScene.SetActive(true);
            Time.timeScale = 0;
        }
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        audioFinish.Play();
        backMusic.Stop();
        menuControl.SetActive(false);
        isFinished = true;
    }

    
}
