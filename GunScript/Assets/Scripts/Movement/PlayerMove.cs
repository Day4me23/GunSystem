using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerMove : MonoBehaviourPunCallbacks
{
    public PhotonView view;
    public CharacterController controller;
    public Transform groundCheck;
    public float groundDistance = .4f;
    public LayerMask groundMask;
    bool isGrounded;
    public float speed = 12f;
    public Vector3 velocity;
    public float gravity = -9.81f;
    public float jumpHeight = 3f;

    private void Start()
    {
        //view = GetComponent<PhotonView>();
    }
    private void Update()
    {
        
        if (photonView.IsMine && (GameManager.instance == null || !(GameManager.instance.gameIsOver)))
        {
            Debug.Log(photonView.ViewID);
            isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

            if (isGrounded && velocity.y < 0)
            {
                velocity.y = -2f;
            }
            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");

            Vector3 move = transform.right * x + transform.forward * z;

            controller.Move(move * speed * Time.deltaTime);

            if (Input.GetButtonDown("Jump") && isGrounded)
            {
                velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            }

            velocity.y += gravity * Time.deltaTime;

            controller.Move(velocity * Time.deltaTime);
        }
          
        else
        {
            Debug.Log("You Can't Move...");
        }
        
    }
}
