using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloud : MonoBehaviour
{
    [SerializeField] private float _maxSpeed = 0.4f;
    [SerializeField] private float _minSpeed = 0.1f;

    private float _speed;

    private void Awake()
    {
        _speed = Random.Range(_minSpeed, _maxSpeed);
    }

    private void FixedUpdate()
    {
        transform.Translate(Vector3.right * Time.deltaTime * _speed);
    }
}
