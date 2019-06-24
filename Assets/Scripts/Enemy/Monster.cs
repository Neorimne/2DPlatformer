using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : Unit
{
    [SerializeField] Character character;
    private SpriteRenderer sprite;
    private bool facingRight = true;
    private new AudioSource audio;

    private void Awake()
    {
        audio = GetComponent<AudioSource>();
        sprite = GetComponentInChildren<SpriteRenderer>();
    }

    private void Update()
    {
        CheckCharacterPos();
    }

    private void CheckCharacterPos()
    {
        try
        {
            if (transform.position.x - character.transform.position.x > 0)
            {
                facingRight = true;
            }
            else facingRight = false;

            if (facingRight == false)
            {
                sprite.flipX = true;

            }
            else
            {
                sprite.flipX = false;
            }
        }
        catch
        {
            facingRight = true;
        }


    }

    public void OnTriggerEnter2D(Collider2D collider)
    {
        Unit unit = collider.GetComponent<Unit>();

        if (unit && unit is Character)
        {
            if (Mathf.Abs(unit.transform.position.x - transform.position.x) < 0.7f)
            {
                audio.Play();
                ReceiveDamage();
            }

            else
            {
                character.State = CharState.GetDamage;
                unit.ReceiveDamage();
                
            }
        }
    }
}
