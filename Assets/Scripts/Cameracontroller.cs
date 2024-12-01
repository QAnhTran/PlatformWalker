using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cameracontroller : MonoBehaviour
{
    [SerializeField] public Transform Player;
    public Vector3 offset = new Vector3(0, 10, 0); // Offset for the minimap position

    void Start()
    {
        UpdatePlayerTarget(); // Find the active player at the start
    }

    void Update()
    {
        UpdatePlayerTarget(); // Dynamically update the player target

        if (Player != null)
        {
            // Follow the player's position with an offset
            transform.position = new Vector3(Player.position.x, Player.position.y, transform.position.z);
        }
    }

    private void UpdatePlayerTarget()
    {
        // Dynamically find the active player by tag
        GameObject activePlayer = GameObject.FindGameObjectWithTag("Player");
        if (activePlayer != null)
        {
            Player = activePlayer.transform;
        }
    }
}


