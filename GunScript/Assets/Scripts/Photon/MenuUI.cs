using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuUI : MonoBehaviour
{
    public InputField createInput;
    public InputField joinInput;

    public void CreateRoom()
    {
        NetworkManager.instance.CreateRoom(createInput.text);
    }

    public void JoinRoom()
    {
        NetworkManager.instance.JoinRoom(joinInput.text);
    }
}
