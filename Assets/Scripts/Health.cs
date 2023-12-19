using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int _health = 50;
    [SerializeField] private ParticleSystem _hitEffect;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        DamageDealer damageDealer = collision.GetComponent<DamageDealer>();

        if(damageDealer != null)
        {
            TakeDamage(damageDealer.GetDamage());
            PlayHitEffect();
            damageDealer.Hit();
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