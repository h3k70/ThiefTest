using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animation))]

public class SpriteTransparencyChanger : MonoBehaviour
{
    [SerializeField] private Animation _animation;

    private const string HideSprite = "HideSprite";
    private const string ShowSprite = "ShowSprite";

    private void Awake()
    {
        _animation = GetComponent<Animation>();
    }
    public void Hide()
    {
        _animation.Play(HideSprite);
    }

    public void Show()
    {
        _animation.Play(ShowSprite);
    }
}
