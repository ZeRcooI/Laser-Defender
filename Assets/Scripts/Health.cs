using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private bool _isPlayer;
    [SerializeField] private int _health = 50;
    [SerializeField] private int _score = 50;
    [SerializeField] private ParticleSystem _hitEffect;

    [SerializeField] private bool _applyCameraShake;
    private CameraShake _cameraShake;

    private AudioPlayer _audioPlayer;
    private ScoreKeeper _scoreKeeper;
    LevelManager _levelManager;

    private void Awake()
    {
        _cameraShake = Camera.main.GetComponent<CameraShake>();
        _audioPlayer = FindObjectOfType<AudioPlayer>();
        _scoreKeeper = FindObjectOfType<ScoreKeeper>();
        _levelManager = FindObjectOfType<LevelManager>();
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

    public int GetHealth()
    {
        return _health;
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
            Die();
        }
    }

    private void Die()
    {
        if(!_isPlayer)
        {
            _scoreKeeper.ModifyScore(_score);
        }
        else
        {
            _levelManager.LoadGameOver();
        }

        Destroy(gameObject);
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