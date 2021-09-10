using UnityEngine;

public class CameraShake : MonoBehaviour
{
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        
        EventManager.AddListener(Events.PLAYER_DIED, OnPlayerDied);
        EventManager.AddListener(Events.DEVIL_START, OnDevilStart);
    }

    private void OnDestroy()
    {
        EventManager.RemoveListener(Events.PLAYER_DIED, OnPlayerDied);
        EventManager.RemoveListener(Events.DEVIL_START, OnDevilStart);
    }

    void OnPlayerDied()
    {
        _animator.SetTrigger("Shake");
    }

    void OnDevilStart()
    {
        _animator.SetTrigger("Shake");
    }
}
