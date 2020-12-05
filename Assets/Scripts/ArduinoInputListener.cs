using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArduinoInputListener : MonoBehaviour
{
    SimpleCharacterController simpleCharacterController;
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

        }
    
    void OnConnectionEvent(bool success)
    {
        Debug.Log(success ? "Device Connected" : "Device discounted");
    }
    
}
