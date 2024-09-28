using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CreateAndJoinRooms : MonoBehaviourPunCallbacks
{
    public InputField createInput;
    public InputField joinInput;

    void Start()
    {
        if (!GameManager.Instance.isMultiplayer)
        {
            // In single-player mode, just load the gameplay scene
            Debug.Log("Single-player mode selected, loading gameplay scene.");
        }
    }

    public void CreateRoom()
    {
        if (!GameManager.Instance.isMultiplayer)
        {
            Debug.LogWarning("CreateRoom called in single-player mode. Ignoring.");
            return; // Exit if we're in single-player mode
        }

        if (string.IsNullOrEmpty(createInput.text))
        {
            Debug.LogWarning("Room name cannot be empty.");
            return;
        }

        if (PhotonNetwork.IsConnectedAndReady)
        {
            PhotonNetwork.CreateRoom(createInput.text);
        }
        else
        {
            Debug.LogWarning("Photon Network is not ready.");
        }
    }

    public void JoinRoom()
    {
        if (!GameManager.Instance.isMultiplayer)
        {
            Debug.LogWarning("JoinRoom called in single-player mode. Ignoring.");
            return; // Exit if we're in single-player mode
        }

        if (string.IsNullOrEmpty(joinInput.text))
        {
            Debug.LogWarning("Room name cannot be empty.");
            return;
        }

        if (PhotonNetwork.IsConnectedAndReady)
        {
            PhotonNetwork.JoinRoom(joinInput.text);
        }
        else
        {
            Debug.LogWarning("Photon Network is not ready.");
        }
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Successfully joined room.");
        PhotonNetwork.LoadLevel("game 1");
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        Debug.LogError("Failed to join room: " + message);
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.LogError("Failed to create room: " + message);
    }
}
