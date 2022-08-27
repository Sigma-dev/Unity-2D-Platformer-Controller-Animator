using UnityEngine;

[CreateAssetMenu(fileName = "AnimationSet", menuName = "ScriptableObjects/AnimationSet", order = 2)]
public class AnimationSet : ScriptableObject
{
    public Animation[] animations;

    public Animation getAnimation(AnimatorState state)
    {
        foreach (Animation anim in animations)
        {
            if (anim.getState() == state)
                return anim;
        }
        return null;
    }
}

[System.Serializable]
public class Animation
{
    [SerializeField]
    private AnimatorState state;
    [SerializeField]
    private float frameWait = 0.5f;
    [SerializeField]
    public Sprite[] sprites;
    int _currentIndex = 0;

    public Sprite getNextFrame()
    {
        Sprite sprite = sprites[_currentIndex];
        _currentIndex ++;
        if (_currentIndex >= sprites.Length)
            _currentIndex = 0;
        return sprite;
    }

    public float getFrameWait()
    {
        return frameWait;
    }

    public AnimatorState getState()
    {
        return state;
    }

}

    
