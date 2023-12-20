using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int _health = 50;
    [SerializeField] private ParticleSystem _hitEffect;

    [SerializeField] private bool _applyCameraShake;
    private CameraShake _cameraShake;

    AudioPlayer _audioPlayer;

    private void Awake()
    {
        _cameraShake = Camera.main.GetComponent<CameraShake>();
        _audioPlayer = FindObjectOfType<AudioPlayer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        DamageDealer damageDealer = collision.GetComponent<DamageDealer>();

        if(damageDealer != null)
        {
            TakeDamage(damageDealer.GetDamage());
            PlayHitEffect();
            _audioPlayer.PlayDamageClip();
            ShakeCamera();
            damageDealer.Hit();
        }
    }

    private void ShakeCamera()
    {
        if(_cameraShake != null && _applyCameraShake)
        {
            _cameraShake.Play();
        }
    }

    private void TakeDamage(int damage)
    {
        _health -= damage;

        if(_health <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void PlayHitEffect()
    {
        if(_hitEffect != null)
        {
            ParticleSystem instance = Instantiate(_hitEffect, transform.position, Quaternion.identity);

            Destroy(instance.gameObject, instance.main.duration + instance.main.startLifetime.constantMax);
        }
    }
}