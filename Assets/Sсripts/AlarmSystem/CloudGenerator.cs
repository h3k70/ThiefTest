using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudGenerator : MonoBehaviour
{
    [SerializeField] private float _period = 20f;
    [SerializeField] private float _spread = 1f;
    [SerializeField] private GameObject[] _templats = new GameObject[2];

    private GameObject _template;
    private bool _isRunning = false;

    private void Awake()
    {
        _isRunning = true;
        StartCoroutine(Generation());
    }

    private IEnumerator Generation()
    {
        var wait = new WaitForSeconds(_period);

        while (_isRunning)
        {
            _template = _templats[Random.Range(0, _templats.Length)];
            var position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + Random.Range(-_spread, _spread), gameObject.transform.position.z);
            GameObject newObject = Instantiate(_template, position, Quaternion.identity);
            yield return wait;
        }
    }
}
