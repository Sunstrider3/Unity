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

    public void setRotation(Quaternion rotaiton)
    {
        myTransform.rotation = player.rotation;


    }

    void LateUpdate()
    {
        myTransform.position = player.position;
    }
}
