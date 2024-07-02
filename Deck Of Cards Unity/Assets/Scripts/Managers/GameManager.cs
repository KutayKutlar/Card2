using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField]private int playerHeroHealth;
    [SerializeField]private int enemyHeroHealth;
    [SerializeField] private TextMeshProUGUI playerHealthText;
    [SerializeField] private TextMeshProUGUI enemyHealthText;
    private int playerXP;
    private int difficulty = 5;

    public OptionsManager OptionsManager { get; private set; }
    public AudioManager AudioManager { get; private set; }

    public DeckManager DeckManager
    {
        get
        {
            return DeckManager.Instance;
        } 
    }

    public bool PlayingCard = false;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }

        Instance = this;
    }
    

    private void InitializeManagers()
    {
        OptionsManager = GetComponentInChildren<OptionsManager>();
        AudioManager = GetComponentInChildren<AudioManager>();
        //DeckManager = GetComponentInChildren<DeckManager>();

        if (OptionsManager == null)
        {
            GameObject prefab = Resources.Load<GameObject>("Prefabs/OptionsManager");
            if (prefab == null)
            {
                Debug.Log($"OptionsManager prefab not found");
            }
            else
            {
                Instantiate(prefab, transform.position, Quaternion.identity, transform);
                OptionsManager = GetComponentInChildren<OptionsManager>();
            }
        }
        if (AudioManager == null)
        {
            GameObject prefab = Resources.Load<GameObject>("Prefabs/AudioManager");
            if (prefab == null)
            {
                Debug.Log($"AudioManager prefab not found");
            }
            else
            {
                Instantiate(prefab, transform.position, Quaternion.identity, transform);
                AudioManager = GetComponentInChildren<AudioManager>();
            }
        }
        
        /*if (DeckManager == null)
        {
            GameObject prefab = Resources.Load<GameObject>("Prefabs/DeckManager");
            if (prefab == null)
            {
                Debug.Log($"DeckManager prefab not found");
            }
            else
            {
                Instantiate(prefab, transform.position, Quaternion.identity, transform);
                DeckManager = GetComponentInChildren<DeckManager>();
            }
        }*/
    }

    public int PlayerHeroHealth
    {
        get { return playerHeroHealth; }
        set
        {
            playerHeroHealth = value;
            UpdatePlayerHealthDisplay();
        }
    }

    public int EnemyHeroHealth
    {
        get { return enemyHeroHealth; }
        set
        {
            enemyHeroHealth = value;
            UpdateEnemyHealthDisplay();
        }
    }

    private void Start()
    {
        // Initial display update
        UpdatePlayerHealthDisplay();
        UpdateEnemyHealthDisplay();
        
    }

    private void UpdatePlayerHealthDisplay()
    {
        playerHealthText.text = "Player Health: " + playerHeroHealth.ToString();
    }

    private void UpdateEnemyHealthDisplay()
    {
        enemyHealthText.text = "Enemy Health: " + enemyHeroHealth.ToString();
    }

    public int PlayerXP
    {
        get { return playerXP; }
        set { playerXP = value; }
    }

    public int Difficulty
    {
        get { return difficulty; }
        set { difficulty = value; }
    }
}
