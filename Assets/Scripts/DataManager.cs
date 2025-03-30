using Newtonsoft.Json;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Threading.Tasks;
using Unity.Services.CloudSave;
using Unity.Services.Core;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static DataManager Instance {get; private set; }
    private void Awake()
    {
        Instance = this;
    }
    async void Start()
    {
        await UnityServices.InitializeAsync();
    }
    void Update()
    {

    }
    public async Task LoadPlayerDeck()
    {
        try
        {
            var data = await CloudSaveService.Instance.Data.Player.LoadAsync(new HashSet<string> { "PlayerDeck" });
            if (data.TryGetValue("PlayerDeck", out var playerDeck))
            {
                Debug.Log("플레이어 덱 불러오기 성공!");
                var deckJson = playerDeck.Value.GetAs<string>();
                PlayerInfo.Instance.playerDeck = new OrderedDictionary();
                List<KeyValuePair<string, int>> deckList = JsonConvert.DeserializeObject<List<KeyValuePair<string, int>>>(deckJson);
                PlayerInfo.Instance.playerDeck = new OrderedDictionary();
                foreach (var item in deckList)
                {
                    PlayerInfo.Instance.playerDeck.Add(item.Key, item.Value);
                }

            }
        }
        catch (System.Exception ex)
        {
            Debug.LogError($"플레이어 덱 불러오기 중 오류 발생: {ex.Message}");
        }
    }

    public async Task LoadPlayerCards()
    {
        try
        {
            var data = await CloudSaveService.Instance.Data.Player.LoadAsync(new HashSet<string> { "PlayerCards" });
            if (data.TryGetValue("PlayerCards", out var playerDeck))
            {
                Debug.Log("플레이어 카드 불러오기 성공!");
                PlayerInfo.Instance.playerCards = playerDeck.Value.GetAs<Dictionary<string, int>>();
            }
        }
        catch (System.Exception ex)
        {
            Debug.LogError($"플레이어 덱 불러오기 중 오류 발생: {ex.Message}");
        }
    }

    /*public async Task SavePlayerDeck(Dictionary<string,int> deck)
    {
        try
        {
            var data = await CloudSaveService.Instance.Data.Player.LoadAsync(new HashSet<string> { "PlayerDeck" });
            if (data.TryGetValue("PlayerDeck", out var playerDeck))
            {
                Debug.Log("플레이어 덱 불러오기 성공!");
                PlayerInfo.Instance.playerDeck = playerDeck.Value.GetAs<Dictionary<string, int>>();
            }
        }
        catch (System.Exception ex)
        {
            Debug.LogError($"플레이어 덱 불러오기 중 오류 발생: {ex.Message}");
        }
    }*/
}
