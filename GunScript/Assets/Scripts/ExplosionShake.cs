using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionShake : MonoBehaviour
{
    public CameraShake cameraShake;
    public float duration = .05f;
    public float mag = .4f;
    void Update()
    {
        /*if (Input.GetKeyDown(KeyCode.R))
        {
            StartCoroutine(cameraShake.Shake(duration, mag));
        }*/
    }
}
