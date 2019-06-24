using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FinishScore : MonoBehaviour
{
    [SerializeField] Character character;
    [SerializeField] Text score;
   

    private void Update()
    {
        score.text = "Score: " + character.score.ToString();

        

    }
}
