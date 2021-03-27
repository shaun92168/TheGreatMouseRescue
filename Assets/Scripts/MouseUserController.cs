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
     /*   mouseCharacter.Walk();
        if (Input.GetKeyDown(KeyCode.Space))
        {
            mouseCharacter.Run();
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            mouseCharacter.Walk();
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            mouseCharacter.Sneak();
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            mouseCharacter.Walk();
        }

        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            mouseCharacter.Crawl();
        }
        if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            mouseCharacter.Walk();
        }*/
        mouseCharacter.forwardSpeed = Input.GetAxis("Vertical");
        //mouseCharacter.forwardSpeed = mouseCharacter.maxWalkSpeed * Input.GetAxis("Vertical");
        mouseCharacter.turnSpeed = Input.GetAxis("Horizontal");
       
    }

}
