using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DisableImage : MonoBehaviour
{
    public RawImage image;


    void Start()
    {
        // Set an opaue color so we can see throug it to the player
        image.color = new Color(1f, 1f, 1f, 0.7f);
        image.enabled = true;
    }


    public void OnMouseClick()
    {
        image.enabled = false;
    }
}