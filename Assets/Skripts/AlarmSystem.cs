using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]

public class AlarmSystem : MonoBehaviour
{
    [SerializeField] private float _initialValume = 0.1f;
    [SerializeField] private float _attenuationRate = 2f;
    [SerializeField] private float _frequencyColorChange = 0.5f;

    private float _runningTime;
    private bool _alarm = false;

    private AudioSource _audioSource;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        GetComponentInParent<House>().PlayerGone.AddListener(StartAttenuationAlarm);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Player>(out Player player))
            Alarm();
    }

    private void Alarm()
    {
        if (_alarm == false)
        {
            _alarm = true;
            _audioSource.Play();
            _audioSource.volume = _initialValume;
            StartCoroutine(AlarmColor());
        }
    }

    private void StartAttenuationAlarm()
    {
        if (_alarm == true)
            StartCoroutine(AttenuationAlarm());
    }

    private IEnumerator AlarmColor()
    {
        var wait = new WaitForSeconds(_frequencyColorChange);

        while (_alarm)
        {
            GetComponent<SpriteRenderer>().color = Color.red;
            yield return wait;
            GetComponent<SpriteRenderer>().color = Color.white;
            yield return wait;
        }
    }
    private IEnumerator AttenuationAlarm()
    {
        _runningTime = 0;
        _alarm = false;

        while (_audioSource.volume > 0 && _alarm == false)
        {
            _runningTime += Time.deltaTime;
            _audioSource.volume = _initialValume * (1 - _runningTime / _attenuationRate);

            yield return null;
        }
    }
}
