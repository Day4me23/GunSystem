using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoBamba : MonoBehaviour
{
    private void Awake()
    {
        GameManager.instance.timer = 45;
        GameManager.instance.phase = Phase.bomb;
    }
}
