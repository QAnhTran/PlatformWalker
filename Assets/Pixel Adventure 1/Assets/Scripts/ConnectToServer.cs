using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;

public class ConnectToServer : MonoBehaviourPunCallbacks
{
    private void Start()
    {
        // Check if multiplayer mode is selected
        if (GameManager.Instance.isMultiplayer)
        {
            Debug.Log("Multiplayer mode selected. Connecting to Photon...");
            PhotonNetwork.ConnectUsingSettings();
        }
        else
        {
            Debug.Log("Single Player mode selected. Skipping Photon connection.");
        }
    }

    // Called when connected to the Master server
    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to Master Server.");

        if (GameManager.Instance.isMultiplayer)
        {
            Debug.Log("Joining Lobby...");
            PhotonNetwork.JoinLobby();
        }
    }

    // Called when joined the lobby
    public override void OnJoinedLobby()
    {
        Debug.Log("Joined Lobby.");

        if (GameManager.Instance.isMultiplayer)
        {
            Debug.Log("Loading Lobby scene...");
            SceneManager.LoadScene("Lobby");
        }
    }

    // Handle disconnection or failure
    public override void OnDisconnected(Photon.Realtime.DisconnectCause cause)
    {
        Debug.LogError("Disconnected from Photon: " + cause);
    }
}
