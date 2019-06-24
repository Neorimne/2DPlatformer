using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTrigger : MonoBehaviour
{
    [SerializeField] new CameraControll camera;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        camera.damping = 0;
    }
}
