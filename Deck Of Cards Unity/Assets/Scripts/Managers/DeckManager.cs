using System.Collections.Generic;
using SinuousProductions;
using UnityEngine;

public class DeckManager : MonoBehaviour
{
    public List<Card> allCards = new List<Card>();

    public int startingHandSize = 6;
    public int maxHandSize = 12;
    private HandManager handManager;
    private DrawPileManager drawPileManager;
    public bool startBattleRun = true;
    public static DeckManager Instance;
    
    
    void Start()
    {
        //Load all card assets from the Resources folder
        Card[] cards = Resources.LoadAll<Card>("Cards");

        //Add the loaded cards to the allCards list
        allCards.AddRange(cards);
    }

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        Instance = this;
        if (drawPileManager == null)
        {
            drawPileManager = FindObjectOfType<DrawPileManager>();
        }
        if (handManager == null)
        {
            handManager = FindObjectOfType<HandManager>();
        }
    }

    void Update()
    {
        if (startBattleRun)
        {
            BattleSetup();
        }
    }

    public void BattleSetup()
    {
        handManager.BattleSetup(maxHandSize);
        drawPileManager.MakeDrawPile(allCards);
        drawPileManager.BattleSetup(startingHandSize, maxHandSize);
        startBattleRun = false;
    }
}
