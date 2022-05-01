using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class MapSelection : MonoBehaviour
{
    public Button level1;
    public Button level2;
    public Button level3;
    public Text text;


    private void Start()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
    }
    private void Update()
    {
        bool temp = PhotonNetwork.CurrentRoom.PlayerCount > 1 && PhotonNetwork.IsMasterClient;
            level1.interactable = temp;
            level2.interactable = temp;
            level3.interactable = temp;
        if (PhotonNetwork.CurrentRoom.PlayerCount > 1) {
            if (PhotonNetwork.IsMasterClient)
                text.text = "Select Map...";
        else
                text.text = "Waiting For Host To Select Map...";
        }

    }
    public void Level1()
    {
        if (PhotonNetwork.IsMasterClient)
            PhotonNetwork.LoadLevel("Map1");
        else
            Debug.Log("You are not the host.");
    }
    public void Level2()
    {
        if (PhotonNetwork.IsMasterClient)
            PhotonNetwork.LoadLevel("Map1");
        else
            Debug.Log("You are not the host.");
    }
    public void Level3()
    {
        if (PhotonNetwork.IsMasterClient)
            PhotonNetwork.LoadLevel("Map1");
        else
            Debug.Log("You are not the host.");
    }
}
