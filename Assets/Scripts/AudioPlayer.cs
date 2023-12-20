using System;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    [Header("Shooting")]
    [SerializeField] private AudioClip _shootingClip;
    [SerializeField][Range(0f, 1f)] private float _shootingVolume = 1f;

    [Header("Damage")]
    [SerializeField] private AudioClip _damageClip;
    [SerializeField][Range(0f, 1f)] private float _damageVolume = 1f;

    private static AudioPlayer _instance;

    private void Awake()
    {
        ManageSingletone();
    }

    private void ManageSingletone()
    {
        if (_instance != null)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void PlayShootingClip()
    {
        PlayClip(_shootingClip, _shootingVolume);
    }

    public void PlayDamageClip()
    {
        PlayClip(_damageClip, _damageVolume);
    }

    private void PlayClip(AudioClip audioClip, float volume)
    {
        if (audioClip != null)
        {
            Vector3 cameraPosition = Camera.main.transform.position;

            AudioSource.PlayClipAtPoint(audioClip, cameraPosition, volume);
        }
    }
}