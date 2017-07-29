using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour {

    public float speed = 5;
    public float playerAcceleration = 10;
    private Rigidbody playerRigidBody;

    public bool running;


	// Use this for initialization
	void Start ()
    {
       
        playerRigidBody = GetComponent<Rigidbody>();
    }
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        if (playerRigidBody.velocity.z > speed)
        {
             
        }
        if (playerRigidBody.velocity.z < speed)
        {
            playerRigidBody.AddForce(transform.forward * playerAcceleration);
        }
		
	}

    void Update()
    {
        //Debug.Log(playerRigidBody.velocity);

    }
}
