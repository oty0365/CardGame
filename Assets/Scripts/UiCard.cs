using ScriptableObject;
using System;
using System.Linq;
using System.Xml.Schema;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UiCard : MonoBehaviour
{
    [Header("ī�� ������")]
    [SerializeField] private Sprite[] cardFrame;
    [Header("ī�� ������")]
    public CardScriptableObject card;
    [Header("ī������")]
    public Image currentCardFrame;
    [SerializeField] private TextMeshProUGUI cardName;
    [SerializeField] private TextMeshProUGUI cardCoast;
    [SerializeField] private TextMeshProUGUI cardDesc;
    [SerializeField] private TextMeshProUGUI cardCount;
    [SerializeField] private TextMeshProUGUI damage;
    [SerializeField] private TextMeshProUGUI hp;
    [SerializeField] private Image cardImage;
    [Header("������ ī������ Ȯ��")]
    public bool isInDeck;

    public static Action CheckAllCardsInDeck;
    public static Action UpdateInventroyInDeck;

    private void Awake()
    {
        CheckAllCardsInDeck = CheckDeck;
        UpdateInventroyInDeck= UpdateInventory;
    }
    void Start()
    {
        if(!isInDeck)
        {
            var index = transform.GetSiblingIndex();
            card = CardManager.Instance.cardSets.CardList[index];
            cardName.text = card.CardName;
            UpdateCard();
            if (PlayerInfo.Instance.playerCards.TryGetValue(card.CardCode, out int value))
            {
                cardCount.text = value.ToString();
            }
            else
            {
                gameObject.SetActive(false);
            }
        }
        CheckDeck();
    }

    public void CheckDeck()
    {
        
        if (isInDeck)
        {
            var index = transform.GetSiblingIndex();
            if (PlayerInfo.Instance.cardsInDeck[index].card != null)
            {
                Debug.Log(index);
                card = PlayerInfo.Instance.cardsInDeck[index].card;
                UpdateCard();
                cardCount.text = PlayerInfo.Instance.cardsInDeck[index].count.ToString();
            }
        }
    }

    public void UpdateInventory()
    {
        if (!isInDeck)
        {
            UpdateCard();
        }
    }
    public void UpdateCard()
    {
        Debug.Log("!");
        switch (card.CardType)
        {
            case _CardType.Monster:
                currentCardFrame.sprite = cardFrame[0];

                cardCoast.text = card.Cost.ToString();
                damage.text = card.Damage.ToString();
                hp.text = card.Health.ToString();
                cardDesc.text = card.MonsterEffects.ToString();
                cardImage.sprite = card.Sprite;
                break;
            case _CardType.Magic:
                currentCardFrame.sprite = cardFrame[1];
                cardCoast.text = card.Cost.ToString();
                cardDesc.text = card.MagicEffects.ToString();
                cardImage.sprite = card.Sprite;
                break;
            case _CardType.Passive:
                currentCardFrame.sprite = cardFrame[2];
                cardDesc.text = card.PassiveEffects.ToString();
                cardImage.sprite = card.Sprite;
                break;
        }
    }
    
}
