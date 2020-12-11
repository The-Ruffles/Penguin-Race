using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class SimpleCharacterController : MonoBehaviour
{
    // Variable list
    public CharacterController cc;
    public SerialController serialController;
    public ArduinoInputListener inputListener;
    public float moveSpeed = 10;
    float currentSpeed;
    public float gravity = -9.8f;
    public float jumpHeight = 33;
    public Transform groundCheck;
    public float groundDistance;
    public LayerMask groundMask;

    Vector3 move;
    Vector3 velocity;
    public float speedBuff, speedDebuff, rotationSpeed;
    public string redLightName, yellowLightName, allLightsOffName;
    public Color speedBoostColor, normalSpeedColor, speedDecreaseColor;
    public string buzzerActivate;
    public GameObject splashAudio, iceCracking;
    
    public string speedBoostMessage, normalSpeedMessage, speedDecreaseMessage;
    public TMP_Text speedMessage;
    public Image speedTypeDialogImage;

    bool isGrounded;
    Vector3 startingPosition;
    GameObject fish, completedLevelPanel;
    LevelManager levelManager;
    public float fishCooldown = 5f;

    public bool btnInput = false;

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
        inputListener = GetComponent<ArduinoInputListener>();
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
        
        move = transform.right * x + transform.forward * z;
        
        if (inputListener.xInput != 0 || inputListener.yInput != 0)
        {
            move = (transform.right * -inputListener.xInput + transform.forward * -inputListener.yInput).normalized;
        }
       
        //if countdown timer is active, no movement allowed
        if (!levelManager.isCountdownTimerActive && !levelManager.levelFinished)
        {
            cc.Move(move * currentSpeed * Time.deltaTime);

            if (isGrounded)
            {
                if (Input.GetButtonDown("Jump"))
                {
                    Jump();
                }

                else if (btnInput == true)
                {
                    Jump();
                    btnInput = false;
                }
            }
        }

        velocity.y += gravity * Time.deltaTime;
        cc.Move(velocity * Time.deltaTime);
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.CompareTag ("Sea"))
                {
                    //please rememeber to activate this libe before build/ or test
                    BuzzerActivate();
                    splashAudio.GetComponent<AudioSource>().Play();
                    TurnSpeedEffectsOff();
                    currentSpeed = moveSpeed;
                    transform.position = startingPosition;
                }

        if (hit.gameObject.CompareTag ("GoodFish"))
                {
                    Debug.Log("Speed buff");
                    fish = hit.gameObject;
                    FishMechanic(speedBuff, redLightName, speedBoostMessage, speedBoostColor);
                }
        
        if (hit.gameObject.CompareTag ("BadFish"))
                {
                    Debug.Log("Speed Debuff");
                    fish = hit.gameObject;
                    FishMechanic(speedDebuff, yellowLightName, speedDecreaseMessage, speedDecreaseColor);
                }

        if (hit.gameObject.CompareTag ("Igloo"))
                {
                    // Links to level manager
                    //calls method
                    levelManager.LevelFinished(levelManager.completedLevelPanel);
                }

        if (hit.gameObject.CompareTag ("Final_Igloo"))
                {
                    SceneManager.LoadScene("EndMenu");
                }

        if (hit.gameObject.CompareTag("PlatformDestroy"))
        {
            iceCracking.GetComponent<AudioSource>().Play();
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
    void FishMechanic(float speedMultiplier, string lightColour, string currentMessage, Color currentUIColor)
    { 
        Destroy(fish);
        TurnSpeedEffectsOff();
        //add lights here (prefered)
        speedMessage.text = currentMessage;
        speedTypeDialogImage.color = currentUIColor;
        serialController.SendSerialMessage(lightColour);
        currentSpeed = moveSpeed * speedMultiplier;
        StartCoroutine("FishTimer");
    }
    //what to do after fish timer finishes. basically resets current speed to original speed
    IEnumerator FishTimer()
    {
       yield return new WaitForSeconds(fishCooldown);
       TurnSpeedEffectsOff();
       currentSpeed = moveSpeed;
       Debug.Log("Speed Reset");
    }

    void TurnSpeedEffectsOff()
    {
        speedTypeDialogImage.color = normalSpeedColor;
        speedMessage.text = normalSpeedMessage;
        serialController.SendSerialMessage(allLightsOffName);
    }

    void BuzzerActivate()
    {
        serialController.SendSerialMessage(buzzerActivate);
    }

    // public void Movement(Vector2 input)
    // {
    //     Debug.Log("Input: " + input);
    //     move = transform.forward * -input.y;
    //     //transform.Rotate(transform.up * -input.x * -rotationSpeed * Time.deltaTime);
    //     Debug.Log("move vector 3: " + move);
    //     cc.Move(move * currentSpeed * Time.deltaTime);
    // }

    void Jump()
    {
        Debug.Log("Player Jumps weeeeeee!!!!!");
        velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity); 
    }
}