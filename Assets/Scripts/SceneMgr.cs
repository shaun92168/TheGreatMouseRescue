using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMgr : MonoBehaviour
{
    public string NextSceneName;
    public string PrevSceneName;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            ChangeSceneTo(NextSceneName);
        }
    }

    public void NextScene()
    {
        ChangeSceneTo(NextSceneName);
    }

    public void PrevScene()
    {
        ChangeSceneTo(PrevSceneName);
    }

    void ChangeSceneTo(string NewScene)
    {
        SceneManager.LoadScene(NewScene);

        if (NewScene.Length != 0 || SceneManager.GetActiveScene().name != NewScene)
        {
            Debug.LogFormat("Scene name does not exist! '{0}'", NewScene);
        }
    }
}
