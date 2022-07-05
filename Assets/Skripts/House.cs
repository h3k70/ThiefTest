using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class House : MonoBehaviour
{
    [SerializeField] private GameObject _outsideWall;

    public UnityEvent PlayerGone;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Player>(out Player player))
        {
            _outsideWall.GetComponent<ChangingTransparency>().Hide();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Player>(out Player player))
        {
            PlayerGone.Invoke();
            _outsideWall.GetComponent<ChangingTransparency>().Show();
        }
    }
}
