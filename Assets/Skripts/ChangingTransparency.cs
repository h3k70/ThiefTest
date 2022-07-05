using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]

public class ChangingTransparency : MonoBehaviour
{
    [SerializeField] private Animation _animation;

    private void Awake()
    {
        _animation = GetComponent<Animation>();
    }
    public void Hide()
    {
        _animation.Play("HideSprite");
    }

    public void Show()
    {
        _animation.Play("ShowSprite");
    }
}
