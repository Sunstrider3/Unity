using UnityEngine;
using System.Collections;

public class SplitRotationFromParent : MonoBehaviour {

    Quaternion keepRotation;
    void Awake()
    {
        keepRotation = transform.rotation;
    }
    void Update()
    {
        transform.rotation = keepRotation;
    }
}
