using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class EventClimb : MonoBehaviour
{
    public Text label;
    public bool isClimb;
    public MouseCharacter mouseCharacter;
    public GameObject goImageForClimb;
    private Image promptImage;

    private void Awake()
    {
        label.text = "";
        isClimb = mouseCharacter.isClimbing;
        goImageForClimb.active = false;
        goImageForClimb.GetComponent<Image>().color = new Color(1f, 1f, 1f, .70f);
    }

    void Start()
    {
        
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //mouseCharacter.isClimbing = true;
            //label.text = "Press [C] to Climb !!!";
            goImageForClimb.active = true;
            label.fontSize = 70;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            label.text = "";
            //mouseCharacter.isClimbing = false;
            goImageForClimb.active = false;
        }
    }


}
