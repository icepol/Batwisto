using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private float minX;
    [SerializeField] private float maxX;
    
    [SerializeField] private float startSpawnDelay;
    [SerializeField] private float finalSpawnDelay;
    [SerializeField] private float spawnDelayRandomness;
    [SerializeField] private float spawnDelayStep;
    
    [SerializeField] private float devilProbability;

    [SerializeField] private Money moneyPrefab;
    [SerializeField] private DevilGroup devilGroupPrefab;

    private bool _isActive;
    private float _spawnDelay;
    private bool _isDevilOnScreen;
    
    void Awake()
    {
        EventManager.AddListener(Events.LEVEL_START, OnLevelStart);
        EventManager.AddListener(Events.DEVIL_START, OnDevilStart);
        EventManager.AddListener(Events.DEVIL_END, OnDevilEnd);
        
        _spawnDelay = startSpawnDelay;
    }

    private void OnDestroy()
    {
        EventManager.RemoveListener(Events.LEVEL_START, OnLevelStart);
        EventManager.RemoveListener(Events.DEVIL_START, OnDevilStart);
        EventManager.RemoveListener(Events.DEVIL_END, OnDevilEnd);
    }
    
    IEnumerator SpawnerLoop()
    {
        while (true)
        {
            yield return new WaitForSeconds(_spawnDelay + Random.Range(-spawnDelayRandomness, spawnDelayRandomness));

            if (!GameState.isGameRunning)
                break;
            
            if (_isDevilOnScreen)
                // do nothing in such case
                yield return null;
            else if (Random.Range(0f, 100f) < devilProbability)
                SpawnDevil();
            else
                SpawnMoney();
        }
    }

    private void OnLevelStart()
    {
        StartCoroutine(SpawnerLoop());
    }

    private void OnDevilStart()
    {
        StartCoroutine(WaitAndDevil());
    }

    IEnumerator WaitAndDevil()
    {
        if (GameState.isGameRunning)
        {
            Vector2 position = new Vector2(Random.Range(minX, maxX), transform.position.y + 2f);

            Instantiate(devilGroupPrefab, position, Quaternion.identity);

            // increase spawn speed 
            _spawnDelay -= spawnDelayStep;
            _spawnDelay = Mathf.Max(_spawnDelay, finalSpawnDelay);

            yield return new WaitForSeconds(1.2f);

            EventManager.TriggerEvent(Events.DEVIL_END);
        }
    }

    private void OnDevilEnd()
    {
        _isDevilOnScreen = false;
    }

    void SpawnDevil()
    {
        _isDevilOnScreen = true;
        
        EventManager.TriggerEvent(Events.DEVIL_START);
    }

    void SpawnMoney()
    {
        Vector2 position = new Vector2(Random.Range(minX, maxX), transform.position.y);
        
        Instantiate(moneyPrefab, position, Quaternion.identity);
    }
}
