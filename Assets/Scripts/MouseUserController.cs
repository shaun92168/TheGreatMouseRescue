using UnityEngine;
using System.Collections;

public class MouseUserController : MonoBehaviour
{
    MouseCharacter mouseCharacter;

    void Start()
    {
        mouseCharacter = GetComponent<MouseCharacter>();
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            mouseCharacter.Attack();
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            mouseCharacter.Jump();
            //FindObjectOfType<AudioManager>().Play("Alert");
        }
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            mouseCharacter.Hit();
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            mouseCharacter.Walk();
        }
        // Crawl
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            mouseCharacter.Crawl();
        }
        if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            mouseCharacter.Walk();
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            mouseCharacter.EatStart();
        }
        if (Input.GetKeyUp(KeyCode.E))
        {
            mouseCharacter.EatEnd();
        }



        if (Input.GetKeyDown(KeyCode.N))
        {
            mouseCharacter.Sleep();
        }
        if (Input.GetKeyDown(KeyCode.Y))
        {
            mouseCharacter.WakeUp();
        }

        if (Input.GetKeyDown(KeyCode.J))
        {
            mouseCharacter.Gallop();
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            mouseCharacter.Walk();
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            mouseCharacter.Walk();
        }
        
        //mouseCharacter.forwardSpeed = mouseCharacter.maxWalkSpeed * Input.GetAxis("Vertical");
        mouseCharacter.forwardSpeed = Input.GetAxis("Vertical");
        mouseCharacter.turnSpeed = Input.GetAxis("Horizontal");
    }

}
