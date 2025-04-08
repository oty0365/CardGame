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

    public static int IsCustomizing;
    //public static CardScriptableObject HoverdCard;
    public static Action CheckAllCardsInDeck;
    public static Action UpdateInventroyInDeck;
    public static Action InitInventroyInDeck;

    public static RectTransform InstantinatedObj;
    private void Awake()
    {

    }

    private void Update()
    {
        if(InstantinatedObj == null)
        {
            isMoving = false;
        }
        if (isMoving)
        {
            Vector2 localPoint;
            if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
                DeckBulidingManager.Instance.canvasReck,
                Input.mousePosition,
                null,
                out localPoint
            ))
            {
                var additionalPos = ScreenManager.Instance.scaler.referenceResolution;
                var newPoint = new Vector2(localPoint.x + (additionalPos.x/2), localPoint.y +(additionalPos.y/2) );
                InstantinatedObj.anchoredPosition = newPoint;
            }
        }

    }

    void Start()
    {
        if (InstantinatedObj==null || InstantinatedObj.gameObject != gameObject)
        {
            CheckAllCardsInDeck += CheckDeck;
            UpdateInventroyInDeck += UpdateInventory;
            InitInventroyInDeck += InitCard;
            InitCard();
        }
        //Debug.Log(CheckAllCardsInDeck.GetInvocationList().Length);
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
    }
    public void OnDestroy()
    {
        CheckAllCardsInDeck -= CheckDeck;
        UpdateInventroyInDeck -= UpdateInventory;
        InitInventroyInDeck -= InitCard;
    }
    public void OnStartDrag()
    {
        if (IsCustomizing==0)
        {
            IsCustomizing = isInDeck ? 1 : 2;
            InstantinatedObj = Instantiate(gameObject, DeckBulidingManager.Instance.canvasReck).GetComponent<RectTransform>();
            var c = InstantinatedObj.GetComponent<UiCard>();
            c.currentCardFrame.raycastTarget = false;
            c.cardImage.raycastTarget = false;
            InstantinatedObj.GetComponent<Image>().raycastTarget = false;
            c.card = card;
            c.UpdateCard();
            InstantinatedObj.localScale = new Vector3(4.5f, 4.5f, 1);
            InstantinatedObj.SetParent(DeckBulidingManager.Instance.canvasReck, false);
            InstantinatedObj.SetAsLastSibling();

            Vector2 localPoint;
            if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
                DeckBulidingManager.Instance.canvasReck,
                Input.mousePosition,
                null,
                out localPoint
            ))
            {
                InstantinatedObj.anchoredPosition = localPoint;
            }

            DeckBulidingManager.Instance.OnDragingCard(IsCustomizing);
            isMoving = true;
            Debug.Log(InstantinatedObj.GetComponent<UiCard>().card.CardCode);
        }
    }
    

    public void CheckDeck()
    {
        if (isInDeck)
        {
            var index = transform.GetSiblingIndex();
            if (PlayerInfo.Instance.cardsInDeck[index].card != null && PlayerInfo.Instance.cardsInDeck[index].count>0)
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
                gameObject.SetActive(true);
                cardCount.text = value.ToString();
            }
            else
            {
                gameObject.SetActive(false);
            }
        }
    }
    
}
