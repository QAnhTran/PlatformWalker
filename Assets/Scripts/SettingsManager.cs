using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    public static SettingsManager Instance;
    public Toggle BGMToggle;
    public Toggle SFXToggle;
    private bool _togglesInitialized = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            // Listen for scene changes
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        _togglesInitialized = false; 
        FindSettingsToggles(); 
        if (BGMToggle != null && SFXToggle != null)
        {
            InitializeToggles(); 
            _togglesInitialized = true;
        }
    }

    public void OnSettingsOpened()
    {
        FindSettingsToggles(); 
        if (!_togglesInitialized && BGMToggle != null && SFXToggle != null)
        {
            InitializeToggles(); 
            _togglesInitialized = true;
        }
    }

    void FindSettingsToggles()
    {
        BGMToggle = GameObject.Find("BGM Toggle")?.GetComponent<Toggle>();
        SFXToggle = GameObject.Find("SFX Toggle")?.GetComponent<Toggle>();

        if (BGMToggle == null) Debug.LogError("BGMToggle not found in the scene!");
        if (SFXToggle == null) Debug.LogError("SFX Toggle not found in the scene!");
    }

    void InitializeToggles()
    {
        bool isBGMEnabled = PlayerPrefs.GetInt("BGMEnabled", 1) == 1;
        bool isSFXEnabled = PlayerPrefs.GetInt("SFXEnabled", 1) == 1;

        BGMToggle.isOn = isBGMEnabled;
        SFXToggle.isOn = isSFXEnabled;

        BGMToggle.onValueChanged.AddListener(OnBGMToggleChanged);
        SFXToggle.onValueChanged.AddListener(OnSFXToggleChanged);
    }

    private void OnBGMToggleChanged(bool isOn)
    {
        PlayerPrefs.SetInt("BGMEnabled", isOn ? 1 : 0);
        AudioManager.Instance.SetBGMEnabled(isOn);
    }

    private void OnSFXToggleChanged(bool isOn)
    {
        PlayerPrefs.SetInt("SFXEnabled", isOn ? 1 : 0);
        AudioManager.Instance.SetSFXEnabled(isOn);
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
