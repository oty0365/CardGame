using ScriptableObject;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class DeckCardSet
{
    public CardScriptableObject card;
    public int count;
}

public class PlayerInfo : MonoBehaviour
{
    public static PlayerInfo Instance;
    [Header("UI")]
    [SerializeField] private TextMeshProUGUI nameTmp;
    [SerializeField] private TextMeshProUGUI goldTmp;
    [Header("플레이어 정보")]
    private string _playerName;
    [Header("덱 최대 매수")]
    public int count;

    private int _gold;
    private int _stage;
    public string PlayerName {
        get => _playerName;
        set
        {
            if (_playerName != value)
            {
                _playerName = value;
                UpdateUI(nameTmp, _playerName);
            }
        } 
    }
    public int Gold {
        get => _gold;
        set
        {
            if ( _gold != value)
            {
                _gold = value;
                UpdateUI(goldTmp, _gold.ToString());
            }
        }
    }
    public int Stage 
    {
        get => _stage;
        set
        {
            if (_stage != value)
            {
                _stage = value;
            }
        }
    }
    public Dictionary<string, int> playerDeck = new Dictionary<string, int>();
    public Dictionary<string, int> playerCards = new Dictionary<string, int>();
    public DeckCardSet[] cardsInDeck;



    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        cardsInDeck = new DeckCardSet[count];
    }

    void Update()
    {
        
    }
        
        
    public void UpdateUI(TextMeshProUGUI frame, string value)
    {
        frame.text = value;
    }
    public void UpdateUI(Slider frame, string value)
    {
        frame.value = float.Parse(value);
    }
}
