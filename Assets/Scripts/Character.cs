using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    Rigidbody rigidBody;
    float horizontalMovement;
    float verticalMovement;

    public float speed = 10.0f;

    private void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        horizontalMovement = Input.GetAxisRaw("Horizontal");
        verticalMovement = Input.GetAxisRaw("Vertical");
        rigidBody.velocity = new Vector3(horizontalMovement * speed, 0, verticalMovement * speed);
    }

}
