using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

public class MenuManager : MonoBehaviourPunCallbacks
{
    public TMP_InputField CreateRoomInput;
    public TMP_InputField JoinRoomInput;

    public int MaxPlayers = 2;

    public void CreateRoom()
    {
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = MaxPlayers;
        PhotonNetwork.CreateRoom(CreateRoomInput.text, roomOptions);
    }
    public void JoinRoom()
    {
        PhotonNetwork.JoinRoom(JoinRoomInput.text);
    }
    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel("GameScene");
    }
}
