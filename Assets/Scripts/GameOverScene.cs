using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScene : MonoBehaviour
{
    public GameState gameState;
    public void Retry()
    {
        if (gameState.level3Complete || !gameState.level1Complete) SceneManager.LoadScene("GamePlay_1");
        else if (gameState.level2Complete) SceneManager.LoadScene("GamePlay_3");
        else if (gameState.level1Complete) SceneManager.LoadScene("GamePlay_2");
    }
    public void BacktoStory()
    {
        SceneManager.LoadScene("StoryScene_Act1");
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
