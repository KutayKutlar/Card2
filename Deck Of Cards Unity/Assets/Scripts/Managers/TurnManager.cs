using System.Collections;
using System.Collections.Generic;
using SinuousProductions;
using UnityEngine;
using UnityEngine.UI;

public class TurnManager : MonoBehaviour
{
    [SerializeField]private GameObject endTurnButton;
    
    public static TurnManager Instance { get; private set; }
    [SerializeField]private GameObject winPanel;
    [SerializeField]private GameObject loosePanel;
    private bool once;
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }

        Instance = this;
    }

    public enum GameState
    {
        PreparationPhase,
        ActionPhase,
        ResultPhase
    }

    private GameState currentState;

    // Start is called before the first frame update
    void Start()
    {
        SetState(GameState.PreparationPhase);
    }

    // Update is called once per frame
    void Update()
    {
        switch (currentState)
        {
            case GameState.PreparationPhase:
                once = false;
                
                break;

            case GameState.ActionPhase:
                // Action phase logic
                if (!once)
                {
                    
                    StartCoroutine(FightManager.instance.Fight());
                    
                    once = true;
                }
                
                
                break;

            case GameState.ResultPhase:
                
                break;
        }
    }

    public void SetState(GameState newState)
    {
        currentState = newState;
        OnStateChanged(newState);
    }

    private void OnStateChanged(GameState newState)
    {
        // State değiştiğinde yapılacak işlemler
        switch (newState)
        {
            case GameState.PreparationPhase:
                Debug.Log("Preparation Phase başladı.");
                endTurnButton.SetActive(true);
                winPanel.SetActive(false);
                loosePanel.SetActive(false);
                break;

            case GameState.ActionPhase:
                endTurnButton.SetActive(false);
                DrawPileManager.Instance.DrawCard(HandManager.Instance);
                break;

            case GameState.ResultPhase:
                Debug.Log("Result Phase başladı.");
                endTurnButton.SetActive(false);
                if (GameManager.Instance.PlayerHeroHealth <= 0)
                {
                    loosePanel.SetActive(true);
                }
                if (GameManager.Instance.EnemyHeroHealth <= 0)
                {
                    
                    winPanel.SetActive(true);
                    CardSelection.Instance.ShowCards();
                }
                break;
        }
    }
}

    
    

