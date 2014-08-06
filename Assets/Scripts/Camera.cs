using UnityEngine;
using System.Collections;

public class Camera : MonoBehaviour
{
    public Transform player;

    private Transform myTransform;
    void Start ()
    {
        myTransform = this.transform;
    }

    void Update()
    {
    }

    void LateUpdate()
    {
        myTransform.rotation = player.rotation;
        myTransform.position = player.position;
    }
}
