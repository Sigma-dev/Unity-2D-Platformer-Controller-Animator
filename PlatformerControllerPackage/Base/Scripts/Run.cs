using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Run : MonoBehaviour
{
    private PlayerStats _playerStats;
    private InputActions _inputActions;
    private InputAction _runAction;
    private Rigidbody2D _rb;
    private void Start()
    {
        PlayerManager pm = GetComponent<PlayerManager>();
        _playerStats = pm.GetStats();
        _inputActions = pm.GetActions();
        _playerStats = GetComponent<PlayerManager>().GetStats();
        _rb = GetComponent<Rigidbody2D>();
        _runAction = _inputActions.Player.Run;
        _runAction.Enable();
    }

    private void FixedUpdate()
    {
        float input = _runAction.ReadValue<float>();
        if (input < 0)
            transform.rotation = Quaternion.Euler(0, 180, 0);
        else if (input > 0)
            transform.rotation = Quaternion.Euler(0,  0, 0);
        _rb.AddForce(Mathf.Abs(input) * _playerStats.horizontalAcceleration * transform.right);
        _rb.AddForce(Vector2.right * (-_rb.velocity.x * _playerStats.horizontalDrag));
    }
}
