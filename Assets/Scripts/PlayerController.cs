using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using Photon.Pun;

public class PlayerController : MonoBehaviour
{
    public float DefaultSpeed = 10f;

    public Gun Weapon;

    public TMP_Text NameHolder;

    private Animator _animator;
    private Rigidbody2D _rb;

    public string Name;

    private PhotonView _view;

    private Vector2 _moveInput;

    private bool _isFacingRight = true;
    public bool IsFacingRight
    {
        get
        {
            return _isFacingRight;
        }
        private set
        {
            if (_isFacingRight != value)
            {
                transform.localScale *= new Vector2(-1, 1);
            }
            _isFacingRight = value;
        }
    }
    private bool _isMoving = false;


    public bool isMoving
    {
        get
        {
            return _isMoving;
        }
        private set
        {
            _isMoving = value;
            _animator.SetBool(AnimatorStrings.isMoving, value);
        }
    }

    private Vector2 _shootInput;

    public float CurrentMoveSpeed
    {
        get
        {
            if (CanMove)
            {
                if (isMoving)
                {
                    return DefaultSpeed;
                }
                else
                {
                    return 0;
                }
            }
            else
            {
                return 0;
            }
        }

    }

    public bool CanMove
    {
        get
        {
            return _animator.GetBool(AnimatorStrings.canMove);
        }
    }
    public void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();

        _view = GetComponent<PhotonView>();

        _animator = GetComponent<Animator>();


        Name = _view.IsMine?"You":PhotonNetwork.NickName;

        NameHolder.text = Name;
    }
    public void FixedUpdate()
    {
        if (_view.IsMine)
        {
            _rb.velocity = new(_moveInput.x * CurrentMoveSpeed, _moveInput.y * CurrentMoveSpeed);
            _animator.SetFloat("xVelocity", Mathf.Abs(_rb.velocity.x));
            _animator.SetFloat("yVelocity", _rb.velocity.y);
            NameHolder.transform.localScale = transform.localScale;
            Flip();
            if (_shootInput != Vector2.zero)
            {
                Weapon.Shoot(_shootInput, _view.ViewID);
            }
        }
    }
    private void Flip()
    {
        if (_moveInput.x > 0 && !IsFacingRight)
        {
            IsFacingRight = true;
        }
        else if (_moveInput.x < 0 && IsFacingRight)
        {
            IsFacingRight = false;
        }
    }
    public void onMove(InputAction.CallbackContext context)
    {
        if (!_view.IsMine)
            return;
        _moveInput = context.ReadValue<Vector2>();
        isMoving = _moveInput != Vector2.zero;
    }
    public void onAttack(InputAction.CallbackContext context)
    {
        _shootInput = context.ReadValue<Vector2>();
        if (context.canceled)
        {
            Weapon.HideWeapon();
        }
    }


}
