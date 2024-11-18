using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonStart : MonoBehaviour
{
    public string targetSceneName = "lv1"; 

    public void LoadTargetScene()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(targetSceneName); 
    }
}
