using UnityEngine;

public class Devil : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private ParticleSystem deathParticle;

    private SoundEffects _soundEffects;

    private void Awake()
    {
        _soundEffects = GetComponent<SoundEffects>();

        EventManager.AddListener(Events.PLAYER_DIED, OnPlayerDied);
    }

    private void OnDestroy()
    {
        EventManager.RemoveListener(Events.PLAYER_DIED, OnPlayerDied);
    }

    void Update()
    {
        if (!GameState.isGameRunning)
            return;
        
        Vector2 position = transform.position;
            
        position += Vector2.down * (moveSpeed * Time.deltaTime);

        transform.position = position;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("DeathZone"))
        {
            Destroy(gameObject, 1);
        }
        else if (other.CompareTag("Player"))
        {
            EventManager.TriggerEvent(Events.DEVIL_CONTACT);
            
            Explode();
        }
    }

    void OnPlayerDied()
    {
        Explode();
    }

    void Explode()
    {
        Instantiate(deathParticle, transform.position, Quaternion.identity);
        
        _soundEffects.PlayOnDevilDie();
        
        Destroy(gameObject);
    }
}
