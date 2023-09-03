using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class PlayerManager : MonoBehaviourPunCallbacks
{
    public int coinCounter = 0;
    public GameObject playerUIPrefab;
    private PhotonView view;
    private GameObject _pui;
    private void Start()
    {
        view = GetComponent<PhotonView>();
        if (view.IsMine)
        {
            _pui = GameObject.Find(playerUIPrefab.name);
            _pui.SendMessage("SetTarget", this, SendMessageOptions.RequireReceiver);
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
