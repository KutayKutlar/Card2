using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using NaughtyAttributes;
using Random = UnityEngine.Random;

public class FightManager : MonoBehaviour
{
    public static FightManager instance;
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        instance = this;
    }

    private void Start()
    {
        GameManager.Instance.PlayerHeroHealth = 100;
        GameManager.Instance.EnemyHeroHealth = 30;
    }

    [Button]
    private void StartFight()
    {
        
        StartCoroutine(Fight());
        
    }
    public IEnumerator Fight()
    {
        for (int i = 0; i < 4; i++)
        {
            GridCell playerCell = GridManager.Instance.gridCells[0, i].gameObject.GetComponent<GridCell>();
            GridCell enemyCell = GridManager.Instance.gridCells[1, i].gameObject.GetComponent<GridCell>();
            CharacterStats playerChar = null;
            CharacterStats enemyChar = null;

            if (playerCell.objectInCell != null)
            {
                playerChar = playerCell.objectInCell.GetComponent<CharacterStats>();
            }
            if (enemyCell.objectInCell != null)
            {
                enemyChar = enemyCell.objectInCell.GetComponent<CharacterStats>();
            }
            if (playerCell.cellFull && enemyCell.cellFull)
            {
                yield return Attack(playerChar,enemyChar);
                if (!enemyChar.isDead)
                {
                    yield return Attack(enemyChar,playerChar,-1);
                    if (playerChar.isDead)
                    {
                        GridManager.Instance.ClearGrid(playerCell);
                        Destroy(playerChar.gameObject,2);
                    }
                }
                else
                {
                    GridManager.Instance.ClearGrid(enemyCell);
                    Destroy(enemyChar.gameObject,2);
                }
                
            }
            else if (playerCell.cellFull || enemyCell.cellFull)
            {
                if (playerCell.cellFull)
                {
                    yield return AttackOnHero(playerChar);
                }
                else
                {
                    yield return AttackOnHero(enemyChar,-1);
                }

                if (GameManager.Instance.PlayerHeroHealth <= 0 || GameManager.Instance.EnemyHeroHealth <= 0 )
                {
                    TurnManager.Instance.SetState(TurnManager.GameState.ResultPhase);
                    yield break;
                }
            }

            
        }
        TurnManager.Instance.SetState(TurnManager.GameState.PreparationPhase);
        yield return null;
    }
   
    public IEnumerator Attack(CharacterStats attacker,CharacterStats enemyTarget,int num =1)
    {
        Sequence hitSequence = DOTween.Sequence();
        hitSequence.Append(attacker.transform.DOMoveX(num*0.75f, 0.25f).SetRelative());
        hitSequence.Insert(0.2f, enemyTarget.transform.DOMoveX(num*0.3f, 0.1f).SetRelative());
        hitSequence.InsertCallback(0.2f,() => enemyTarget.health -= Random.Range(attacker.damageMin,attacker.damageMax+1));
        hitSequence.Append(attacker.transform.DOMoveX(num*-0.75f, 0.25f).SetRelative());
        hitSequence.Insert(0.30f, enemyTarget.transform.DOMoveX(num*-0.3f, 0.1f).SetRelative());
        hitSequence.AppendCallback(() =>
        {
            if (enemyTarget.isDead)
            {
                enemyTarget.transform.DOScale(0, 0.25f).SetEase(Ease.InCubic);
            }
        });
        yield return hitSequence.WaitForCompletion();
    }
    public IEnumerator AttackOnHero(CharacterStats attacker,int num =1)
    {
        Sequence hitSequence = DOTween.Sequence();
        hitSequence.Append(attacker.transform.DOMoveX(num*0.75f, 0.25f).SetRelative());
        if (num == 1)
        {
            GameManager.Instance.EnemyHeroHealth -= Random.Range(attacker.damageMin, attacker.damageMax + 1);
        }
        else
        {
            GameManager.Instance.PlayerHeroHealth -= Random.Range(attacker.damageMin, attacker.damageMax + 1);
        }
        hitSequence.Append(attacker.transform.DOMoveX(num*-0.75f, 0.25f).SetRelative());
        
        yield return hitSequence.WaitForCompletion();
    }
}
