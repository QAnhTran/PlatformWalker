using UnityEngine;

public class Cameracontroller : MonoBehaviour
{
    [Header("Player Target")]
    [SerializeField] private Transform Player; // Current player to follow

    [Header("Camera Offsets")]
    public Vector3 mainCameraOffset = new Vector3(0, 0, -10); // Offset for the main camera
    public Vector3 minimapCameraOffset = new Vector3(0, 10, 0); // Offset for the minimap camera

    [Header("Camera Type")]
    public bool isMainCamera; // True if this is the main camera, false if minimap

    void Start()
    {
        // Find the initial player target
        UpdatePlayerTarget();
    }

    void LateUpdate()
    {
        // Follow the assigned player target
        if (Player != null)
        {
            Vector3 offset = isMainCamera ? mainCameraOffset : minimapCameraOffset;
            transform.position = new Vector3(Player.position.x, Player.position.y, transform.position.z);
        }
        else
        {
            Debug.LogWarning("No player assigned to follow!");
        }
    }

    public void UpdatePlayerTarget(Transform newPlayer = null)
    {
        // Update the player target dynamically
        if (newPlayer != null)
        {
            Player = newPlayer;
        }
        else
        {
            GameObject activePlayer = GameObject.FindGameObjectWithTag("Player");
            if (activePlayer != null)
            {
                Player = activePlayer.transform;
            }
        }

        Debug.Log($"{(isMainCamera ? "Main Camera" : "Minimap Camera")} target updated to: {Player?.name ?? "None"}");
    }
}
