using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(SpriteRenderer))]

public class AlarmSystem : MonoBehaviour
{
    [SerializeField] private House _house;
    [SerializeField] private float _MaxValume = 1f;
    [SerializeField] private float _duration = 2f;
    [SerializeField] private float _frequencyColorChange = 0.5f;

    private float _runningTime;
    private bool _IsAlarmed = false;
    private float _currentVolume;
    private Coroutine _changingVolumeJob;

    private AudioSource _audioSource;
    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _house.PlayerGone += StopAlarm;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Player>(out Player player))
        {
            if (_IsAlarmed == false)
            {
                RaiseAlarm();
            }
        }
    }

    private void RaiseAlarm()
    {
        _IsAlarmed = true;
        _audioSource.Play();
        StartCoroutine(FlashingAlarm());

        if (_changingVolumeJob != null)
            StopCoroutine(_changingVolumeJob);

        _changingVolumeJob = StartCoroutine(ChangingVolume(_MaxValume));
    }
    private void StopAlarm()
    {
        if (_IsAlarmed)
        {
            _IsAlarmed = false;
            StopCoroutine(_changingVolumeJob);
            _changingVolumeJob = StartCoroutine(ChangingVolume(0));
        }
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

    private IEnumerator ChangingVolume(float boundaryValue)
    {
        _runningTime = 0;
        _currentVolume = _audioSource.volume;

        while (_audioSource.volume != boundaryValue)
        {
            _runningTime += Time.deltaTime;
            _audioSource.volume = Mathf.MoveTowards(_currentVolume, boundaryValue, _runningTime / _duration);

            yield return null;
        }
    }
}
