using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private ParticleSystem explosion;

    private Animator _animator;
    private SoundEffects _soundEffects;

    private float _immortalTime = 0; 
    
    void Awake()
    {
        _animator = GetComponent<Animator>();
        _soundEffects = GetComponent<SoundEffects>();
        
        EventManager.AddListener(Events.PLAYER_DIED, OnPlayerDied);
        EventManager.AddListener(Events.MONEY_SUCCESS, OnMoneySuccess);
        EventManager.AddListener(Events.DEVIL_CONTACT, OnDevilContact);
    }

    private void Update()
    {
        if (_immortalTime > 0)
            _immortalTime -= Time.deltaTime;
    }

    private void OnDestroy()
    {
        EventManager.RemoveListener(Events.PLAYER_DIED, OnPlayerDied);
        EventManager.RemoveListener(Events.MONEY_SUCCESS, OnMoneySuccess);
        EventManager.RemoveListener(Events.DEVIL_CONTACT, OnDevilContact);
    }

    private void OnDevilContact()
    {
        if (_immortalTime > 0) return;
        
        EventManager.TriggerEvent(Events.DECREASE_LIVES);

        _immortalTime = 1;
    }

    void OnPlayerDied()
    {
        Instantiate(explosion, transform.position, Quaternion.identity);
        
        Destroy(gameObject);
    }

    void OnMoneySuccess()
    {
        _animator.SetTrigger("IsHappy");
        
        _soundEffects.PlayOnCoinPickup();
    }
}
