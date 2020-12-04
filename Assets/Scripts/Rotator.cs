using UnityEngine;
using System.Collections;

public class Rotator : MonoBehaviour {

    public int rotateX, rotateY, rotateZ;
    void Update () 
    {
        transform.Rotate (new Vector3 (rotateX, rotateY, rotateZ) * Time.deltaTime);
    }
}