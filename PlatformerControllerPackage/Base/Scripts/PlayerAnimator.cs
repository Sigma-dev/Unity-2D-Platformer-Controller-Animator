using System.Collections;
using System.Collections.Generic;
using UnityEngine; 

public class PlayerAnimator : SpriteAnimator
{
    PlayerInfo _playerInfo;
    Status _status;

    protected override void Start()
    {
        base.Start();
        _playerInfo = GetComponent<PlayerManager>().GetInfo();
    }
    void Update()
    {
        if (_status == Status.OnGround)
        {
            if (_playerInfo.justJumped())
            {
                _status = Status.InAir;
                ResetState(AnimatorState.Jump);
            }
            else if (!_playerInfo.isGrounded())
                _status = Status.InAir;
            else if (_playerInfo.isRunning())
                SetOrKeepState(AnimatorState.Run);
            else
                SetOrKeepState(AnimatorState.Idle);
        }
        else if (_status == Status.InAir)
        {
            if (!_playerInfo.isGrounded() || _playerInfo.getVelocity().y > 0)
            {
                if (_playerInfo.getWallStatus() != WallStatus.None)
                    SetOrKeepState(AnimatorState.WallSlide);
                else
                    SetOrKeepState(AnimatorState.Fall);
            }
            else
                _status = Status.OnGround;
        }
        Debug.Log(_status);
    }

    enum Status
    {
        OnGround,
        InAir
    }
}
