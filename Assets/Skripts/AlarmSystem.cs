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
    private bool _IsAlarmed = false;

    private AudioSource _audioSource;
    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        GetComponentInParent<House>().PlayerGone += StartAttenuationAlarm;
        _spriteRenderer = GetComponentInParent<SpriteRenderer>();
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
            _audioSource.volume = _initialValume;
            StartCoroutine(FlashingAlarm());
        }
    }

    private void StartAttenuationAlarm()
    {
        if (_IsAlarmed == true)
            StartCoroutine(AttenuationAlarm());
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
    private IEnumerator AttenuationAlarm()
    {
        _runningTime = 0;
        _IsAlarmed = false;

        while (_audioSource.volume > 0 && _IsAlarmed == false)
        {
            _runningTime += Time.deltaTime;
            _audioSource.volume = _initialValume * (1 - _runningTime / _attenuationRate);

            yield return null;
        }
    }
}
