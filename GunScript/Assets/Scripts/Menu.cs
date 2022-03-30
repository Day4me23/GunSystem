using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    public GameObject menuUI;
    public static bool openShop = false;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (openShop)
            {
                CloseWeaponShop();
            }
            else
            {
                OpenWeaponShop();
            }
            
        }   
    }

    void CloseWeaponShop()
    {
        menuUI.SetActive(false);
        openShop = false;
    }

    void OpenWeaponShop()
    {
        menuUI.SetActive(true);
        openShop = true;
    }
}
