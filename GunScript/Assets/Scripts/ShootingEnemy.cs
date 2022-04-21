using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShootingEnemy : MonoBehaviour
{
    public int maxHealth = 100;
    public Text playerScore;
    public Text timer;
    int counter;
    public GameObject playerPrefab;
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
            EnemyKill();
        }
    }
    public virtual void EnemyKill()
    {
        Destroy(gameObject);
        playerScore.text = counter++.ToString();
        Respawn();
    }
    public void Respawn()
    {
        Instantiate(playerPrefab, new Vector3(Random.Range(-5, 5), 2.5f, Random.Range(-10, 5)), Quaternion.identity);
    }
}
