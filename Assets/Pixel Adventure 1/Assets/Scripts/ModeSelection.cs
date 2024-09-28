using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class ModeSelection : MonoBehaviourPunCallbacks
{
    public string joinInput;
    public CreateAndJoinRooms createAndJoinRooms;
    public void SelectSinglePlayer()
    {
        GameManager.Instance.isMultiplayer = false;
        LoadGameplayScene();
    }

    public void SelectMultiplayer()
    {
        GameManager.Instance.isMultiplayer = true;
        ConnectToPhoton();
        UnityEngine.SceneManagement.SceneManager.LoadScene("Lobby 1");
    }

    void LoadGameplayScene()
    {
        // Load the gameplay scene
        UnityEngine.SceneManagement.SceneManager.LoadScene("Gameplay");
    }

    // Connect to Photon Master Server
    void ConnectToPhoton()
    {
        if (!PhotonNetwork.IsConnected)
        {
            PhotonNetwork.ConnectUsingSettings(); // Connects to the Photon Master Server
        }
    }

    // Callback when connected to the Master Server
    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to Master Server.");

        // Now that we're connected, we can join a lobby or create a room
        PhotonNetwork.JoinLobby();
    }

    // Callback when joined to a lobby
    public override void OnJoinedLobby()
    {
        Debug.Log("Joined the Lobby.");

        // Once in the lobby, we can create or join rooms
        UnityEngine.SceneManagement.SceneManager.LoadScene("Lobby");
    }

    // Callback when a room is successfully created
    public override void OnCreatedRoom()
    {
        Debug.Log("Room Created Successfully.");
        LoadGameplayScene();
    }

    // Callback when creating a room fails
    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.LogError($"Room creation failed: {message}");
    }
}
