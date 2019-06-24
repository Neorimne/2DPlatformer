using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootMonster : Unit
{
    [SerializeField] private float rate = 2.0f;
    [SerializeField] private Color BulletColor = Color.white;
    private new AudioSource audio;
    private Character character;
    private Bullet bullet;
    private SpriteRenderer sprite;
    private bool facingRight = true;
    private float bulletDirection = -1;


    private void Awake()
    {
        audio = GetComponent<AudioSource>();
        character = FindObjectOfType<Character>();
        sprite = GetComponentInChildren<SpriteRenderer>();
        bullet = Resources.Load<Bullet>("Bullet");
    }

    private void Start()
    {
        InvokeRepeating("Shoot", rate, rate);
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
                bulletDirection = 1;

            }
            else
            {
                sprite.flipX = false;
                bulletDirection = -1;
            }
        }
        catch
        {
            facingRight = true;
        }
        
        
    }
    private void Shoot()
    {
        Vector2 position = transform.position;
        position.y += 0.3f;
        position.x -= 0.2f;

        Bullet newBullet = Instantiate(bullet, position, Quaternion.identity) as Bullet;
        newBullet.Color = BulletColor;
        newBullet.Direction = bulletDirection * newBullet.transform.right;
    }


    public void OnTriggerEnter2D(Collider2D collider)
    {
        Unit unit = collider.GetComponent<Unit>();

        if (unit && unit is Character)
        {
            
            if ((Mathf.Abs(unit.transform.position.x - transform.position.x) <= 0.91f) && (Mathf.Abs(unit.transform.position.y - transform.position.y) >= 0.3f))
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
