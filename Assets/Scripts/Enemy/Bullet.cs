using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed = 10.0f;
    private SpriteRenderer sprite;
    new private Rigidbody2D rigidbody;

    private Vector2 direction;
    public Vector2 Direction
    {
        set { direction = value; }
    }


    public Color Color
    {
        set { sprite.color = value; }
    }

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        sprite = GetComponentInChildren<SpriteRenderer>();
    }

    private void Start()
    {
        Destroy(gameObject, 1.0f);
    }

    private void FixedUpdate()
    {
        rigidbody.MovePosition(rigidbody.position + direction * speed * Time.deltaTime);
    }

    private void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        Unit unit = col.GetComponent<Unit>();

        if (unit && unit is Character)
        {
            unit.ReceiveDamage();
            Destroy(gameObject);
        }
    }
}
