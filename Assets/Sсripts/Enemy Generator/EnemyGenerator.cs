using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
    [SerializeField] private float _period = 2f;
    [SerializeField] private Enemy[] _templats;

    private Enemy _template;
    private Transform[] _spawnPoints;
    private Transform _spawnPoint;
    private bool _isRunning = false;

    private void Awake()
    {
        _isRunning = true;
        _spawnPoints = gameObject.GetComponentsInChildren<Transform>();
        StartCoroutine(Generate());
    }

    private IEnumerator Generate()
    {
        var wait = new WaitForSeconds(_period);

        while (_isRunning)
        {
            _template = _templats[Random.Range(0, _templats.Length)];
            _spawnPoint = _spawnPoints[Random.Range(0, _spawnPoints.Length)];

            var newObject = Instantiate(_template, _spawnPoint.transform.position, Quaternion.identity);

            yield return wait;
        }
    }
}
