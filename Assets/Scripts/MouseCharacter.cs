using UnityEngine;
using System.Collections;

public class MouseCharacter : MonoBehaviour
{
    Animator mouseAnimator;
    public bool jumpStart = false;
    public float groundCheckDistance = 0.1f;
    public float groundCheckOffset = 0.01f;
    public bool isGrounded = true;
    public float jumpSpeed = 2f;
    Rigidbody mouseRigid;
    public float speed = 4f;

    // Speed for animation
    public float forwardSpeed;
    public float turnSpeed;
    public float walkMode = 1f;
    //public float jumpStartTime = 0f;
    //public float maxWalkSpeed = 1f;

    public GameState gameState;

    
    void Start()
    {
        mouseAnimator = GetComponent<Animator>();
        mouseRigid = GetComponent<Rigidbody>();
    }

    void Update()
    {
        //Walk();
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Run();
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            Walk();
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            Sneak();
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            Walk();
        }

        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            Crawl();
        }
        if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            Walk();
        }

        // Movement
        float vertical = Input.GetAxis("Vertical");
        float horizontal = Input.GetAxis("Horizontal");
        Vector3 movement = new Vector3(horizontal * speed, 0, vertical * speed);
        if (movement.magnitude > 0)
        {
            movement.Normalize();
            movement *= speed * Time.deltaTime;
            transform.Translate(movement, Space.World);
        }

        // Animating
        float Forward = Vector3.Dot(movement.normalized, transform.forward);
        float Turn = Vector3.Dot(movement.normalized, transform.right);

        mouseAnimator.SetFloat("Forward", Forward, 0.1f, Time.deltaTime);
        mouseAnimator.SetFloat("Turn", Turn, 0.1f, Time.deltaTime);
    }


    public void Run()
    {
        speed = 6.5f;
        gameState.playerState = 1;
    }

    public void Sneak()
    {
        speed = 3f;
        gameState.playerState = 2;
    }
    public void Crawl()
    {
        speed = 2f;
        gameState.playerState = 3;
    }

    public void Walk()
    {
        speed = 5f;
        gameState.playerState = 0;
    }
}
