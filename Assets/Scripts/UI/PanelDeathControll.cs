using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelDeathControll : MonoBehaviour
{
    [SerializeField] Character character;
    [SerializeField] GameObject panelDeath;
    private bool death = false;
    private void Start()
    {
        
    }
    private void Update()
    {
        StartCoroutine(Ex());
        if (character.lives <= 0)
        {
            death = true;
        }
        
    }
    IEnumerator Ex ()
    {
        if (death)
        {
            
            yield return new WaitForSeconds(1.0f);
            Cursor.visible = true;
            panelDeath.SetActive(true);
            
        }
    }
}
