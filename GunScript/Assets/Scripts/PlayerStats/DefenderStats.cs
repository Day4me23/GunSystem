using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenderStats : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth { get; private set; }


    private void Awake()
    {
        currentHealth = maxHealth;
    }
    public void TakeDamage(int damage)
    {
        damage = Mathf.Clamp(damage, 0, int.MaxValue);
        currentHealth -= damage;
        Debug.Log(transform.name + " takes " + damage + " damage.");
        if (currentHealth <= 0)
        {
            Debug.Log("enemy dead");
        }
    }
}
