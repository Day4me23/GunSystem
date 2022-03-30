using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    public GameObject assault;
    public GameObject sniper;
    public GameObject lmg;
    public GameObject smg;
    public GameObject pistol;
    public GameObject shotgun;
    private GameObject temp;
    public bool hasSecondary;

    public void BuyAR()
    {
        if (temp != null)
            DeleteWeapon(temp);
        
            temp = Instantiate(assault, GameObject.Find("Camera").transform);
    }
    public void BuySniper()
    {
        if (temp != null)
            DeleteWeapon(temp);

        temp = Instantiate(sniper, GameObject.Find("Camera").transform);
    }
    public void BuySMG()
    {
        if (temp != null)
            DeleteWeapon(temp);

        temp = Instantiate(smg, GameObject.Find("Camera").transform);
    }
    public void BuyLMG()
    {
        if (temp != null)
            DeleteWeapon(temp);

        temp = Instantiate(lmg, GameObject.Find("Camera").transform);
    }
    public void BuyPistol()
    {
        if (temp != null)
            DeleteWeapon(temp);

        temp = Instantiate(pistol, GameObject.Find("Camera").transform);
    }
    public void BuyShotgun()
    {
        if (temp != null)
            DeleteWeapon(temp);

        temp = Instantiate(shotgun, GameObject.Find("Camera").transform);
    }
    public void DeleteWeapon(GameObject temp)
    {
        Debug.Log("Deleting Primary Weapon");
        Destroy(temp);
    }
}
