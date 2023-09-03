using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;

public class UI : MonoBehaviour
{
    private PlayerManager target;

    [SerializeField]private GameObject window;

    [SerializeField]private TMP_Text coinCounter;

    public void SetTarget(PlayerManager _target)
    {
        target = _target;
    }
    void Update()
    {
        if (target != null)
        {
            coinCounter.text = target.coinCounter+"";
        }
    }

    public void ShowWindow(string message)
    {
        window.SetActive(true);
        window.GetComponentInChildren<TMP_Text>().text = message;
    }
    public void onOK()
    {
        PhotonNetwork.LeaveRoom();
        window.SetActive(false);
        SceneManager.LoadScene(0);
    }
}
