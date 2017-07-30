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
    public int batteryEnergyDecrement = 10;
    public float horizontalSpeed = 2.0f;

    public Image healthBar;


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
        moveIndicator.x = Input.GetAxisRaw("Horizontal") * horizontalSpeed;
        if (!controller.isGrounded)
        {
            verticalAcceleration -= gForce * Time.deltaTime;
            moveIndicator.x = 0.0f;
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
            Destroy(this.gameObject);
        energyLevel -= Time.deltaTime * 5;
        healthBar.fillAmount = energyLevel / 100;
        if (energyLevel > 60)
        {
            healthBar.color = new Color32(28, 242, 0, 255);
        }
        if (energyLevel < 60)
        {
            healthBar.color = new Color32(255, 249, 38, 255);
        }
        if (energyLevel < 25)
        {
            healthBar.color = new Color32(255, 0, 0, 255);
        }
    }

    public void SetLevelSpeed(float mod)
    {
        playerSpeed += mod;
    }

    private float PlusBatteryEnergyLevel (int amount)
    {
        if (energyLevel + amount >= 100)
            energyLevel = 100;
        else
            energyLevel += amount;
        return energyLevel;
    }

    private float DecBatteryEnergyLevel (int amount)
    {
        if (energyLevel - amount >= 1)
            energyLevel -= amount;
        else
            energyLevel = 0;
            
        return energyLevel;
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Battery")
        {
            Destroy(collision.gameObject);
            PlusBatteryEnergyLevel(batteryEnergyIncrement);
        }
        if (collision.gameObject.tag == "Obstacule")
        {
            Destroy(collision.gameObject);
            DecBatteryEnergyLevel(batteryEnergyIncrement);
        }
    }

}
