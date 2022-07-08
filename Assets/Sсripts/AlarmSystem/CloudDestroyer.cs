using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudDestroyer : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Cloud>(out Cloud Cloud))
            Destroy(collision.gameObject);
    }
}
