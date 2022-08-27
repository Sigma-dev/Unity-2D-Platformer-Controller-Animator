using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] private float lerpSpeed = 0.01f;
    static Transform _target;
    Vector2 _targetPos = Vector2.zero;
    public static void setCameraTarget(Transform target)
    {
        _target = target;
    }

    private void LateUpdate()
    {
        if (_target)
            _targetPos = _target.transform.position;
        Vector3 finalPos = new(_targetPos.x, _targetPos.y, -10);
        float blend = 1f - Mathf.Pow(1f - lerpSpeed, Time.deltaTime * 30f);
        transform.position = Vector3.Lerp(transform.position, finalPos, lerpSpeed);
    }
}
