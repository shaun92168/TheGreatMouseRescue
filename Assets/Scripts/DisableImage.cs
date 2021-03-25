using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DisableImage : MonoBehaviour
{
    //public UnityEngine.UI.Image image;
    public RawImage image;
    //public bool isImageOn;
    //private bool enable;


    void Start()
    {


        //isImageOn = true;
        image.enabled = true;
    }
    public void OnMouseClick()
    {
        //isImageOn = false;
        image.enabled = false;
    }
}