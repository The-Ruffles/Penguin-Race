using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArduinoInputListener : MonoBehaviour
{
    SimpleCharacterController simpleCharacterController;
    Vector2 moveJoystick;
    void Start() 
    {
        simpleCharacterController = GetComponent<SimpleCharacterController>();
    }
    
    void OnMessageArrived(string msg)  
        {
            Debug.Log("Recieved Message: " + msg);
            //Debug.Log("jumping: " + btnInput);
            if(msg == "3")
            {
                simpleCharacterController.btnInput = true;
            }

            if(msg == "W")
            {
                moveJoystick = new Vector2(0, 1);
                simpleCharacterController.Movement(moveJoystick);
                Debug.Log("Forward");
            }

            else if(msg == "A")
            {
                moveJoystick = new Vector2(-1,0);
                simpleCharacterController.Movement(moveJoystick);
                Debug.Log("Left");
            }

            else if(msg == "S")
            {
                moveJoystick = new Vector2(0, -1);
                simpleCharacterController.Movement(moveJoystick);
                Debug.Log("Back");
            }

            else if(msg == "D")
            {
                moveJoystick = new Vector2(1, 0);
                simpleCharacterController.Movement(moveJoystick);
                Debug.Log("Right");
            }

            else{
                moveJoystick = Vector2.zero;
                simpleCharacterController.Movement(moveJoystick);
                Debug.Log("NoInput");
            }
        }
    
    void OnConnectionEvent(bool success)
    {
        Debug.Log(success ? "Device Connected" : "Device discounted");
    }
    
}
