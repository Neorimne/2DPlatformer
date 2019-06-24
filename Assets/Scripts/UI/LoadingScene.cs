using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingScene : MonoBehaviour
{
    [SerializeField] private Scrollbar scrollbar;
    [SerializeField] private int sceneID;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SceneLoader()); 
    }

    IEnumerator SceneLoader()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneID);
        while (!operation.isDone)
        {
            float progress = operation.progress / 0.9f;
            scrollbar.size = progress;
            yield return null;
        }
       
    }
}
