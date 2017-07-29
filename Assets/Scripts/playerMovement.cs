using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour {

    public float speed = 5;
    public float increaseSpeedTime;
    public float playerAcceleration = 10;
    private CharacterController playerController;

    public bool running;


	// Use this for initialization
	void Start ()
    {
       
        playerController = GetComponent<CharacterController>();
    }
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        playerController.Move((Vector3.forward *speed)* Time.deltaTime);
        Debug.Log(speed);

        if (!playerController.isGrounded)
        {
            playerController.Move((Vector3.down * speed) * Time.deltaTime);
        }
    }

    void Update()
    {
        if (increaseSpeedTime < 0) // increment of 10% speed every 10 seconds
        {
            increaseSpeedTime = 10;
            speed = speed * 1.1f;
        }

        increaseSpeedTime -= Time.deltaTime; 
    }
}
