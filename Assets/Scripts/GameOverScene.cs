using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScene : MonoBehaviour
{
    public void Retry()
    {
        SceneManager.LoadScene("GamePlay_1");
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
