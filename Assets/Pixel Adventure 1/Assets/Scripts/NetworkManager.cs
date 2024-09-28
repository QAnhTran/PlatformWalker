using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class NetworkManager : MonoBehaviourPunCallbacks
{
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby();
    }

    public void CreateRoom()
    {
        PhotonNetwork.CreateRoom("Room1", new RoomOptions { MaxPlayers = 2 });
    }

    public void JoinRoom()
    {
        PhotonNetwork.JoinRoom("Room1");
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Joined room successfully");
        // Load the race scene or spawn players here
        PhotonNetwork.LoadLevel("RaceScene");
    }
}
