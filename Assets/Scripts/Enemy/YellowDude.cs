using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class YellowDude : Unit
{
    [SerializeField] private float speed = 5.0f;
    private Vector2 direction;
    private SpriteRenderer sprite;
    new private Rigidbody2D rigidbody;
    new private AudioSource audio;

    public Vector2 Direction
    {
        get
        {
            return direction;
        }

        set
        {
            direction = value;
        }
    }

    private void Awake()
    {
        audio = GetComponent<AudioSource>();
        rigidbody = GetComponent<Rigidbody2D>();
        sprite = GetComponentInChildren<SpriteRenderer>();
        Direction = new Vector2(-1.0f, 0);
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
        
    }

    private void CheckMove()
    {

        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position + transform.right * 0.5f * Direction.x, 0.2f);
        if (colliders.Length > 1 && colliders.All(x => !x.GetComponent<Character>() && !x.GetComponent<Bullet>()))
        {
            Direction *= -1.0f;
            sprite.flipX = Direction.x > 0;
        }


    }
    private void Fly()
    {
        rigidbody.MovePosition(rigidbody.position + Direction * Time.deltaTime * speed);
    }

    public void OnTriggerEnter2D(Collider2D collider)
    {
        Unit unit = collider.GetComponent<Unit>();

        if (unit && unit is Character)
        {
            if (Mathf.Abs(unit.transform.position.x - transform.position.x) < 0.8f)
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
