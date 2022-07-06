using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]

public class AlarmSystem : MonoBehaviour
{
    [SerializeField] private House _house;
    [SerializeField] private float _MaxValume = 1f;
    [SerializeField] private float _duration = 2f;
    [SerializeField] private float _frequencyColorChange = 0.5f;

    private float _runningTime;
    private bool _IsAlarmed = false;
    private float _currentVolume;

    private AudioSource _audioSource;
    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _house.PlayerGone += StartAttenuationAlarmSound;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Player>(out Player player))
            RaiseAlarm();
    }

    private void RaiseAlarm()
    {
        if (_IsAlarmed == false)
        {
            _IsAlarmed = true;
            _audioSource.Play();
            StartCoroutine(FlashingAlarm());
            StartCoroutine(IncreaseAlarmSound());
        }
    }

    private void StartAttenuationAlarmSound()
    {
        if (_IsAlarmed == true)
            StartCoroutine(AttenuationAlarmSound());
    }

    private IEnumerator FlashingAlarm()
    {
        var wait = new WaitForSeconds(_frequencyColorChange);

        while (_IsAlarmed)
        {
            _spriteRenderer.color = Color.red;
            yield return wait;
            _spriteRenderer.color = Color.white;
            yield return wait;
        }
    }

    private IEnumerator IncreaseAlarmSound()
    {
        _runningTime = 0;
        _currentVolume = _audioSource.volume;

        while (_audioSource.volume < _MaxValume && _IsAlarmed == true)
        {
            _runningTime += Time.deltaTime;
            _audioSource.volume = Mathf.MoveTowards(_currentVolume, _MaxValume, _runningTime / _duration);

            yield return null;
        }
    }

    private IEnumerator AttenuationAlarmSound()
    {
        _runningTime = 0;
        _IsAlarmed = false;
        _currentVolume = _audioSource.volume;

        while (_audioSource.volume > 0 && _IsAlarmed == false)
        {
            _runningTime += Time.deltaTime;
            _audioSource.volume = Mathf.MoveTowards(_currentVolume, 0, _runningTime / _duration);

            yield return null;
        }
    }
}
