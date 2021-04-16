using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class EventClimb : MonoBehaviour
{
    public Text label;
    public bool isClimb;
    public MouseCharacter mouseCharacter;

    private void Awake()
    {
        label.text = "";
        isClimb = mouseCharacter.isClimbing;
    }

    void Start()
    {
        
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //mouseCharacter.isClimbing = true;
            label.text = "Press [C] to Climb !!!";
            label.fontSize = 70;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            label.text = "";
            //mouseCharacter.isClimbing = false;
        }
    }


}
