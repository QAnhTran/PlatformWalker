using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public Transform[] spawnPoints;
    public GameObject playerPrefab;
    public bool isMultiplayer;

    private bool playerInstantiated = false;


    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        Debug.Log("GameManager Start called");

        if (playerPrefab == null)
        {
            Debug.LogError("Player Prefab is not assigned.");
            return;
        }

        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
        Debug.Log($"Selected spawn point: {spawnPoint.position}");

        Debug.Log("Instantiating player for single-player");
        GameObject player = Instantiate(playerPrefab, spawnPoint.position, spawnPoint.rotation);

        SetCameraFollow(player);

        playerInstantiated = true; // Prevent additional instantiations
    }

    private void SetCameraFollow(GameObject player)
    {
        Cameracontroller cameraFollow = Camera.main.GetComponent<Cameracontroller>();
        if (cameraFollow != null)
        {
            Debug.Log("Camera follow set to player");
            cameraFollow.Player = player.transform;
        }
        else
        {
            Debug.LogError("CameraFollowPlayer script not found on the Main Camera.");
        }
    }

}
