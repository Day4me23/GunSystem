using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Photon.Pun;

public class GameManager : MonoBehaviourPunCallbacks
{
    #region
    public static GameManager instance;

    private void Awake()
    {
        photonView = GetComponent<PhotonView>();
        PhotonNetwork.AutomaticallySyncScene = true;
        if (instance != null)
            Destroy(gameObject);
        instance = this;
        timer = 15;
        phase = Phase.buy;
        
        //team2.Add(Instantiate(dummy, spawn2).GetComponent<Player>());
        if (Player.localPlayerInstance == null)
        {
            if (PhotonNetwork.IsMasterClient)
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
        for (int i = 0; i < team1.Count; i++)
            team1[i].setTeam(Team.team1);
        for (int i = 0; i < team2.Count; i++)
            team2[i].setTeam(Team.team2);

        //team2.Add(PhotonNetwork.Instantiate(dummy.name, new Vector3(18, -12, -22), Quaternion.identity).GetComponent<Player>());
    }
    #endregion
    public List<Player> team1 = new List<Player>();
    public List<Player> team2 = new List<Player>();
    const int roundTotal = 10;
    public Text winText;
    public Transform spawn1;
    [SerializeField]
    PhotonView photonView;
    public Transform spawn2;
    public Transform deadZone;
    [HideInInspector]
    public float timer;
    public int currentRound = 0;
    public Bomb bomb;
    public Team currentAttacker = Team.team1;
    public Phase phase;
    public GameObject dummy;
    public GameObject player;
    public GameObject gameOver;
    public bool gameIsOver = false;

    void Update()
    {
        if (gameIsOver)
        {
            return;
        }
        if (phase == Phase.buy)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
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
                photonView.RPC("EndRound", RpcTarget.AllBuffered, team2);
            if (currentAttacker == Team.team2)
                photonView.RPC("EndRound", RpcTarget.AllBuffered, team1);
        }
        else if (phase == Phase.bomb && timer <= 0)
            photonView.RPC("EndRound", RpcTarget.AllBuffered, currentAttacker);
        else
            photonView.RPC("EndRound", RpcTarget.AllBuffered, Team.team2);

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
            photonView.RPC("EndRound", RpcTarget.AllBuffered, team2);
        }
        else if (!team2Alive && bomb == Bomb.notPlanted)
        {
            photonView.RPC("EndRound", RpcTarget.AllBuffered, team1);
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

    [PunRPC]
    public void EndRound(Team team)
    {
        GameOver(team);
        /*if (team == Team.team1)
            PointManager.instance.score1++;
        if (team == Team.team2)
            PointManager.instance.score2++;
        if (PhotonNetwork.IsMasterClient)
            PhotonNetwork.LoadLevel(SceneManager.GetActiveScene().name);*/
    }

    public void End(Team team)
    {
        photonView.RPC("EndRound", RpcTarget.AllBuffered, team);
    }

    public void GoToMainMenu()
    {
        PhotonNetwork.LeaveRoom();
        SceneManager.LoadScene("MainMenu");
    }
    public void GameOver(Team team)
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        gameIsOver = true;
        gameOver.SetActive(true);
        if (team == Team.team1)
            winText.text = "Attacker Wins...";
        else
            winText.text = "Defender Wins...";
    }

}
public enum Team
{
    none, team1, team2
}
public enum Bomb
{
    notPlanted, planting, planted, defused
}

public enum Phase
{
    buy, normal, bomb
}
