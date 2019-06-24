using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchOff : MonoBehaviour
{

    [SerializeField] GameObject switchOn;
    private new AudioSource audio;

    private void Awake()
    {
        audio = GetComponent<AudioSource>();
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        Unit unit = collision.GetComponent<Unit>();

        if (unit && unit is Character)
        {
            audio.Play();
            gameObject.SetActive(false);
            switchOn.SetActive(true);
        }
       
        
    }


}
