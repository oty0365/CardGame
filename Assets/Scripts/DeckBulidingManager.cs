using System.Threading.Tasks;
using UnityEngine;

public class DeckBulidingManager : MonoBehaviour
{
    public static DeckBulidingManager Instance;
    [SerializeField] private GameObject deckBulidingPannel;
    [SerializeField] private CardInventory cardInventory;
    [SerializeField] private DeckInventory deckInventory;
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

   public void OnPannelActived()
   {
        OnPannelActivedAsync();     
   }
    public async void OnPannelActivedAsync()
    {
        deckBulidingPannel.SetActive(true);
        await cardInventory.OnEnableCardLoad();
        await deckInventory.OnEnableDecks();
        deckInventory.InitializeDeckInventory();
        UiCard.UpdateInventroyInDeck.Invoke();
        UiCard.CheckAllCardsInDeck.Invoke();
    }

}
