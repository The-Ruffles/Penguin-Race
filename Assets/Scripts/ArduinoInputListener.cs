using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArduinoInputListener : MonoBehaviour
{
    bool btnInput = false;
    void OnMessageArrived(string msg)  
        {
            Debug.Log("Recieved Message: " + msg);
            
            if (msg == "3")
            {
                Debug.Log("Button Input");
            }
        }
    
    void OnConnectionEvent(bool success)
    {
        Debug.Log(success ? "Device Connected" : "Device discounted");
    }
    
}
