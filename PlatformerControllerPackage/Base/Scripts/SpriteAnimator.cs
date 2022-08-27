using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum AnimatorState
{
    Idle,
    Run,
    Jump,
    WallSlide,
    Fall
}

public class SpriteAnimator : MonoBehaviour
{
    public AnimationSet animationSet;
    protected AnimatorState _state;
    private SpriteRenderer _sr;
    private Coroutine _currentAnimCo = null;
    protected virtual void Start()
    {
        _sr = GetComponent<SpriteRenderer>();
        ResetState(AnimatorState.Idle);
    }

    public void ResetState(AnimatorState state)
    {
        _state = state;
        RestartAnimation();
    }

    public void SetOrKeepState(AnimatorState state)
    {
        if (state != _state)
        {
            _state = state;
            RestartAnimation();
        }
    }

    void RestartAnimation()
    {
        Animation anim = animationSet.getAnimation(_state);
        if (anim == null)
            return;
        if (_currentAnimCo != null)
            StopCoroutine(_currentAnimCo);
        _currentAnimCo = StartCoroutine(WaitForNextFrame(anim));
    }

    IEnumerator WaitForNextFrame(Animation anim)
    {
        _sr.sprite = anim.getNextFrame();
        yield return new WaitForSeconds(anim.getFrameWait());
        _currentAnimCo = StartCoroutine(WaitForNextFrame(anim));
    }
}
