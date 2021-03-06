using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Player : ShootingEnemy
{
    public static GameObject localPlayerInstance;
    public PhotonView photonView;
    public bool alive;
    public int currency;
    private void Awake()
    {
        if (photonView.IsMine)
        {
            Player.localPlayerInstance = this.gameObject;
        }
    }
    public override void EnemyKill()
    {
        base.EnemyKill();
        alive = false;
    }
    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        GameManager.instance.team1.Add(this);
        GameManager.instance.NewRound();
    }


}
