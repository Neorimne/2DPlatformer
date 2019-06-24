using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class MoveableMonster : Unit
{
    [SerializeField] private float speed = 5.0f;
    private Vector2 direction;
    private SpriteRenderer sprite;
    new private Rigidbody2D rigidbody;
    private new AudioSource audio;
    

    private void Awake()
    {
        audio = GetComponent<AudioSource>();
        rigidbody = GetComponent<Rigidbody2D>();
        sprite = GetComponentInChildren<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        Fly();
    }

    private void Update()
    {
        CheckMove();
    }

    private void Start()
    {
        direction = new Vector2(-1.0f, 0);
    }

    private void CheckMove()
    {
         
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position + transform.right * 0.5f * direction.x , 0.2f);
        if (colliders.Length > 1 && colliders.All(x => !x.GetComponent<Character>() && !x.GetComponent<Bullet>()))
        {
            direction *= -1.0f;
            sprite.flipX = direction.x < 0;
        }
         
        
    }
    private void Fly()
    {
        rigidbody.MovePosition(rigidbody.position + direction * Time.deltaTime * speed);
    }

    public void OnTriggerEnter2D(Collider2D collider)
    {
        Unit unit = collider.GetComponent<Unit>();

        if (unit && unit is Character)
        {
            if (Mathf.Abs(unit.transform.position.y - transform.position.y) < 0.5f)
            {
                audio.Play();
                ReceiveDamage();
            }

            else
            {
                unit.ReceiveDamage();

            }
        }
    }

}
