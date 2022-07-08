using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class House : MonoBehaviour
{
    [SerializeField] private SpriteTransparencyChanger _outsideWall;
    [SerializeField] private UnityEvent _playerGone;

    public event UnityAction PlayerGone
    {
        add => _playerGone.AddListener(value);
        remove => _playerGone.RemoveListener(value);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Player>(out Player player))
        {
            _outsideWall.Hide();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Player>(out Player player))
        {
            _playerGone.Invoke();
            _outsideWall.Show();
        }
    }
}
