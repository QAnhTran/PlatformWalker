using UnityEngine;

public class SettingsManager : MonoBehaviour
{
    public GameObject settingsPanel;

    private void Start()
    {
        settingsPanel.SetActive(false);
    }

    public void ShowPopup()
    {
        if (settingsPanel != null)
        {
            settingsPanel.SetActive(true);
            Time.timeScale = 0f;
        }
    }

    public void HidePopup()
    {
        if (settingsPanel != null)
        {
            settingsPanel.SetActive(false);
            Time.timeScale = 1f;
        }
    }
}
