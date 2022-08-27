using UnityEngine;

[CreateAssetMenu(fileName = "PlayerStats", menuName = "ScriptableObjects/PlayerStats", order = 1)]
public class PlayerStats : ScriptableObject
{
    public float horizontalAcceleration = 1f;
    public float horizontalDrag = 1f;

    public int maxJumpNb = 2;
    public float jumpForce = 10f;
    public float aerialDownPull = 1f;
    public float groundCheckRange = 0.025f;

    public Vector2 wallJumpForce = new Vector2(10f, 10f);
    [Range(0.0f, 1.0f)] public float wallSlowdownRate = 0;
    public float wallCheckRange = 0.025f;
}