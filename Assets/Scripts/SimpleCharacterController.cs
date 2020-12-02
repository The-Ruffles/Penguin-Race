using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SimpleCharacterController : MonoBehaviour
{
    // Variable list
    public CharacterController controller;
    public SerialController serialController;
    public float moveSpeed = 10;
    float currentSpeed;
    public float gravity = -9.8f;
    public float jumpHeight = 33;
    public Transform groundCheck;
    public float groundDistance;
    public LayerMask groundMask;
    Vector3 velocity;
    public float speedBuff, speedDebuff;
    public string redLightName, yellowLightName, allLightsOffName;
    bool isGrounded;
    Vector3 startingPosition;
    GameObject fish, completedLevelPanel;
    LevelManager levelManager;
    public float fishCooldown = 5f;

    /*  
        start method

        assigning player transfrom to a variable
        assigning move speed to current speed
        getting the script from the object with tag
    */
    
    void Start()
    {
        serialController = GameObject.Find("SerialController").GetComponent<SerialController>();
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
        
        //if countdown timer is active, no movement allowed
        if (!levelManager.isCountdownTimerActive && !levelManager.levelFinished)
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
                    FishMechanic(speedBuff, redLightName);
                }
        
        if (hit.gameObject.CompareTag ("BadFish"))
                {
                    Debug.Log("Speed Debuff");
                    fish = hit.gameObject;
                    //add lights here(idealy in function tho)...
                    FishMechanic(speedDebuff, yellowLightName);
                }

        if (hit.gameObject.CompareTag ("Igloo"))
                {
                    // Links to level manager
                    //calls method
                    levelManager.LevelFinished(levelManager.completedLevelPanel);
                }
        if (hit.gameObject.CompareTag("PlatformDestroy"))
        {
            StartCoroutine("DelayedDestroy", hit.gameObject);
        }      
    }
    IEnumerator DelayedDestroy(GameObject other)
    {
       yield return new WaitForSeconds(1f);
       Destroy(other);
    }
    /*  
        Fish mechanic method
        
        making a method that can be used in both buff/debuff
        changed speed by the speed muliplier variable
        started cooldown till the effect wears off
    */
    void FishMechanic(float speedMultiplier, string lightColour)
    {
        Destroy(fish);
        //add lights here (prefered)
        serialController.SendSerialMessage(lightColour);
        currentSpeed = moveSpeed * speedMultiplier;
        StartCoroutine("FishTimer");
    }
    //what to do after fish timer finishes. basically resets current speed to original speed
    IEnumerator FishTimer()
    {
       yield return new WaitForSeconds(fishCooldown);
       currentSpeed = moveSpeed;
       serialController.SendSerialMessage(allLightsOffName);
       Debug.Log("Speed Reset");
    }
}