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
    public int batteryEnergyIncrement = 50;
    public int colissionDecrement = 10;
    public float horizontalSpeed = 2.0f;
    private Animator animator, hitDamage;
    private bool deadFlag = false;
    private bool jumpingKey = false;
    public Canvas canvas;
    public int batteryLosePerSecond = 1;
    public Image healthBar;
    private int speedColission = 2;

    private bool stopScoreCount = false;

 


    // Use this for initialization
    void Start () {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        hitDamage = canvas.GetComponent<Animator>();

    }
	
	// Update is called once per frame
	void FixedUpdate () {
        if (!deadFlag)
        {
            EnergyController();
            MovementController();
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            jumpingKey = true;
        }
    }

    public void MovementController()
    {
        moveIndicator = Vector3.zero;
        moveIndicator.x = Input.GetAxisRaw("Horizontal") * horizontalSpeed;
        //player Jumping
        if (!controller.isGrounded)
        {
            verticalAcceleration -= gForce * Time.deltaTime;
            moveIndicator.x = 0.0f;
        }
        else
        {
            //player touching ground
            if (jumpingKey && !deadFlag)
            {
                animator.SetTrigger("jumpTrigger");
                verticalAcceleration += jumpPower * Time.deltaTime;
                jumpingKey = false;
            }
            else
            {
                verticalAcceleration = -0.5f;
            }
        }
        moveIndicator.y = verticalAcceleration;
        //when player enter a hole
        if (transform.position.y < 0.80)
        {
            moveIndicator.z = 1;
            moveIndicator.y = -1000 * Time.deltaTime;
            stopScoreCount = true;
            //controller.detectCollisions = !controller.detectCollisions;
        }
        else
        {
            if (!deadFlag)
                moveIndicator.z = playerSpeed;
            else
                moveIndicator.z = 0;
        }

        controller.Move(moveIndicator * Time.deltaTime);
    }

    public void EnergyController()
    {
        if (energyLevel <= 1)
        {
            deadFlag = true;
            animator.SetTrigger("deadTrigger");
        }
        energyLevel -= Time.deltaTime * batteryLosePerSecond;
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
        batteryLosePerSecond += 2;
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
            moveIndicator = Vector3.zero;
            moveIndicator.z = playerSpeed  - speedColission;
            controller.Move(moveIndicator * Time.deltaTime);
            collision.enabled = !collision.enabled;
            DecBatteryEnergyLevel(colissionDecrement);
            hitDamage.SetTrigger("HitDamage");
            animator.SetTrigger("HitTrigger");
        }
    }

    public bool GetDeadFlag()
    {
        return deadFlag;
    }

    public bool GetsStopScoreCount()
    {
        return stopScoreCount;
    }
}
