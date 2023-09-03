using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float Speed = 50;

    private PhotonView _view;

    public float LifeTime = 1;

    public float Distance = 100f;

    public float Damage = 10;

    public LayerMask EnemyMask;

    private void Start()
    {
        Invoke("DestroyBullet", LifeTime);
        
        _view = GetComponent<PhotonView>();
    }
    private void Update()
    {
        RaycastHit2D raycast = Physics2D.Raycast(transform.position, transform.up, Distance, EnemyMask);
        if (raycast.collider != null && raycast.collider.GetComponent<PhotonView>().Owner != _view.Owner)
        {
            if (raycast.collider.CompareTag("Player"))
            {
                raycast.collider.GetComponent<IDagameable>()?.OnHit(Damage);
            }
            DestroyBullet();
        }
        transform.Translate(Vector2.right * Speed * Time.deltaTime);
    }
    private void DestroyBullet()
    {
        if(_view.IsMine)
            PhotonNetwork.Destroy(gameObject);
    }
}
