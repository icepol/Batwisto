using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float minX;
    [SerializeField] private float maxX;

    private bool _isActive;
    private Animator _animator;

    void Awake()
    {
        _animator = GetComponent<Animator>();
        
        EventManager.AddListener(Events.LEVEL_START, OnLevelStart);
        EventManager.AddListener(Events.PLAYER_DIED, OnPlayerDied);
    }

    private void OnDestroy()
    {
        EventManager.RemoveListener(Events.LEVEL_START, OnLevelStart);
        EventManager.RemoveListener(Events.PLAYER_DIED, OnPlayerDied);
    }

    // Update is called once per frame
    void Update()
    {
        if (_isActive)
            HandleControls();
    }

    void HandleControls()
    {
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            Vector2 position = transform.position;
            
            position += Vector2.left * (moveSpeed * Time.deltaTime);
            position.x = Mathf.Clamp(position.x, minX, maxX);
            
            transform.position = position;
            
            _animator.SetBool("IsMoving", true);
        }
        else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            Vector2 position = transform.position;
            
            position += Vector2.right * (moveSpeed * Time.deltaTime);
            position.x = Mathf.Clamp(position.x, minX, maxX);
            
            transform.position = position;
            
            _animator.SetBool("IsMoving", true);
        }
        else
        {
            _animator.SetBool("IsMoving", false);
        }
    }

    void OnLevelStart()
    {
        _isActive = true;
    }

    void OnPlayerDied()
    {
        _isActive = false;
    }
}
