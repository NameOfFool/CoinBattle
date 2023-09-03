using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MatchStatus : MonoBehaviourPunCallbacks
{
    public GameObject CoinPrefab;

    void Start()
    {
        if (PhotonNetwork.IsMasterClient)
            for (int i = -7; i < 9; i++)
            {
                PhotonNetwork.Instantiate(CoinPrefab.name, new Vector3(i - 0.5f, 3.5f, 0f), Quaternion.identity);
            }
    }
    public void GetWinner()
    {
        List<GameObject> players = GameObject.FindGameObjectsWithTag("Player").ToList();
        int alivePlayers = players.Count(g => g.GetComponent<Animator>().GetBool(AnimatorStrings.isAlive));
        if (alivePlayers == 1)
        {
            GameObject winner = players.Where(g => g.GetComponent<Animator>().GetBool(AnimatorStrings.isAlive)).SingleOrDefault();

            UI ui = GameObject.FindGameObjectWithTag("PUI").GetComponent<UI>();
            int coins = winner.GetComponent<PlayerManager>().coinCounter;
            ui.ShowWindow($"Победил Игрок:{winner.GetPhotonView().Owner} с общей суммой монет: {coins}");
        }
    }
}
