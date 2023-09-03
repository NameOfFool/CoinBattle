using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;

public class ServerConnection : MonoBehaviourPunCallbacks//Another parent class(!!!)
{
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();//Connect to Server
    }

    public override void OnConnectedToMaster()
    {
        SceneManager.LoadScene("LobbyScene");//Load Lobby
    }
}
