using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class PlayerManager : MonoBehaviourPunCallbacks
{
    public int coinCounter = 0;
    public GameObject playerUIPrefab;

    public GameObject CoinPrefab;
    private PhotonView view;
    private GameObject _pui;
    private void Start()
    {
        view = GetComponent<PhotonView>();
        if (view.IsMine)
        {
            _pui = Instantiate(playerUIPrefab);
            _pui.SendMessage("SetTarget", this, SendMessageOptions.RequireReceiver);
        }
        if (PhotonNetwork.IsMasterClient)
        {
            view.RPC("SpawnCoins",RpcTarget.MasterClient);
        }

    }

    [PunRPC]
    public void SpawnCoins()
    {
        for (int i = -7; i < 9; i++)
        {
            PhotonNetwork.Instantiate(CoinPrefab.name, new Vector3(i - 0.5f, 3.5f, 0f), Quaternion.identity);
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (view.IsMine && col.gameObject.tag == "Coin")
        {
            coinCounter++;
            Destroy(col.gameObject);
        }
    }
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(coinCounter);
        }
        else if (stream.IsReading)
        {
            coinCounter = (int)stream.ReceiveNext();
        }
    }

}
