﻿using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
    /*  MainCharacter class; 
     *      Movement
     *      Rotation of Player, and CameraRotation
     *      Rotaiton to face velocity
     */


    public float speed;
    public float jump;
    public float gravity = 9.81f;
    public CharacterController controller;
    public Transform graphics;
    public Transform cameraPivot;

    private Transform myTransform;

    void Start()
    {
        myTransform = this.transform;
    }

    void Update()
    {
        movement();
        rotation();
        faceVelocity();
        tiltToAcceleration();
    }

    private Vector3 moveDirection = Vector3.zero;
    private void movement()
    {
        /*      Movement
         *          Clamp magnitude to 1/sqr(2) to keep diagonal movement consistant
         *          transform direction changes from local space to world space making foward relative to camera position
         *          graphics seperate to allow for easier rotaion to face velocity
         */

        if (controller.isGrounded)
        {
            moveDirection = new Vector3(
                Input.GetAxisRaw("Horizontal"),
                0,
                Input.GetAxisRaw("Vertical"));
            moveDirection = Vector3.ClampMagnitude(moveDirection, 0.7071f);
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection *= speed;

            if (Input.GetButton("Jump"))
                moveDirection.y = jump;
        }

        moveDirection.y -= gravity * Time.deltaTime;

        controller.Move(moveDirection * Time.deltaTime);
        graphics.transform.position = myTransform.position;
        cameraPivot = myTransform;
    }

    public float lookSpeed;
    private void rotation()
    {
        /*      Rotation
         *          Rotates transform which only effects the Camera Pivot to simplfy rotating to face the velocity
         */

        float xDistance = Input.GetAxis("Mouse X") * lookSpeed;
        float yDistance = Input.GetAxis("Mouse Y") * lookSpeed;
        Quaternion target = Quaternion.Euler(
            myTransform.eulerAngles.x,
            myTransform.eulerAngles.y + xDistance,
            myTransform.eulerAngles.z);

        myTransform.rotation = target;
    }

    public float turnSpeed;
    private Vector3 direction = Vector3.zero;
    private void faceVelocity()
    {
        /*      Makes Player graphics react to velocity and camera Rotaion not change in position or Input
         */

        direction = controller.velocity;
        direction.y = 0f;

        if (direction != Vector3.zero)
        {
            Quaternion desiredRotation = Quaternion.LookRotation(direction);
            graphics.rotation = Quaternion.Slerp(graphics.rotation, desiredRotation, turnSpeed * Time.deltaTime);
        }
    }

    private Vector3 lastVelocity = Vector3.zero;
    private Vector3 acceleration = Vector3.zero;
    private void tiltToAcceleration()
    {
        acceleration = (controller.velocity - lastVelocity) / Time.deltaTime;

        Vector3 perp = Vector3.Cross(acceleration, Vector3.up);

        //Debug.Log(perp);

        lastVelocity = controller.velocity;
    }
}
