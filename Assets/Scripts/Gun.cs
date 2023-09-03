using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Gun : MonoBehaviour
{
    public Transform shotPoint;

    public float shotInterval = 0.25f; //Interval between shots

    private float _currentTimeBtwShots;//Time, until the next shot

    private SpriteRenderer _sr;

    void Update()
    {
        _sr = GetComponent<SpriteRenderer>();
    }
    public void Shoot(Vector2 direction, int ViewID)
    {
        Vector2 difference = direction;
        _sr.enabled = true;
        float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        transform.localScale = transform.parent.localScale;
        transform.rotation = Quaternion.Euler(0f, 0f, rotZ);
        if (_currentTimeBtwShots <= 0)
        {
            GameObject bullet = PhotonNetwork.Instantiate("Bullet", shotPoint.position, transform.rotation);
            _currentTimeBtwShots = shotInterval;
        }
        else
        {
            _currentTimeBtwShots -= Time.deltaTime;
        }
    }
    public void HideWeapon()
    {
        _sr.enabled = false;
    }
}
