using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShootingRangeMenu : MonoBehaviour
{
    public GameObject menuUI;
    public static bool openShop = false;
    public Text text;

    private void Awake()
    {
        text.text = "Press B to open the weapon menu.";
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            if (openShop)
                CloseWeaponShop();
            else
                OpenWeaponShop();
        }
    }

    void CloseWeaponShop()
    {
        menuUI.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        openShop = false;
    }

    void OpenWeaponShop()
    {
        menuUI.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        openShop = true;
    }
}
