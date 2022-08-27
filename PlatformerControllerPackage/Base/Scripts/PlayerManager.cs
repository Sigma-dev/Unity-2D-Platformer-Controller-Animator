using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(BoxCollider2D))]
public class PlayerManager : MonoBehaviour
{
    [SerializeField] private PlayerStats playerStats;
    private InputActions _inputActions;
    private PlayerInfo _playerInfo;

    private void Awake()
    {
        CameraManager.setCameraTarget(transform);
    }
    public PlayerStats GetStats()
    {
        return playerStats;
    }

    public PlayerInfo GetInfo()
    {
        if (_playerInfo == null)
            _playerInfo = new(transform, playerStats);
        return _playerInfo;
    }

    private void Update()
    {
        _playerInfo.UpdateInfo();
    }

    public InputActions GetActions()
    {
        if (_inputActions == null)
            _inputActions = new InputActions();
        return _inputActions;
    }
}
public enum WallStatus
{
    None,
    Left = -1,
    Right = 1,
}
