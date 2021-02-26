using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    // public Material[] materials;
    public Renderer rend;
    public Transform trans;
    private bool isPressed;
    // 0 = Stand, 1 = Run , 2 = Sneak, 3 = Crawl
    public int playerState = 0;
    //public float sneakHeight = 0.55f;
    //public float crawlHeight = 0.2f;
    public GameState gameState;

    Rigidbody rigidBody;
    float hori;
    float vert;

    public float speed;
    public float runSpeed;
    public float sneakSpeed;
    public float crawlSpeed;

    void Start()
    {
        isPressed = false;
        rigidBody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (playerState == 0)
        {
            transform.localRotation = Quaternion.Euler(0, 0, 0);
            rend.GetComponent<Renderer>().material.color = Color.white;
        }

        if (playerState == 1)
        {
            transform.localRotation = Quaternion.Euler(80, 0, 0);
            speed = runSpeed;
            rend.GetComponent<Renderer>().material.color = Color.red;
        }
        if (playerState == 2)
        {
            transform.localRotation = Quaternion.Euler(10, 0, 0);
            speed = sneakSpeed;
            rend.GetComponent<Renderer>().material.color = Color.yellow;
        }
        if (playerState == 3)
        {

            transform.localRotation = Quaternion.Euler(30, 0, 0);
            speed = crawlSpeed;
            rend.GetComponent<Renderer>().material.color = Color.green;
        }
    }
    // Update is called once per frame
    void Update()
    {
        hori = Input.GetAxisRaw("Horizontal");
        vert = Input.GetAxisRaw("Vertical");
        rigidBody.velocity = new Vector3(hori * speed, 0, vert * speed);

        if (!isPressed)
        {
            playerState = 0;
        }

        // Run
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isPressed = !isPressed;
            if (isPressed)
            {
                playerState = 1;
            }
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            isPressed = false;
        }
        // Sneak
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            isPressed = !isPressed;
            if (isPressed)
            {
                playerState = 2;
            }
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            isPressed = false;
        }
        // Crawl
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            isPressed = !isPressed;
            if (isPressed)
            {
                playerState = 3;
            }
        }
        if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            isPressed = false;
        }

        gameState.playerState = playerState;
    }
}
