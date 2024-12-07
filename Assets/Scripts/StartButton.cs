using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class StartButton : MonoBehaviour
{
public string targetSceneName = "Level 1"; 

    public void LoadTargetScene()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(targetSceneName); 
    }
}
