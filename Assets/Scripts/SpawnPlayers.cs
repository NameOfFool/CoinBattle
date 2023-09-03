using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlayers : MonoBehaviour
{
    public GameObject player;
    public float minX, minY, maxX, maxY;

    public void Start()
    {
        Vector2 spawnPoint = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
        GameObject initPlayer = PhotonNetwork.Instantiate(player.name, spawnPoint, Quaternion.identity);
        int playerNumber = PhotonNetwork.CurrentRoom.PlayerCount;
        PhotonNetwork.NickName = "Player " + playerNumber;

    }
}
