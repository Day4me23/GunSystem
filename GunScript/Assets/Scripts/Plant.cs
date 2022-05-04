using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Plant : MonoBehaviour
{
    public bool isInPlantZone = false;
    public bool isInDefuseZone = false;
    public float time = 3;
    public float timer = 0;
    private Team team;
    Player player;
    public GameObject moBamba;

    private void Start()
    {
        player = GetComponent<Player>();
    }
    private void Update()
    {
        if ((isInPlantZone || isInDefuseZone) && Input.GetKey(KeyCode.F))
            timer += Time.deltaTime;
        if (Input.GetKeyUp(KeyCode.F))
            timer = 0;
        if (timer >= time)
        {
            if (isInPlantZone)
            {
                PhotonNetwork.Instantiate(moBamba.name, transform.position, Quaternion.identity);
                Destroy(this);
            }
            if (isInDefuseZone)
            {
                Debug.Log("Defused");
                if (GameManager.instance.currentAttacker == Team.team1)
                    GameManager.instance.End(Team.team2);
                else
                    GameManager.instance.End(Team.team1);
            }  
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Trigger"))
        {
            if (player.getTeam() == GameManager.instance.currentAttacker)
            {
                isInPlantZone = true;
                Debug.Log("Collision");
            }
            else
                isInDefuseZone = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.CompareTag("Trigger"))
        {
            if (player.getTeam() == GameManager.instance.currentAttacker)
            {
                isInPlantZone = false;
                Debug.Log("Collision");
            }
            else
                isInDefuseZone = false;
        }
    }
    
}
