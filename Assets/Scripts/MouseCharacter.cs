using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class MouseCharacter : MonoBehaviour
{
    Animator mouseAnimator;
    public bool jumpStart = false;
    public float groundCheckDistance = 0.1f;
    public float groundCheckOffset = 0.01f;
    public bool isGrounded = true;
    Rigidbody mouseRigid;
    //public float speed = 4f;
    public float yRot = 0f;
    public bool isClimbing = false;
    // Speed for animation
    //public float forwardSpeed;
    //public float turningSpeed;
    //public float walkMode = 1f;
    //public float jumpStartTime = 0f;
    //public float maxWalkSpeed = 1f;

    public GameState gameState;
    public GameStateManager gameStateManager;

    public Rigidbody rigidbody;
    private float moveSpeed = 6; // move speed
    private float turnSpeed = 90; // turning speed (degrees/second)
    private float lerpSpeed = 10; // smoothing speed
    private float gravity = 10; // gravity acceleration
    private float deltaGround = 0.2f; // character is grounded up to this distance
    private float jumpSpeed = 10; // vertical jump initial speed
    private float jumpRange = 10; // range to detect target wall
    private Vector3 surfaceNormal; // current surface normal
    private Vector3 myNormal; // character normal
    private float distGround; // distance from character position to ground
    private bool jumping = false; // flag &quot;I'm jumping to wall&quot;
    private float vertSpeed = 0; // vertical jump current speed
    public bool onWall = false; 

    private Transform myTransform;
    public BoxCollider boxCollider; // drag BoxCollider ref in editor

    private void Start()
    {
        mouseAnimator = GetComponent<Animator>();
        mouseRigid = GetComponent<Rigidbody>();
        if (SceneManager.GetActiveScene().name == "GamePlay_2")
        {
            yRot = 90.0f;
        }
        if (SceneManager.GetActiveScene().name == "GamePlay_3")
        {
            yRot = 180.0f;
        }

        myNormal = transform.up; // normal starts as character up direction
        myTransform = transform;
        rigidbody.freezeRotation = true; // disable physics rotation
                                         // distance from transform.position to ground
        distGround = boxCollider.extents.y - boxCollider.center.y;
        isClimbing = false;
    }

    private void FixedUpdate()
    {
        // apply constant weight force according to character normal:
        rigidbody.AddForce(-gravity * rigidbody.mass * myNormal);
    }


    private void JumpToWall(Vector3 point, Vector3 normal)
    {
        // jump to wall
        jumping = true; // signal it's jumping to wall
        rigidbody.isKinematic = true; // disable physics while jumping
        Vector3 orgPos = myTransform.position;
        Quaternion orgRot = myTransform.rotation;
        Vector3 dstPos = point + normal * (distGround + 1.5f); // will jump to 0.5 above wall
        Vector3 myForward = Vector3.Cross(myTransform.right, normal);
        Quaternion dstRot = Quaternion.LookRotation(myForward, normal);

        StartCoroutine(jumpTime(orgPos, orgRot, dstPos, dstRot, normal));
        //jumptime
    }

    private IEnumerator jumpTime(Vector3 orgPos, Quaternion orgRot, Vector3 dstPos, Quaternion dstRot, Vector3 normal)
    {
        for (float t = 0.0f; t < 1.0f;)
        {
            t += Time.deltaTime;
            myTransform.position = Vector3.Lerp(orgPos, dstPos, t);
            myTransform.rotation = Quaternion.Slerp(orgRot, dstRot, t);
            yield return null; // return here next frame
        }
        myNormal = normal; // update myNormal
        rigidbody.isKinematic = false; // enable physics
        jumping = false; // jumping to wall finished

    }

    void Update()
    {

        if (gameState.playerState == 0)
        {
            FindObjectOfType<AudioManager>().Play("MouseRunning");
            FindObjectOfType<AudioManager>().Stop("Running2");
            FindObjectOfType<AudioManager>().Stop("Sneak3");
            FindObjectOfType<AudioManager>().Stop("Sneak3");

        }
        if (gameState.playerState == 1)
        {
            FindObjectOfType<AudioManager>().Play("Running2");
        }
        if (gameState.playerState == 2)
        {
            FindObjectOfType<AudioManager>().Play("Sneak3");
        }
        if (gameState.playerState == 3)
        {
            FindObjectOfType<AudioManager>().Play("Sneak2");
        }

        // jump code - jump to wall or simple jump
        if (jumping) return; // abort Update while jumping to a wall

        Ray ray;
        RaycastHit hit;

        if (Input.GetKeyDown(KeyCode.J) && isClimbing)
        { // jump pressed:
            ray = new Ray(myTransform.position, myTransform.forward);
            if (Physics.Raycast(ray, out hit, jumpRange))
            { // wall ahead?
                onWall = true;
                gameStateManager.mainCam.SetActive(false);
                gameStateManager.mainCam.GetComponent<AudioListener>().enabled = false;
                gameStateManager.climbCam.SetActive(true);
                gameStateManager.climbCam.GetComponent<AudioListener>().enabled = true;
                JumpToWall(hit.point, hit.normal); // yes: jump to the wall
                
                FindObjectOfType<AudioManager>().Play("LandFromJump");
            }
            else if (isGrounded)
            { // no: if grounded, jump up
                rigidbody.velocity += jumpSpeed * myNormal;
            }
        }

        if (gameState.activeLevel == 2 | gameState.activeLevel == 3) {
            if (!onWall)
            {
                gameStateManager.mainCam.SetActive(true);
                gameStateManager.mainCam.GetComponent<AudioListener>().enabled = true;
                gameStateManager.climbCam.SetActive(false);
                gameStateManager.climbCam.GetComponent<AudioListener>().enabled = false;
            }
        }

        // movement code - turn left/right with Horizontal axis:
        myTransform.Rotate(0, Input.GetAxis("Horizontal") * turnSpeed * Time.deltaTime, 0);

        // update surface normal and isGrounded:
        ray = new Ray(myTransform.position, -myNormal); // cast ray downwards
        if (Physics.Raycast(ray, out hit))
        { // use it to update myNormal and isGrounded
            isGrounded = hit.distance <= distGround + deltaGround;
            surfaceNormal = hit.normal;
        }
        else
        {
            isGrounded = false;
            // assume usual ground normal to avoid "falling forever"
            surfaceNormal = Vector3.up;
        }
        myNormal = Vector3.Lerp(myNormal, surfaceNormal, lerpSpeed * Time.deltaTime);
        // find forward direction with new myNormal:
        Vector3 myForward = Vector3.Cross(myTransform.right, myNormal);
        // align character to the new myNormal while keeping the forward direction:
        Quaternion targetRot = Quaternion.LookRotation(myForward, myNormal);
        myTransform.rotation = Quaternion.Lerp(myTransform.rotation, targetRot, lerpSpeed * Time.deltaTime);
        // move the character forth/back with Vertical axis:
        myTransform.Translate(0, 0, Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime);

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

        yRot += horizontal * 0.8f;
       
        if (vertical > -0.2f && vertical < 0.2f)
        {
            mouseAnimator.SetFloat("Forward", 0);
        }

        //Debug.Log(vertical);
        //Vector3 movement = new Vector3(horizontal * speed, 0, vertical * speed);
        Vector3 movement = this.gameObject.transform.forward;

        // This line will break the climb
        //this.gameObject.transform.eulerAngles = new Vector3(this.gameObject.transform.forward.x, yRot, this.gameObject.transform.forward.z);
        //var moveVec = new Vector3(0.0f, 0.0f, vertical);
         //this.gameObject.transform.Translate(moveVec * moveSpeed * Time.deltaTime);

        //if (vertical != 0)
        //{
        //    movement.Normalize();
        //    movement *= speed * Time.deltaTime;
        //    transform.Translate(movement, Space.World);
        //}

        // Animating
        float Forward = Vector3.Dot(movement.normalized, transform.forward);
        float Turn = Vector3.Dot(movement.normalized, transform.right);

        mouseAnimator.SetFloat("Forward", Forward, 0.1f, Time.deltaTime);
        mouseAnimator.SetFloat("Turn", Turn, 0.1f, Time.deltaTime);

    }


    public void Run()
    {
        moveSpeed = 6.5f;
        gameState.playerState = 1;
    }

    public void Sneak()
    {
        moveSpeed = 3f;
        gameState.playerState = 2;
    }
    public void Crawl()
    {
        moveSpeed = 2f;
        gameState.playerState = 3;

    }

    public void Walk()
    {
        moveSpeed = 5f;
        gameState.playerState = 0;

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Trigger_1")
        {
            gameState.level1Complete = true;
            Debug.Log("Level 1 Complete");
            SceneManager.LoadScene("CutScene_2");
        }

        if (collision.gameObject.name == "Cheese_02")
        {
            gameState.level2Complete = true;
            Debug.Log("Level 2 Complete");
            SceneManager.LoadScene("CutScene_3");
        }

        if (collision.gameObject.name == "cage")
        {
            gameState.level3Complete = true;
            Debug.Log("Level 3 Complete");
            SceneManager.LoadScene("StoryScene_Act3");
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "JumpEventTran2")
        {
            gameStateManager.mainCam.SetActive(true);
            gameStateManager.mainCam.GetComponent<AudioListener>().enabled = true;
            gameStateManager.climbCam.SetActive(false);
            gameStateManager.climbCam.GetComponent<AudioListener>().enabled = false;
        }
    }

    private void OnCollisionStay(Collision other)
    {
        if (other.gameObject.CompareTag("Climbable"))
        {
            isClimbing = true;
            Debug.Log("Climbable!");
        }
    }
    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.CompareTag("Climbable"))
        {
            isClimbing = false;
        }
    }
}
