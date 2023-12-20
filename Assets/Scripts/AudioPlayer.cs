using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    [Header("Shooting")]
    [SerializeField] private AudioClip _shootingClip;
    [SerializeField][Range(0f, 1f)] private float _shootingVolume = 1f;

    [Header("Damage")]
    [SerializeField] private AudioClip _damageClip;
    [SerializeField][Range(0f, 1f)] private float _damageVolume = 1f;

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
        if(audioClip != null)
        {
            Vector3 cameraPosition = Camera.main.transform.position;

            AudioSource.PlayClipAtPoint(audioClip, cameraPosition, volume);
        }
    }
}