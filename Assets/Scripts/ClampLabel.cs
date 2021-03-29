using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClampLabel : MonoBehaviour
{
    public Text label;
    public GameStateManager gameStateManager;
  
    // Update is called once per frame
    void Update()
    {
        Vector3 labelPos = Camera.allCameras[0].WorldToScreenPoint(this.transform.position);
        label.transform.position = labelPos;
    }
}
