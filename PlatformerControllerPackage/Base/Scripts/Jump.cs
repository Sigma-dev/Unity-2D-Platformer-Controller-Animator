using UnityEngine;
using System.Collections.Generic;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class Jump : MonoBehaviour
{
    private PlayerInfo _playerInfo;
    private PlayerStats _playerStats;
    private InputActions _inputActions;
    private Rigidbody2D _rb;

    private int _jumpsLeft;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }
    private void OnEnable()
    {
        PlayerManager pm = GetComponent<PlayerManager>();
        _playerInfo = pm.GetInfo();
        _playerStats = pm.GetStats();
        _inputActions = pm.GetActions();
        _inputActions.Player.Jump.performed += OnJumpAction;
        _inputActions.Player.Jump.Enable();
    }

    private void FixedUpdate()
    {
        if (!_playerInfo.isGrounded())
            _rb.AddForce(-transform.up * _playerStats.aerialDownPull);
        if (_playerInfo.getWallStatus() != WallStatus.None && _rb.velocity.y < 0 && Mathf.Abs(_rb.velocity.x) < 0.1f)
        {
            Vector2 slowForce = transform.up * (-_rb.velocity.y * _playerStats.wallSlowdownRate);
            _rb.AddForce(slowForce, ForceMode2D.Impulse);
        }
    }

    private void OnJumpAction(InputAction.CallbackContext context)
    {
        WallStatus status = _playerInfo.getWallStatus();
        if (_playerInfo.isGrounded())
            _jumpsLeft = _playerStats.maxJumpNb;
        if (status != WallStatus.None)
        {
            if (status == WallStatus.Left)
                transform.rotation = Quaternion.Euler(0, 0, 0);
            else
                transform.rotation = Quaternion.Euler(0, 180, 0);
            _rb.velocity = new(_playerStats.wallJumpForce.x * -(int)_playerInfo.getWallStatus(), _playerStats.wallJumpForce.y);
        }
        else if (_jumpsLeft > 0)
        {
            _jumpsLeft--;
            Vector2 jumpVel = new(_rb.velocity.x, _playerStats.jumpForce);
            _rb.velocity = jumpVel;
            _playerInfo.CacheJustJumped();
        }
    }
}
