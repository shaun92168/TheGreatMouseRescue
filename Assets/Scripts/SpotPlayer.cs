using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SpotPlayer : MonoBehaviour
{
    [SerializeField]
    private Material myMaterial;

    private Color originalColor;
    public Text label;
    public GameState gameState;

    public int tempCounter = 0;

    private bool isTrigger;
    void Awake()
    {
        label.text = "";
        originalColor = myMaterial.color;
        isTrigger = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            label.text = "!!!";
            myMaterial.color = Color.red;
            tempCounter += 1;
            isTrigger = true;
            gameState.isTrapTrigger = true;
            gameState.alertLevel = gameState.maxAlertLevel;
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
            label.text = "";
            myMaterial.color = originalColor;
        }
        myMaterial.color = originalColor;
    }

}
