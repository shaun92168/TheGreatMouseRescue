using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapTrigger : MonoBehaviour
{
    public GameState gameState;
    private bool isTrigger;

    private void Start()
    {
        isTrigger = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        // check if the player collide with the trap
        if (other.gameObject.name == "Player")
        {
            isTrigger = true;
            gameState.isTrapTrigger = true;
            Debug.Log(transform.gameObject.name + " triggered");
            FindObjectOfType<AudioManager>().Play("CloserToTrap");
        }
        if (isTrigger)
        {
            //play trigger animation
            FindObjectOfType<AudioManager>().Play("TrapActive");
        }
    }
}
