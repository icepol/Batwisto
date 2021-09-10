using UnityEngine;

public class RandomSprite : MonoBehaviour
{
    [SerializeField] private Sprite[] sprites;
    [SerializeField] private float minDelay = 0.05f;
    [SerializeField] private float maxDelay = 0.25f;

    private SpriteRenderer _spriteRenderer;
    private float _nextChange;

    private void Awake()
    {
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    void Update()
    {
        _nextChange -= Time.deltaTime;
        
        if (_nextChange > 0) return;

        _spriteRenderer.sprite = sprites[Random.Range(0, sprites.Length)];

        _nextChange = Random.Range(minDelay, maxDelay);
    }
}
