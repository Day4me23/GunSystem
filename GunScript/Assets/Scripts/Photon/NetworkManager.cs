using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;

public class NetworkManager : MonoBehaviourPunCallbacks
{
    
    public static NetworkManager instance;
    private void Awake()
    {
        instance = this;
    }
    public override void OnCreatedRoom()
    {
        SceneManager.LoadScene("Waiting");
    }
    public override void OnJoinedRoom()
    {
        SceneManager.LoadScene("Waiting");
    }
    public void CreateRoom(string input)
    {
        PhotonNetwork.CreateRoom(input);

    }

    public void JoinRoom(string input)
    {
        PhotonNetwork.JoinRoom(input);
    }

    /*public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel("Game");
    }*/
}
