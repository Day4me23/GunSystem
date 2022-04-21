using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : ShootingEnemy
{
    public bool alive;
    public int currency;

    public override void EnemyKill()
    {
        base.EnemyKill();
        alive = false;
    }
    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }


}
