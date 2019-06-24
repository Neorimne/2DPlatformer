using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : MonoBehaviour
{
    [SerializeField] private float speed = 3.0f;
   
    private Vector2 velocity;
    new private Rigidbody2D rigidbody;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>(); 
    }
    private void Start()
    {
        velocity = new Vector2( 0 , 0.2f);
        InvokeRepeating("velocityInv", 0.75f, 0.75f);
    }
    private void FixedUpdate()
    {
        rigidbody.MovePosition(rigidbody.position + velocity * Time.deltaTime * speed);
    }
   private void velocityInv()
    {
        velocity.y *= -1;
    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        Unit unit = collider.GetComponent<Unit>();
        if (unit && unit is Character)
        {
            unit.ReceiveDamage();
        }
    }
}
