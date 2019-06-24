using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthUp : MonoBehaviour
{
    [SerializeField] Character character;

    private new AudioSource audio;

    private void Awake()
    {
        audio = GetComponent<AudioSource>();
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        Character character = collision.GetComponent<Character>();
        if (character)
        {
            audio.Play();
            character.lives++;
            character.score += 5;
            Destroy(gameObject, 0.1f);
        }
        
    }
}
