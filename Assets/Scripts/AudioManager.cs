using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    public AudioSource BGMAudioSource;
    public AudioSource SFXAudioSource;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            // Subscribe to scene load event
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        Initialize();
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        FindAudioSources(); // Dynamically find new audio sources in the scene
    }

    private void FindAudioSources()
    {
        // Dynamically find the audio sources in the current scene
        BGMAudioSource = GameObject.Find("BGM")?.GetComponent<AudioSource>();
        SFXAudioSource = GameObject.Find("Player")?.GetComponent<AudioSource>();

        if (BGMAudioSource == null) Debug.LogError("BGM AudioSource not found in the scene!");
        if (SFXAudioSource == null) Debug.LogError("SFX AudioSource not found in the scene!");

        // Apply the saved settings to the newly found audio sources
        SetBGMEnabled(PlayerPrefs.GetInt("BGMEnabled", 1) == 1);
        SetSFXEnabled(PlayerPrefs.GetInt("SFXEnabled", 1) == 1);
    }

    public void Initialize()
    {
        // Apply saved preferences to the audio sources
        SetBGMEnabled(PlayerPrefs.GetInt("BGMEnabled", 1) == 1);
        SetSFXEnabled(PlayerPrefs.GetInt("SFXEnabled", 1) == 1);
    }

    public void SetBGMEnabled(bool isEnabled)
    {
        if (BGMAudioSource != null)
        {
            BGMAudioSource.mute = !isEnabled;
            PlayerPrefs.SetInt("BGMEnabled", isEnabled ? 1 : 0);
        }
    }

    public void SetSFXEnabled(bool isEnabled)
    {
        if (SFXAudioSource != null)
        {
            SFXAudioSource.mute = !isEnabled;
            PlayerPrefs.SetInt("SFXEnabled", isEnabled ? 1 : 0);
        }
    }

    public void PlaySFX(AudioClip clip)
    {
        if (SFXAudioSource != null && !SFXAudioSource.mute)
        {
            SFXAudioSource.PlayOneShot(clip);
        }
    }

    private void OnDestroy()
    {
        // Unsubscribe from the scene load event
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
