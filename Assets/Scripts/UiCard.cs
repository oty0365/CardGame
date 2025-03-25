using ScriptableObject;
using System;
using System.Linq;
using System.Xml.Schema;
using TMPro;
using UnityEditor.Build.Content;
using UnityEngine;
using UnityEngine.UI;

public class UiCard : MonoBehaviour
{
    [Header("카드 프레임")]
    [SerializeField] private Sprite[] cardFrame;

    [Header("카드 프리팹")]
    public CardScriptableObject card;

    [Header("카드형식")]
    public Image currentCardFrame;
    [SerializeField] private TextMeshProUGUI cardName;
    [SerializeField] private TextMeshProUGUI cardCoast;
    [SerializeField] private TextMeshProUGUI cardDesc;
    [SerializeField] private TextMeshProUGUI cardCount;
    [SerializeField] private TextMeshProUGUI damage;
    [SerializeField] private TextMeshProUGUI hp;
    [SerializeField] private Image cardImage;

    [Header("덱전용 카드인지 확인")]
    public bool isInDeck;

    [Header("이카드가 이동중인지 확인")]
    public bool isMoving;

    public static bool IsCustomizing;
    //public static CardScriptableObject HoverdCard;
    public static Action CheckAllCardsInDeck;
    public static Action UpdateInventroyInDeck;
    public static Action InitInventroyInDeck;

    private RectTransform _instantinatedObj;
    private void Awake()
    {

    }

    private void Update()
    {
        if (isMoving)
        {
            Vector2 localPoint;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                DeckBulidingManager.Instance.canvasReck,
                Input.mousePosition,
                Camera.main,
                out localPoint
            );
            _instantinatedObj.anchoredPosition = Input.mousePosition;
            //Debug.Log(_instantinatedObj);
            //_instantinatedObj.anchoredPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        }
    }

    void Start()
    {
        CheckAllCardsInDeck += CheckDeck;
        UpdateInventroyInDeck += UpdateInventory;
        InitInventroyInDeck += InitCard;
        Debug.Log(CheckAllCardsInDeck.GetInvocationList().Length);
        IsCustomizing = false;
        InitCard();
        /*if (!isInDeck)
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
        }*/
    }
    public void OnClick()
    {
        CardDescriptionPannel.Instance.gameObject.SetActive(true);
        CardDescriptionPannel.Instance.PutCardInfo(card);
        /*if (!IsCustomizing)
        {
            IsCustomizing = true;
            _instantinatedObj = Instantiate(gameObject, DeckBulidingManager.Instance.canvasReck).GetComponent<RectTransform>();
            _instantinatedObj.GetComponent<UiCard>().UpdateCard();
            _instantinatedObj.localScale = new Vector3(4, 4, 1);
            _instantinatedObj.SetParent(DeckBulidingManager.Instance.canvasReck, false);
            _instantinatedObj.SetAsLastSibling();
            isMoving = true;
        }*/
    }
    public void OnHover()
    {
;       Debug.Log("!");
    }
    

    public void CheckDeck()
    {
        if (isInDeck)
        {
            var index = transform.GetSiblingIndex();
            if (PlayerInfo.Instance.cardsInDeck[index].card != null)
            {
                gameObject.SetActive(true);
                card = PlayerInfo.Instance.cardsInDeck[index].card;
                UpdateCard();
                cardCount.text = PlayerInfo.Instance.cardsInDeck[index].count.ToString();
            }
            else
            {
                gameObject.SetActive(false);
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
        cardName.text = card.CardName;
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
    public void InitCard()
    {
        if (!isInDeck)
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
    }
    
}
