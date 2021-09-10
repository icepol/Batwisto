using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject menuPanel;
    [SerializeField] private GameObject gamePanel;
    [SerializeField] private GameObject gameOverPanel;

    [SerializeField] private Text scoreText;

    [SerializeField] private Image[] lives;
    
    void Awake()
    {
        EventManager.AddListener(Events.LEVEL_START, OnLevelStart);
        EventManager.AddListener(Events.PLAYER_DIED, OnPlayerDied);
        EventManager.AddListener(Events.MONEY_SUCCESS, OnMoneySuccess);
        EventManager.AddListener(Events.MONEY_DESTROYED, OnMoneyDestroyed);
        EventManager.AddListener(Events.DECREASE_LIVES, OnDecreaseLives);
    }

    private void Start()
    {
        HideAllUI();
        
        if (!GameState.isFirstRun)
        {
            // run the game after load
            EventManager.TriggerEvent(Events.LEVEL_START);
        }
        else
        {
            // show first run menu
            menuPanel.SetActive(true);
            GameState.isMenuActive = true;
        }
    }

    private void OnDestroy()
    {
        EventManager.RemoveListener(Events.LEVEL_START, OnLevelStart);
        EventManager.RemoveListener(Events.PLAYER_DIED, OnPlayerDied);
        EventManager.RemoveListener(Events.MONEY_SUCCESS, OnMoneySuccess);
        EventManager.RemoveListener(Events.MONEY_DESTROYED, OnMoneyDestroyed);
        EventManager.RemoveListener(Events.DECREASE_LIVES, OnDecreaseLives);
    }

    private void Update()
    {
        if (GameState.isMenuActive && Input.anyKeyDown)
        {
            if (GameState.isFirstRun)
            {
                EventManager.TriggerEvent(Events.LEVEL_START);
            }
            else
            {
                // reload the scene
                SceneManager.LoadScene("Game");
            }
        }
    }

    void HideAllUI()
    {
        menuPanel.SetActive(false);
        gamePanel.SetActive(false);
        gameOverPanel.SetActive(false);
    }

    void UpdateScore()
    {
        scoreText.text = GameState.moneyCatched.ToString();
    }

    void UpdateLives()
    {
        for (int i = 0; i < 3; i++)
        {
            Image image = lives[i];

            if (GameState.lives <= i)
            {
                image.color = new Color(1, 1, 1, 0.35f);
            }
        }
    }

    void OnLevelStart()
    {
        // hide menu and show game UI
        HideAllUI();
        gamePanel.SetActive(true);

        GameState.lives = 3;
        GameState.moneyCatched = 0;
        GameState.isGameRunning = true;
        GameState.isMenuActive = false;
        
        UpdateScore();
    }

    void OnPlayerDied()
    {
        // show game over screen
        menuPanel.SetActive(true);
        gameOverPanel.SetActive(true);

        GameState.isGameRunning = false;
        GameState.isFirstRun = false;

        StartCoroutine(DelayMenuActive());
    }

    void OnMoneySuccess()
    {
        GameState.moneyCatched++;
        
        UpdateScore();
    }

    void OnMoneyDestroyed()
    {
        DecreaseLives();
    }

    void OnDecreaseLives()
    {
        DecreaseLives();
    }

    void DecreaseLives()
    {
        GameState.lives--;

        UpdateLives();
        
        if (GameState.lives <= 0)
            EventManager.TriggerEvent(Events.PLAYER_DIED);
    }

    IEnumerator DelayMenuActive()
    {
        yield return new WaitForSeconds(0.5f);
        
        GameState.isMenuActive = true;
    }
}
