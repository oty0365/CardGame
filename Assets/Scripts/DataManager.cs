using System.Collections.Generic;
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
                Debug.Log("�÷��̾� �� �ҷ����� ����!");
                PlayerInfo.Instance.playerDeck = playerDeck.Value.GetAs<Dictionary<string, int>>();
            }
        }
        catch (System.Exception ex)
        {
            Debug.LogError($"�÷��̾� �� �ҷ����� �� ���� �߻�: {ex.Message}");
        }
    }

    public async Task LoadPlayerCards()
    {
        try
        {
            var data = await CloudSaveService.Instance.Data.Player.LoadAsync(new HashSet<string> { "PlayerCards" });
            if (data.TryGetValue("PlayerCards", out var playerDeck))
            {
                Debug.Log("�÷��̾� ī�� �ҷ����� ����!");
                PlayerInfo.Instance.playerCards = playerDeck.Value.GetAs<Dictionary<string, int>>();
            }
        }
        catch (System.Exception ex)
        {
            Debug.LogError($"�÷��̾� �� �ҷ����� �� ���� �߻�: {ex.Message}");
        }
    }
}
