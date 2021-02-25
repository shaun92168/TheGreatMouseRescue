using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AlertBarController : MonoBehaviour
{
    public GameState gameState;
    public GameObject alertBar;

    private float sliderValue;
    // Update is called once per frame
    void Update()
    {
        
        sliderValue = gameState.alertLevel / gameState.maxAlertLevel;
        alertBar.GetComponent<Slider>().value = sliderValue;
    }
}
