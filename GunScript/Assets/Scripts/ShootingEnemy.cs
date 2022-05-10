using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShootingEnemy : MonoBehaviour
{
    public int maxHealth = 100;
    //public Text playerScore;
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
       // playerScore.text = counter++.ToString();
        Respawn();
        Destroy(gameObject);
    }
    public void Respawn()
    {
        RangeManager.instance.IncreaseScore();
        RangeManager.instance.reference = Instantiate(playerPrefab, new Vector3(Random.Range(5, 21), 2.5f, Random.Range(-2, -18)), Quaternion.identity);
    }
}
