using UnityEngine;
using System.Collections;

public class PlayerControllerCustom : MonoBehaviour
{

    public GameObject bumper;
    public GameObject lifter;

    private Transform myTransform;
    private Transform cameraPivot;

	void Start ()
    {
	    myTransform = this.transform;
        cameraPivot = GameObject.FindGameObjectWithTag("MainCamera").transform.parent.transform;
	}

    void FixedUpdate ()
    {
        myTransform.position = myTransform.position + Vector3.forward;


        kinematics();
    }

    private Vector3 velocity = Vector3.zero;
    private Vector3 lastPos = Vector3.zero;
    private Vector3 acceleration = Vector3.zero;
    private Vector3 lastVelocity = Vector3.zero;
    void kinematics()
    {
        velocity = (myTransform.position - lastPos) / Time.deltaTime;

        acceleration = (velocity - lastVelocity) / Time.deltaTime;

        lastPos = myTransform.position;
        lastVelocity = velocity;


        {
            Debug.Log("V = " + velocity);
            Debug.Log("A = " + acceleration);
        }
    }
}

//Velocity = (Delta Distance) / (deltaTime)