using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollText : MonoBehaviour
{
    public float scrollSpead;
    
    void Update()
    {
        Vector3 pos = transform.position;

        Vector3 localVectorUp = transform.TransformDirection(0,1,0);

        pos += localVectorUp * scrollSpead * Time.deltaTime;
        transform.position = pos;   
    }
}