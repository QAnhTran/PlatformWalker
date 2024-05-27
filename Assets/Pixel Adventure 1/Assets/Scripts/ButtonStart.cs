using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonStart : MonoBehaviour
{
    public string targetSceneName = "lv1"; 

    public void LoadTargetScene()
    {
        SceneManager.LoadScene(targetSceneName); 
    }
}
