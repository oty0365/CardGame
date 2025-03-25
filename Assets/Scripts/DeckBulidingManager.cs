using System;
using System.Threading.Tasks;
using UnityEngine;

public class DeckBulidingManager : MonoBehaviour
{
    public static DeckBulidingManager Instance;
    [SerializeField] private GameObject deckBulidingPannel;
    [SerializeField] private CardInventory cardInventory;
    [SerializeField] private DeckInventory deckInventory;
    public RectTransform canvasReck;
    //[SerializeField] private UiCard uiCard;

    void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        deckBulidingPannel.SetActive(false);
    }

    void Update()
    {

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
                deckInventory.InitializeDeckInventory();
                UiCard.UpdateInventroyInDeck?.Invoke();
                UiCard.CheckAllCardsInDeck?.Invoke();
                UiCard.InitInventroyInDeck?.Invoke();
                MainScreenManager.Instance.EndLoadingPannel();

        }
            catch (Exception ex)
            {
                Debug.LogError($"패널 활성화 중 오류 발생: {ex.Message}");
            }
    }
    public void OnPannelClose()
    {
        deckBulidingPannel.SetActive(false);
    }

}
