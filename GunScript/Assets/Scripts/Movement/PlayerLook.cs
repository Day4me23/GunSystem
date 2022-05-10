using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerLook : MonoBehaviourPunCallbacks
{
    public PhotonView view;
    public float mouseSensitivity = 500f;
    public Transform playerBody;
    float xRotation = 0f;
    public bool isFollowing = true;
    Transform cameraTransform;
    public Camera cam;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        if (!photonView.IsMine)
        {
            cam.enabled = false;
        }
        //view = GetComponent<PhotonView>();
    }

    public void LateUpdate()
    {
        if (photonView.IsMine && cameraTransform == null && isFollowing)
        {
            OnStartFollowing();
        }
    }
    private void Update()
    {
        if (photonView.IsMine && ( GameManager.instance == null || !(GameManager.instance.gameIsOver)))
        {
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);

            transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

            playerBody.Rotate(Vector3.up * mouseX);
        }
        
    }

    public void OnStartFollowing()
    {
        cameraTransform = Camera.main.transform;
        isFollowing = true;
    }
}
