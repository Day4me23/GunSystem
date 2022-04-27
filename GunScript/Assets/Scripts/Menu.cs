using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public GameObject menuUI;
    public static bool openShop = false;
    public Text text;
    void Update()
    {
        if (GameManager.instance.phase == Phase.buy)
        {
            text.text = "Press B to open the buy menu.";
            if (Input.GetKeyDown(KeyCode.B))
                if (openShop)
                    CloseWeaponShop();
                else
                    OpenWeaponShop();
        }
        else
        {
            text.text = "";
            CloseWeaponShop();
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
