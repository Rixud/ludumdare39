using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMotor : MonoBehaviour {

    private CharacterController controller;
    private float playerSpeed = 5.0f;
    private float verticalAcceleration = 0f;
    private float gForce = 25.0f;
    private Vector3 moveIndicator;
    private float jumpPower = 600;
    public float energyLevel = 100;
    public int batteryEnergyIncrement = 20;

    public Text energyText;


    // Use this for initialization
    void Start () {
        controller = GetComponent<CharacterController>();	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        MovementController();
        EnergyController();
    }

    public void MovementController()
    {
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
        //when player enter a hole
        if (transform.position.y < 0.76)
        {
            moveIndicator.z = 5;
            moveIndicator.y = -1000 * Time.deltaTime;
        }
        else
        {
            moveIndicator.z = playerSpeed;
        }

        controller.Move(moveIndicator * Time.deltaTime);
    }

    public void EnergyController()
    {
        if (energyLevel == 0)
            Destroy(this);
        energyLevel -= Time.deltaTime;
        energyText.text = ((int)energyLevel).ToString();
    }

    public void SetLevelSpeed(float mod)
    {
        playerSpeed += mod;
    }

    private float SetBatteryEnergyLevel (int amount)
    {
        if (energyLevel + amount >= 100)
            energyLevel = 100;
        else
            energyLevel += amount;
        return energyLevel;
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Battery")
        {
            Destroy(collision.gameObject);
            SetBatteryEnergyLevel(batteryEnergyIncrement);
            Debug.Log("Entro");
        }
    }

}
