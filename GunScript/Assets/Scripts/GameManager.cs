using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class GameManager : MonoBehaviourPunCallbacks
{
    #region
    public static GameManager instance;

    private void Awake()
    {
        if (instance != null)
            Destroy(gameObject);
        instance = this;
        DontDestroyOnLoad(gameObject);
        //team2.Add(Instantiate(dummy, spawn2).GetComponent<Player>());
        if (Player.localPlayerInstance == null)
        {
            if (PhotonNetwork.IsMasterClient == true)
            {
                Debug.Log("Host join team 1.");
                team1.Add(PhotonNetwork.Instantiate(player.name, new Vector3(18, 5, 25), Quaternion.identity).GetComponent<Player>());
            }
            else
            {
                Debug.Log("New player joined team 2.");
                team2.Add(PhotonNetwork.Instantiate(player.name, new Vector3(18, 5, -24), Quaternion.identity).GetComponent<Player>());
            }
        }
        //team2.Add(PhotonNetwork.Instantiate(dummy.name, new Vector3(18, -12, -22), Quaternion.identity).GetComponent<Player>());
    }
    #endregion
    public List<Player> team1 = new List<Player>();
    public List<Player> team2 = new List<Player>();
    const int roundTotal = 10;
    public Transform spawn1;
    public Transform spawn2;
    public Transform deadZone;
    [HideInInspector]
    public float timer;
    public int currentRound = 0;
    public Bomb bomb;
    Team currentAttacker = Team.team1;
    int team1Score = 0;
    int team2Score = 0;
    public Phase phase;
    public GameObject dummy;
    public GameObject player;

    void Update()
    {
        if (timer > 0)
            timer -= Time.deltaTime;
        else if (phase == Phase.buy)
        {
            timer = 90;
            phase = Phase.normal;
        }
        else if (phase == Phase.normal && Bomb.planting == bomb)
        {
            timer = 0;
            if (currentAttacker == Team.team1)
                EndRound(Team.team2);
            if (currentAttacker == Team.team2)
                EndRound(Team.team1);
        }
        else if (phase == Phase.bomb && timer <= 0)
            EndRound(currentAttacker);
        
    }

    public void OnPlayerDeath()
    {
        bool team1Alive = false;
        bool team2Alive = false;

        for (int i = 0; i < team1.Count; i++)
            if (team1[i].alive)
                team1Alive = true;
            
        for (int i = 0; i < team2.Count; i++)
            if (team2[i].alive)
                team2Alive = true;
        if (!team1Alive && bomb == Bomb.notPlanted)
        {
            EndRound(Team.team2);
        }
        else if (!team2Alive && bomb == Bomb.notPlanted)
        {
            EndRound(Team.team1);
        }
    }

    public void NewRound()
    {
        phase = Phase.buy;
        timer = 15;
        bomb = Bomb.notPlanted;
        currentRound++;
        for (int i = 0; i < team1.Count; i++)
        {
            team1[i].alive = true;
            //team2[i].alive = true;
            team1[i].transform.position = spawn1.position;
            //team2[i].transform.position = spawn2.position;
        }


        if (currentRound > roundTotal / 2)
            currentAttacker = Team.team2;
        else
            currentAttacker = Team.team1;
 
    }

    public void EndRound(Team team)
    {
        if (team == Team.team1)
            team1Score++;
        if (team == Team.team2)
            team2Score++;
    }

}
public enum Team
{
    team1, team2
}
public enum Bomb
{
    notPlanted, planting, planted, defused
}

public enum Phase
{
    buy, normal, bomb
}
