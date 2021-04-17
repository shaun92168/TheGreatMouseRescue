using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class EventClimb : MonoBehaviour
{
    public Text label;
    public bool isClimb;
    public MouseCharacter mouseCharacter;
    public GameObject goClimbImage;

    private void Awake()
    {
        label.text = "";
        isClimb = mouseCharacter.isClimbing;
        goClimbImage.SetActive(false);
        goClimbImage.GetComponent<Image>().color = new Color(1f, 1f, 1f, 0.7f);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            label.text = "";
            label.fontSize = 70;
            goClimbImage.SetActive(true);
        }
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            label.text = "";
            goClimbImage.SetActive(false);
        }
    }
}
