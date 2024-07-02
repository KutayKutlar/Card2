using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
public class ButtonManager : MonoBehaviour
{
    
    public void EndTurnButton()
    {
        EnemyDeckManager.Instance.TakeAllCards();
        EnemyDeckManager.Instance.isFirstTurn = false;
    }
    public void TryAgainButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        DeckManager.Instance.startBattleRun = true;
    }
    public void CardSelectButton()
    {
        CardSelection.Instance.card1.transform.DOScale(0, 0.5f);
        CardSelection.Instance.card2.transform.DOScale(0, 0.5f);
        CardSelection.Instance.card3.transform.DOScale(0, 0.5f);
        CardSelection.Instance.cardAdded.DOScale(1, 1).OnComplete(()=> 
            CardSelection.Instance.panel.DOScale(0,0.2f).OnComplete(()=>
                CardSelection.Instance.panel.gameObject.SetActive(false)).OnComplete(()=>CardSelection.Instance.nameText2.transform.DOScale(0,0.5f).OnComplete(()=> TryAgainButton())));
        DeckManager.Instance.startBattleRun = true;
    }
}
