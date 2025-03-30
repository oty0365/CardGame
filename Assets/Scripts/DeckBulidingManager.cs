using System;
using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class DeckBulidingManager : MonoBehaviour
{
    public static DeckBulidingManager Instance;
    [SerializeField] private GameObject deckBulidingPannel;
    [SerializeField] private CardInventory cardInventory;
    [SerializeField] private DeckInventory deckInventory;
    public RectTransform canvasReck;

    [Header("카드 추가 시 등장하는 패널")]
    [SerializeField] private GameObject addToDeckPannel;
    [SerializeField] private Image addToDeckAdder;
    [SerializeField] private GameObject addToInvenPannel;
    [SerializeField] private Image addToInvenAdder;
    //[SerializeField] private UiCard uiCard;

    void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        deckBulidingPannel.SetActive(false);
        addToDeckPannel.SetActive(false);
        addToInvenPannel.SetActive(false);
    }

    void Update()
    {
       
    }
    public void CardsUpdate()
    {
        deckInventory.InitializeDeckInventory();
        UiCard.UpdateInventroyInDeck?.Invoke();
        UiCard.CheckAllCardsInDeck?.Invoke();
        UiCard.InitInventroyInDeck?.Invoke();
        MainScreenManager.Instance.EndLoadingPannel();
    }
  
   public async void OnPannelActived()
   {
        MainScreenManager.Instance.StartLoadingPannel();
        await OnPannelActivedAsync();     
   }
    public async Task OnPannelActivedAsync()
    {
            try
            {
                deckBulidingPannel.SetActive(true);
                await cardInventory.OnEnableCardLoad();
                await deckInventory.OnEnableDecks();
                CardsUpdate();
            }
            catch (Exception ex)
            {
                Debug.LogError($"패널 활성화 중 오류 발생: {ex.Message}");
            }
    }
    public void OnPannelClose()
    {
        if (UiCard.IsCustomizing ==0)
        {
            deckBulidingPannel.SetActive(false);
        }

    }
    public void OnDragingCard(int mode)
    {
        
        if (mode == 2)
        {
            addToDeckPannel.SetActive(true);
            addToDeckAdder.color = Color.white;
        }
        else if(mode == 1)
        {
            addToInvenPannel.SetActive(true);
            addToInvenAdder.color = Color.white;
        }
    }
    public void OnHoveringCardToPoint()
    {
        int mode = UiCard.IsCustomizing;
        if (mode == 2)
        {
            addToDeckAdder.color = Color.black;
        }
        else if(mode == 1)
        {
            addToInvenAdder.color = Color.black;
        }
    }
    public void ExitHoveringCardToPoint()
    {
        int mode = UiCard.IsCustomizing;
        if (mode==2)
        {
            addToDeckAdder.color = Color.white;
        }
        else if(mode == 1)
        {
            addToInvenAdder.color = Color.white;
        }
    }
    public void OnClickWhileMovingCard()
    {
        addToDeckPannel.SetActive(false);
        addToInvenPannel.SetActive(false);
        int mode = UiCard.IsCustomizing;
        var cardCode = UiCard.InstantinatedObj.gameObject.GetComponent<UiCard>().card.CardCode;
        Debug.Log(cardCode);
        foreach (var i in PlayerInfo.Instance.playerCards)
        {
            Debug.Log(i);
        }
        if (mode == 2)
        {
            Debug.Log(2);
            Debug.Log(cardCode);

            foreach (var i in PlayerInfo.Instance.playerCards)
            {
                Debug.Log($"카드 코드: {i.Key}, 개수: {i.Value}");
            }
            foreach (DictionaryEntry i in PlayerInfo.Instance.playerDeck)
            {
                Debug.Log($"덱 카드 코드: {i.Key}, 개수: {i.Value}");
            }

            /*if (PlayerInfo.Instance.playerDeck.ContainsKey(cardCode) && PlayerInfo.Instance.playerDeck[cardCode] > 0)
            {
            }*/
                PlayerInfo.Instance.playerCards[cardCode] -= 1;
                if (PlayerInfo.Instance.playerCards[cardCode] == 0)
                {
                    PlayerInfo.Instance.playerCards.Remove(cardCode);
                }
                if (PlayerInfo.Instance.playerDeck.Contains(cardCode))
                {
                    PlayerInfo.Instance.playerDeck[cardCode] = (int)PlayerInfo.Instance.playerDeck[cardCode] + 1;
                }
                else
                {
                    PlayerInfo.Instance.playerDeck[cardCode] = 1;
                    Debug.Log(cardCode);
                }

            /*else
            {
                Debug.Log("불가능: 덱에 해당 카드가 없음 또는 개수가 0임");
            }*/

            CardsUpdate();
        }

        else if (mode == 1)
        {
            Debug.Log(1);
            if (!PlayerInfo.Instance.playerDeck.Contains(cardCode))
            {
                Debug.Log("불가능!");
                return;
            }

            if (PlayerInfo.Instance.playerDeck.Contains(cardCode)) 
            {
                int deckValue = (int)PlayerInfo.Instance.playerDeck[cardCode]; 

                if (deckValue - 1 < 0)
                {
                    Debug.Log("불가능!");
                    return;
                }


                PlayerInfo.Instance.playerDeck[cardCode] = deckValue - 1;
                if ((int)PlayerInfo.Instance.playerDeck[cardCode] == 0)
                {
                    PlayerInfo.Instance.playerDeck.Remove(cardCode);
                }

                    if (PlayerInfo.Instance.playerCards.TryGetValue(cardCode, out int cardValue))
                 {
                    if (cardValue + 1 > 3)
                    {
                        Debug.Log("불가능");
                    }
                    else
                    {
                        PlayerInfo.Instance.playerCards[cardCode] = cardValue + 1;
                        Debug.Log(PlayerInfo.Instance.playerCards[cardCode]);
                    }
                }

                else
                {
                    PlayerInfo.Instance.playerCards.Add(cardCode, 1);
                }
            }
            CardsUpdate();
        }

        Destroy(UiCard.InstantinatedObj.gameObject);
        UiCard.InstantinatedObj = null;
        UiCard.IsCustomizing = 0;
    }

}
