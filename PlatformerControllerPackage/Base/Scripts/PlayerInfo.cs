using UnityEngine;
using System.Collections.Generic;

enum CachedEvent
{
    Cached,
    True,
    False
}

public class PlayerInfo
{
    private bool _isGrounded = false;
    private WallStatus _wallStatus = WallStatus.None;
    private bool _isRunning = false;
    private CachedEvent _justJumped = CachedEvent.False;

    private Transform _transform;
    private PlayerStats _playerStats;
    private BoxCollider2D _collider;
    private Rigidbody2D _rb;

    public PlayerInfo(Transform playerTransform, PlayerStats playerStats)
    {
        _transform = playerTransform;
        _playerStats = playerStats;
        _collider = playerTransform.GetComponent<BoxCollider2D>();
        _rb = playerTransform.GetComponent<Rigidbody2D>();
        UpdateInfo();
    }

    public bool isGrounded()
    {
        return _isGrounded;
    }
    public WallStatus getWallStatus()
    {
        return _wallStatus;
    }

    public bool isRunning()
    {
        return _isRunning;
    }

    public bool justJumped()
    {
        return _justJumped == CachedEvent.True;
    }

    public Vector2 getVelocity()
    {
        return _rb.velocity;
    }

    public void UpdateInfo()
    {
        _isGrounded = CheckGround();
        _wallStatus = CheckWallStatus();
        _isRunning = CheckRunning();
        _justJumped = CheckJustJumped();
    }

    private CachedEvent CheckJustJumped()
    {
        if (_justJumped == CachedEvent.Cached)
            return CachedEvent.True;
        else
            return CachedEvent.False;
    }

    private bool CheckRunning()
    {
        if (!_isGrounded)
            return false;
        if (Mathf.Abs(_rb.velocity.x) < 3f)
            return false;
        return true;
    }

    private bool CheckGround()
    {
        bool isOnGround = false;
        int floorMask = LayerMask.GetMask("Floor");
        float yOff = -_collider.size.y / 2;
        for (int i = -1; i < 2; i += 2)
        {
            float xOff = (_collider.size.x / 2 - 0.1f) * i;
            Vector2 origin = new Vector2(_transform.position.x + xOff,
                _transform.position.y + yOff);
            RaycastHit2D hit = Physics2D.Raycast(origin, -_transform.up, _playerStats.groundCheckRange, floorMask);
            if (hit)
                isOnGround = true;
        }
        return isOnGround;
    }

    private WallStatus CheckWallStatus()
    {
        WallStatus status = WallStatus.None;
        int floorMask = LayerMask.GetMask("Floor");
        Dictionary<int, WallStatus> dirs = new()
        {
            { -1, WallStatus.Left },
            { 1, WallStatus.Right }
        };
        for (int i = -1; i < 2; i += 2)
        {
            float xOff = _collider.size.x * i;
            Vector2 origin = new(_transform.position.x + xOff,
                _transform.position.y);
            RaycastHit2D hit = Physics2D.Raycast(origin, _transform.right * i, _playerStats.groundCheckRange, floorMask);
            if (!hit)
                continue;
            if (status == WallStatus.None)
                status = dirs[i];
        }
        return status;
    }

    public void CacheJustJumped()
    {
        _justJumped = CachedEvent.Cached;
    }
}

