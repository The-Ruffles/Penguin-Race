using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SimpleCharacterController : MonoBehaviour
{
    // Start is called before the first frame update
    public CharacterController controller;
    public float moveSpeed = 10;
    public float gravity = -9.8f;
    public float jumpHeight = 33;
    public Transform groundCheck;
    public float groundDistance;
    public LayerMask groundMask;
    Vector3 velocity;

    bool isGrounded;

    Vector3 startingPosition;

    public string nextSceneName;

    GameObject fish;

// Update is called once per frame
    
    void Start()
    {
        startingPosition = transform.position;    
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
        controller.Move(move * moveSpeed * Time.deltaTime);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity); 
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.CompareTag ("Sea"))
                {
                    transform.position = startingPosition;
                }

        if (hit.gameObject.CompareTag ("GoodFish"))
                {
                    //Speed increases
                    fish = hit.gameObject;
                    FishMechanic(1.2f);
                }
        
        if (hit.gameObject.CompareTag ("BadFish"))
                {
                    //Speed decreases
                    fish = hit.gameObject;
                    FishMechanic(1.2f);
                }

        if (hit.gameObject.CompareTag ("Igloo"))
                {
                    Debug.Log("FINISHED!!!!!");
                    SceneManager.LoadScene(nextSceneName);
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
    }
}