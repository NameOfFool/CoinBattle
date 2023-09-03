using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour, IPunObservable, IDagameable
{
    public float maxHP;
    public float _HP;
    [SerializeField] private Slider _bar;

    private PhotonView _view;

    private Animator _animator;
    public float MaxHP
    {
        set { maxHP = value; }
        get
        {
            return maxHP;
        }
    }
    public float CurrentHP
    {
        set
        {
            _HP = value;
            _bar.value = _HP / 100f;
            if (_HP <= 0)
            {
                _animator.SetBool(AnimatorStrings.isAlive, false);
            }
        }
        get
        {
            return _HP;
        }
    }

    public void Start()
    {
        CurrentHP = MaxHP;
        _animator = GetComponent<Animator>();
        _view = GetComponent<PhotonView>();
    }

    public void OnHit(float damage)
    {
        _view.RPC("RPC_TakeDamage", RpcTarget.OthersBuffered, damage);
    }
    [PunRPC]
    private void RPC_TakeDamage(float damage)
    {
        Debug.Log(gameObject.name);
        CurrentHP -= damage;
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(CurrentHP);
        }
        else if (stream.IsReading)
        {
            CurrentHP = (float)stream.ReceiveNext();
        }
    }

}
