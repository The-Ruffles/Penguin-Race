using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArduinoInputListener : MonoBehaviour
{
    SimpleCharacterController simpleCharacterController;
    public float xInput, yInput;
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
                yInput = 1;
                Debug.Log("Forward");
            }

            else if(msg == "A")
            {
                xInput = -1;
                Debug.Log("Left");
            }

            else if(msg == "S")
            {
                yInput = -1;
                Debug.Log("Back");
            }

            else if(msg == "D")
            {
                xInput = 1;
                Debug.Log("Right");
            }

            else{
                xInput = 0;
                yInput = 0;
                Debug.Log("NoInput");
            }
        }
    
    void OnConnectionEvent(bool success)
    {
        Debug.Log(success ? "Device Connected" : "Device discounted");
    }
    
}
