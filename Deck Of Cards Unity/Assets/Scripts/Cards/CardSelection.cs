using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class CardSelection : MonoBehaviour
{
    public GameObject card1;
    public GameObject card2;
    public GameObject card3;
    public Transform panel;
    public Image cardImage;
    public TextMeshProUGUI damageTextt;
    public TextMeshProUGUI healthTextt;
    public TextMeshProUGUI nameTextt;

    public Image cardImage2;
    public TextMeshProUGUI damageText2;
    public TextMeshProUGUI healthText2;
    public TextMeshProUGUI nameText2;

    public Image cardImage3;
    public TextMeshProUGUI damageText3;
    public TextMeshProUGUI healthText3;
    public TextMeshProUGUI nameText3;

    public static CardSelection Instance;
    public Transform cardAdded;

    private void Awake()
    {
        Instance = this;
    }

    public void ShowCards()
    {
        List<Character> enemyCards = new List<Character>();
        
        Character[] loadedCards = Resources.LoadAll<Character>("Cards");


        enemyCards.AddRange(loadedCards);

        int rand = Random.Range(0, enemyCards.Count - 1);
        enemyCards.RemoveAt(rand);
        healthTextt.text = "Health: " + enemyCards[rand].health.ToString();
        damageTextt.text = "Damage: " + enemyCards[rand].damageMax.ToString();
        nameTextt.text = enemyCards[rand].name.ToString();
        cardImage.sprite = enemyCards[rand].cardSprite;

        int rand2 = Random.Range(0, enemyCards.Count - 1);
        enemyCards.RemoveAt(rand2);
        

        healthText2.text = "Health: " + enemyCards[rand2].health.ToString();
        damageText2.text = "Damage: " + enemyCards[rand2].damageMax.ToString();
        nameText2.text = enemyCards[rand2].name.ToString();
        cardImage2.sprite = enemyCards[rand2].cardSprite;
        int rand3 = Random.Range(0, enemyCards.Count - 1);
        enemyCards.RemoveAt(rand3);
        

        healthText3.text = "Health: " + enemyCards[rand3].health.ToString();
        damageText3.text = "Damage: " + enemyCards[rand3].damageMax.ToString();
        nameText3.text = enemyCards[rand3].name.ToString();
        cardImage3.sprite = enemyCards[rand3].cardSprite;
    }
}