using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
    [SerializeField] private float _period = 2f;
    [SerializeField] private GameObject[] _templats;

    private GameObject _template;
    private SpawnEnemies[] _spawnPoints;
    private SpawnEnemies _spawnPoint;
    private bool _isRunning = false;

    private void Awake()
    {
        _isRunning = true;
        _spawnPoints = gameObject.GetComponentsInChildren<SpawnEnemies>();
        StartCoroutine(Generation());
    }

    private IEnumerator Generation()
    {
        var wait = new WaitForSeconds(_period);

        while (_isRunning)
        {
            _template = _templats[Random.Range(0, _templats.Length)];
            _spawnPoint = _spawnPoints[Random.Range(0, _spawnPoints.Length)];

            _spawnPoint.CreateEemy(_template);

            yield return wait;
        }
    }
}
