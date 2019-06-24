using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoostSpeed : MonoBehaviour
{
    [SerializeField] Character character;
    [SerializeField] private float BoostCnt = 3.0f;
    private new AudioSource audio;

    private void Awake()
    {
        audio = GetComponent<AudioSource>();
    }

    private void Update()
    {
        
  

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Unit unit = collision.GetComponent<Unit>();

        if (unit && unit is Character)
        {
            audio.Play();
            character.speed *= BoostCnt;
            Destroy(gameObject, 0.1f);
        }
        
    }
}
