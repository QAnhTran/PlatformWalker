using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class GameManager : MonoBehaviourPunCallbacks
{
    public static GameManager Instance { get; private set; }
    public Transform[] spawnPoints; // Array of spawn points for players
    public GameObject playerPrefab; // Player prefab to instantiate
    public bool isMultiplayer; // Determine if the game is in multiplayer mode

    private bool playerInstantiated = false; // Flag to prevent multiple player instances


    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Preserve across scenes
        }
        else
        {
            Destroy(gameObject); // Ensure only one instance exists
        }
    }
    void Start()
    {
        Debug.Log("GameManager Start called");

        if (playerInstantiated) 
        {
            Debug.Log("Player already instantiated, skipping spawn");
            return; // Ensure only one player is spawned
        }

        if (playerPrefab == null)
        {
            Debug.LogError("Player Prefab is not assigned.");
            return;
        }

        if (spawnPoints.Length == 0)
        {
            Debug.LogError("No spawn points assigned!");
            return;
        }

        // Choose a random spawn point for the player
        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
        Debug.Log($"Selected spawn point: {spawnPoint.position}");

        if (Instance.isMultiplayer)
        {
            if (PhotonNetwork.IsConnectedAndReady)
            {
                // Instantiate the player via Photon for multiplayer
                Debug.Log("Instantiating player for multiplayer");
                GameObject player = PhotonNetwork.Instantiate(playerPrefab.name, spawnPoint.position, spawnPoint.rotation);

                // Set the camera to follow the player
                SetCameraFollow(player);
            }
            else
            {
                Debug.LogError("Photon Network is not connected and ready.");
            }
        }
        else
        {
            // Single-player mode instantiation
            Debug.Log("Instantiating player for single-player");
            GameObject player = Instantiate(playerPrefab, spawnPoint.position, spawnPoint.rotation);

            // Set the camera to follow the player
            SetCameraFollow(player);
        }

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
