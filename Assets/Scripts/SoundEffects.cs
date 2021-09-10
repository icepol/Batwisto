using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class SoundEffects : MonoBehaviour
{
    [SerializeField] private AudioClip coinSpawn;
    [SerializeField] private AudioClip coinPickup;
    [SerializeField] private AudioClip coinExplosion;
    [SerializeField] private AudioClip devilSpawn;
    [SerializeField] private AudioClip devilDie;

    private Transform _cameraTransform;

    private Vector2 GetCameraPosition()
    {
        if (!_cameraTransform)
            _cameraTransform = Camera.main.transform;

        return _cameraTransform.position;
    }

    public void PlayOnCoinSpawn()
    {
        AudioSource.PlayClipAtPoint(coinSpawn, GetCameraPosition());
    }

    public void PlayOnCoinPickup()
    {
        AudioSource.PlayClipAtPoint(coinPickup, GetCameraPosition());
    }
    
    public void PlayOnCoinExplosion()
    {
        AudioSource.PlayClipAtPoint(coinExplosion, GetCameraPosition());
    }

    public void PlayOnDevilSpawn()
    {
        if (devilSpawn)
            AudioSource.PlayClipAtPoint(devilSpawn, GetCameraPosition());
    }

    public void PlayOnDevilDie()
    {
        if (devilDie)
            AudioSource.PlayClipAtPoint(devilDie, GetCameraPosition());
    }
}
