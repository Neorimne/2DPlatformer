using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchOn : MonoBehaviour
{

    [SerializeField] GameObject swithcOff;
    [SerializeField] AudioSource audioSpawn;
    private YellowDude dude;
    private bool spawn = false;
    private int count = 0;
    new private AudioSource audio;
    private float _direction = 1.0f;


    private void Awake()
    {
        audio = GetComponent<AudioSource>();
    }

    private void Start()
    {
        dude = Resources.Load<YellowDude>("YellowDude");
    }

    private void FixedUpdate()
    {
        if (spawn && count < 3) StartCoroutine(DudeSpawn());
        spawn = false;
        
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
            YellowDude[] dudes = FindObjectsOfType<YellowDude>();
            if (dudes.Length < 7)
            {
                spawn = true;
            }

            if (dudes.Length < 7 && count > 3)
            {
                spawn = true;
                count = 0;
            }
        }
        

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Unit unit = collision.GetComponent<Unit>();

        if (unit && unit is Character)
        {
            gameObject.SetActive(false);
            swithcOff.SetActive(true);
            spawn = false;
            count++;
        }
        
    }

    IEnumerator DudeSpawn()
    {
        //yield return new WaitForSeconds(0.2f);
        
        for (int i = 0; i < 3; i++)
        {
            yield return new WaitForSeconds(0.3f);
            audioSpawn.Play();
            Vector3 position = transform.position;
            position.x += 7.0f;
            position.z += 9.0f;
            YellowDude newDude = Instantiate(dude, position, Quaternion.identity) as YellowDude;
            newDude.Direction = new Vector2(_direction, 0);
            _direction *= -1;
        }
        
        spawn = false;
    }

   
}
