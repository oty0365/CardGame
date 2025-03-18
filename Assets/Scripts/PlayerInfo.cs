using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;



public class PlayerInfo : MonoBehaviour
{
    public static PlayerInfo Instance;
    [Header("UI")]
    [SerializeField] private TextMeshProUGUI nameTmp;
    [SerializeField] private TextMeshProUGUI goldTmp;
    [Header("플레이어 정보")]
    private string _playerName;
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
                //UpdateUI(goldTmp, _stage.ToString());
            }
        }
    }
    public Dictionary<string, int> playerDeck = new Dictionary<string, int>();
    public Dictionary<string, int> playerCards = new Dictionary<string, int>();

    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        
    }


    void Update()
    {
        
    }
    public void UpdateUI(object frame, string value)
    {
        if(frame is TextMeshProUGUI tmp)
        {
            tmp.text = value;   
        }
        else if(frame is Slider slider)
        {
            slider.value = float.Parse(value);
        }
    }

}
