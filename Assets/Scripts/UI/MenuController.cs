using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{ 
        public bool IsPaused;
        [SerializeField] GameObject panelPause;
        [SerializeField] private KeyCode pauseButton;
        [SerializeField] private Character character;
        

    private void Update()
        {
          if (Input.GetKeyDown(pauseButton))
           {
              IsPaused = !IsPaused;
           }
        

          if (IsPaused)
            {
            Cursor.visible = true;
              panelPause.SetActive(true);
              Time.timeScale = 0;
            }else {
            Cursor.visible = false;
                 panelPause.SetActive(false);
                 Time.timeScale = 1; 
              }
                                     
        }

 }
