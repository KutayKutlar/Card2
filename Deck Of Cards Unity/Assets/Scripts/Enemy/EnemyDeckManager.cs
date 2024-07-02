using System;
using System.Collections;
using System.Collections.Generic;
using SinuousProductions;
using UnityEngine;
using NaughtyAttributes;
using Random = UnityEngine.Random;

public class EnemyDeckManager : MonoBehaviour
{
    [SerializeField] private List<Card> allCards = new List<Card>();
    [SerializeField] private int howManyCardsHaveEnemy;
    public bool isFirstTurn;

    public static EnemyDeckManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        isFirstTurn = true;
    }

    [Button("Take Cards")]
    public void TakeAllCards()
    {
        if (isFirstTurn)
        {
            
            Character[] enemyCards = Resources.LoadAll<Character>("Cards");


            for (int i = 0; i < 4; i++)
            {
                if (Random.Range(0, 2) % 2 == 1)
                {
                    GridCell cell = GridManager.Instance.gridCells[1, i].GetComponent<GridCell>();
                    if (!cell.cellFull)
                    {
                        Vector2 targetPos = new Vector2(1, i);
                        int rand = Random.Range(0, enemyCards.Length - 1);
                        GridManager.Instance.AddObjectToGrid(enemyCards[rand].prefab, targetPos);
                        cell.objectInCell.GetComponent<CharacterStats>().characterStartData = enemyCards[rand];
                        cell.objectInCell.transform.localScale = new Vector3(-cell.objectInCell.transform.localScale.x,
                            cell.objectInCell.transform.localScale.y, cell.objectInCell.transform.localScale.z);
                    }
                }
            }

            int number = 0;
            for (int i = 0; i < 4; i++)
            {
                GridCell cell = GridManager.Instance.gridCells[1, i].GetComponent<GridCell>();
                if (cell.cellFull)
                {
                    number++;
                }
            }

            if (number == 0)
            {
                TakeAllCards();
            }

            
        }
        else
        {
            TakeAllCardsOtherTurns();
        }
        TurnManager.Instance.SetState(TurnManager.GameState.ActionPhase);
    }

    public void TakeAllCardsOtherTurns()
    {
        Character[] enemyCards = Resources.LoadAll<Character>("Cards");


        for (int i = 0; i < 4; i++)
        {
            if (Random.Range(0, 3) % 2 == 1)
            {
                GridCell cell = GridManager.Instance.gridCells[1, i].GetComponent<GridCell>();
                if (!cell.cellFull)
                {
                    Vector2 targetPos = new Vector2(1, i);
                    int rand = Random.Range(0, enemyCards.Length - 1);
                    GridManager.Instance.AddObjectToGrid(enemyCards[rand].prefab, targetPos);
                    cell.objectInCell.GetComponent<CharacterStats>().characterStartData = enemyCards[rand];
                    cell.objectInCell.transform.localScale = new Vector3(-cell.objectInCell.transform.localScale.x,
                        cell.objectInCell.transform.localScale.y, cell.objectInCell.transform.localScale.z);
                }
            }
        }
    }
}