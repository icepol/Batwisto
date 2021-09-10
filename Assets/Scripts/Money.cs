using UnityEngine;

public class Money : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private ParticleSystem deathParticle;
    [SerializeField] private ParticleSystem successParticle;

    private SoundEffects _soundEffects;

    private void Awake()
    {
        _soundEffects = GetComponent<SoundEffects>();
        
        EventManager.AddListener(Events.PLAYER_DIED, OnPlayerDied);
    }

    private void Start()
    {
        _soundEffects.PlayOnCoinSpawn();
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
            EventManager.TriggerEvent(Events.MONEY_DESTROYED);
            
            _soundEffects.PlayOnCoinExplosion();
            
            Instantiate(deathParticle, transform.position, Quaternion.identity);
        }
        else if (other.CompareTag("Player"))
        {
            EventManager.TriggerEvent(Events.MONEY_SUCCESS);
            
            Instantiate(successParticle, transform.position, Quaternion.identity);
        
            Destroy(gameObject);
        }
    }

    void OnPlayerDied()
    {
        Instantiate(deathParticle, transform.position, Quaternion.identity);
            
        Destroy(gameObject);
    }
}
