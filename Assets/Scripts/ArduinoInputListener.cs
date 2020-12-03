using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArduinoInputListener : MonoBehaviour
{
    public bool btnInput = false;

    void OnMessageArrived(string msg)  
        {
            Debug.Log("Recieved Message: " + msg);
            //Debug.Log("jumping: " + btnInput);
            if (msg == "3")
            {
                Debug.Log("Unity knows 3 is pressed");
                btnInput = true;
            }

        }
    
    void OnConnectionEvent(bool success)
    {
        Debug.Log(success ? "Device Connected" : "Device discounted");
    }
    
}
