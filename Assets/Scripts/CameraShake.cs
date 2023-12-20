using System.Collections;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    [SerializeField] private float _shakeDuration = 1f;
    [SerializeField] private float _shakeMagnitude = 1f;

    private Vector3 _initializationPosition;

    private void Start()
    {
        _initializationPosition = transform.position;
    }

    public void Play()
    {
        StartCoroutine(Shake());
    }

    private IEnumerator Shake()
    {
        float elapsedTime = 0;

        while (elapsedTime < _shakeDuration)
        {
            transform.position = _initializationPosition + (Vector3)Random.insideUnitCircle * _shakeMagnitude;

            elapsedTime += Time.deltaTime;

            yield return new WaitForEndOfFrame();
        }

        transform.position = _initializationPosition;
    }
}