using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotor : MonoBehaviour {

    private CharacterController controller;
    private float playerSpeed = 5.0f;
    private float verticalAcceleration = 0f;
    private float gForce = 10.0f;
    private Vector3 moveIndicator;
    private float jumpPower = 500;

    // Use this for initialization
    void Start () {
        controller = GetComponent<CharacterController>();	
	}
	
	// Update is called once per frame
	void Update () {
        moveIndicator = Vector3.zero;
        if (!controller.isGrounded)
        {
            verticalAcceleration -= gForce * Time.deltaTime;
        }
        else
        {
            //player touching ground
            if (Input.GetKeyDown(KeyCode.Space))
            {
                verticalAcceleration += jumpPower * Time.deltaTime;
            }
            else
            {
                verticalAcceleration = -0.5f;
            }
        }
        
        moveIndicator.y = verticalAcceleration;
        moveIndicator.x = Input.GetAxisRaw("Horizontal") * playerSpeed;

        
        moveIndicator.z = playerSpeed;

        controller.Move(moveIndicator * Time.deltaTime);
	}

  
}
