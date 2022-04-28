using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Plant : MonoBehaviour
{
    public bool isInArea = false;
    public float time = 3;
    public float timer = 0;
    public GameObject moBamba;

    private void Update()
    {
        if (isInArea && Input.GetKey(KeyCode.F))
            timer += Time.deltaTime;
        if (Input.GetKeyUp(KeyCode.F))
            timer = 0;
        if (timer >= time)
        {
            PhotonNetwork.Instantiate(moBamba.name, transform.position, Quaternion.identity);
            Destroy(this);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Trigger"))
        {
            isInArea = true;
            Debug.Log("Collision");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.CompareTag("Trigger"))
            isInArea = false;
    }
}
