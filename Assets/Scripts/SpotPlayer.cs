using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SpotPlayer : MonoBehaviour
{

    public Material myMaterial;
    private Color originalColor;
    public Text label;

    public int tempCounter = 0;
    void Awake()
    {
        label.text = "???";
        originalColor = myMaterial.color;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            label.text = "!!!";
            myMaterial.color = Color.red;
            tempCounter += 1;
            if (tempCounter == 5)
            {
               SceneManager.LoadScene("GameOver");
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            label.text = "???";
            myMaterial.color = originalColor;
        }
        myMaterial.color = originalColor;
    }

}
