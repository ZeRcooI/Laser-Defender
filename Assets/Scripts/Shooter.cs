using System.Collections;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [Header("General")]
    [SerializeField] private GameObject _projectilePrefab;
    [SerializeField] private float _projectileSpeed = 10f;
    [SerializeField] private float _projectileLifetime = 5f;
    [SerializeField] private float _baseFiringRate = 0.2f;

    [Header("AI")]
    [SerializeField] private bool _useAI;
    [SerializeField] private float _firingRateVariance = 0f;
    [SerializeField] private float _minFiringRate = 0.1f;

    [HideInInspector] public bool isFiring;

    private Coroutine _firingCoroutine;
    private AudioPlayer _audioPlayer;

    private void Awake()
    {
        _audioPlayer = FindObjectOfType<AudioPlayer>();
    }

    void Start()
    {
        if (_useAI)
        {
            isFiring = true;
        }
    }

    void Update()
    {
        Fire();
    }

    private void Fire()
    {
        if (isFiring && _firingCoroutine == null)
        {
            _firingCoroutine = StartCoroutine(FireContinuously());
        }
        else if(!isFiring && _firingCoroutine != null)
        {
            StopCoroutine(_firingCoroutine);
            _firingCoroutine = null;
        }
    }

    private IEnumerator FireContinuously()
    {
        while (true)
        {
            GameObject instance = Instantiate(_projectilePrefab, transform.position, Quaternion.identity);

            Rigidbody2D rigidbody2D = instance.GetComponent<Rigidbody2D>();

            if (rigidbody2D != null)
            {
                rigidbody2D.velocity = transform.up * _projectileSpeed;
            }

            Destroy(instance, _projectileLifetime);

            float timeToNextProjectile = Random.Range(_baseFiringRate - _firingRateVariance, _baseFiringRate + _firingRateVariance);
            timeToNextProjectile = Mathf.Clamp(timeToNextProjectile, _minFiringRate, float.MaxValue);

            _audioPlayer.PlayShootingClip();

            yield return new WaitForSeconds(timeToNextProjectile);
        }
    }
}