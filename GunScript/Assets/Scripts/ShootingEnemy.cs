using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingEnemy : MonoBehaviour
{
    public void TakeDamage(int damage)
    {
        Debug.Log("Enemy Hit For -" + damage);
    }
}
