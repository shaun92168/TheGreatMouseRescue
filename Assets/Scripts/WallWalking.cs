﻿using System;
using UnityEngine;
/*using UnityStandardAssets.Characters.FirstPerson;*/

public class WallWalking : MonoBehaviour
{
    [Header("Properties")]

    public bool isWallRunning = false; //If the player is currently WallRunning.
    public float wallRunDuration = 4; //The second before stop wallrun.
    public float upForce = 15; // The vertical force applied when the player enter in wallRun.
    public float constantUpForce = 10;// The vertical force to not let the player fall.
    public float WallForce = 4000;
    public float forwardForce = 12;
    public bool isWallLeft;
    public bool isWallRight;
    public Transform target;
    public float t;
    public float forceUpward;
    public float force;

    [Header("Camera")]
  
    public float rayDistance;
    public float camAngle = 20;
    public float curCamAngle;
    public Transform cam;

    private Vector3 wallDir; //The direction to the wall
    [Header("Controller")]
    //public MouseUserController controller; // Your Controller

    public Rigidbody rb; // The rigidbody

    public static WallWalking instance;

    private bool isCancellingWallrunning;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        instance = this;
        wallDir = Vector3.up;
    }

    private void Update()
    {

        WallRunning();
       /* if(cam != null)
        {
           
            if (Physics.Raycast(transform.position, transform.right, rayDistance) && isWallRunning)
            {
                curCamAngle = camAngle;
                isWallRight = true;
                isWallLeft = false;
          
            }
            else if (Physics.Raycast(transform.position, -transform.right, rayDistance) && isWallRunning)
            {
                curCamAngle = -camAngle;
                isWallRight = false;
                isWallLeft = true;
            }
            else
            {
                curCamAngle = 0;
                isWallRight = false;
                isWallLeft = false;
            }
            

            
        }*/
    }


    private void EnterInWall()
    {

        if (!isWallRunning)
        {
            rb.AddForce(Vector3.up * upForce, ForceMode.Impulse);
            //rb.AddForce(controller.transform.forward * forwardForce, ForceMode.Impulse);
            Debug.Log("Start Wall Run");
        }

        isWallRunning = true;
    }

    private void WallRunning()
    {
        if (isWallRunning)
        {
            rb.AddForce(-wallDir * WallForce * Time.deltaTime); // Push the player on the wall.
            //rb.AddForce(Vector3.up * constantUpForce * Time.deltaTime); // Apply a constant force to not let the player fall.
            Vector3 f = target.position - transform.position;
            forceUpward = target.position.y + 10f;
            Vector3 upForceVector = new Vector3(0f, forceUpward, 0f);

            f = f.normalized;
            f = f * force;
            rb.AddForce(f);
        }
    }

    //Check the angle of the surface.
    private bool CheckSurfaceAngle(Vector3 v, float angle)
    {
        return Math.Abs(angle - Vector3.Angle(Vector3.up, v)) < 0.1f;
    }


    private void ExitWallRunning()
    {
        isWallRunning = false;
    }


    private void OnCollisionStay(Collision other)
    {
        Vector3 surface = other.contacts[0].normal;
        if (CheckSurfaceAngle(surface, 90))
        {
            EnterInWall();
            wallDir = surface;

            isCancellingWallrunning = false;
            CancelInvoke("ExitWallRunning");
        }
 
        if (!isCancellingWallrunning)
        {
            isCancellingWallrunning = true;
            Invoke("ExitWallRunning", wallRunDuration * Time.deltaTime);
        }

    }

    private void OnCollisionExit(Collision collision)
    {
        if (isWallRunning)
        {
            ExitWallRunning();
   
        }
        
    }

}
