﻿using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SimpleCharacterController : MonoBehaviour
{
    // Start is called before the first frame update
    public CharacterController controller;
    public float moveSpeed = 10;
    float currentSpeed;
    public float gravity = -9.8f;
    public float jumpHeight = 33;
    public Transform groundCheck;
    public float groundDistance;
    public LayerMask groundMask;
    Vector3 velocity;
    public float speedBuff, speedDebuff;
    bool isGrounded;
    Vector3 startingPosition;
    public string nextSceneName;
    GameObject fish, completedLevelPanel;
    LevelManager levelManager;
    public float fishCooldown = 5f;

// Update is called once per frame
    
    void Start()
    {
        startingPosition = transform.position;
        currentSpeed = moveSpeed;
        levelManager = GameObject.FindWithTag("LevelManager").GetComponent<LevelManager>();

    }
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;
        if (!levelManager.isCountdownTimerActive)
        {
            controller.Move(move * currentSpeed * Time.deltaTime);

            if (Input.GetButtonDown("Jump") && isGrounded)
            {
                velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity); 
            }
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.CompareTag ("Sea"))
                {
                    //add buzzer here...
                    currentSpeed = moveSpeed;
                    transform.position = startingPosition;
                }

        if (hit.gameObject.CompareTag ("GoodFish"))
                {
                    Debug.Log("Speed buff");
                    fish = hit.gameObject;
                    //add lights here(idealy in function tho)...
                    FishMechanic(speedBuff);
                }
        
        if (hit.gameObject.CompareTag ("BadFish"))
                {
                    Debug.Log("Speed Debuff");
                    fish = hit.gameObject;
                    //add lights here(idealy in function tho)...
                    FishMechanic(speedDebuff);
                }

        if (hit.gameObject.CompareTag ("Igloo"))
                {
                    Debug.Log("FINISHED!!!!!");
                    // Links to level manager
                    levelManager.LevelFinished(levelManager.completedLevelPanel);
                }
        if (hit.gameObject.CompareTag("PlatformDestroy"))
        {
            Debug.Log("Falling! AHHHH!!!!");
            StartCoroutine("DelayedDestroy", hit.gameObject);
        }      
    }
    IEnumerator DelayedDestroy(GameObject other)
    {
       yield return new WaitForSeconds(1f);
       Destroy(other);
    }
    void FishMechanic(float speedMultiplier)
    {
        Destroy(fish);
        //add lights here (prefered)
        currentSpeed = moveSpeed * speedMultiplier;
        StartCoroutine("FishTimer");
    }
    IEnumerator FishTimer()
    {
       yield return new WaitForSeconds(fishCooldown);
       currentSpeed = moveSpeed;
       Debug.Log("Speed Reset");
    }
}