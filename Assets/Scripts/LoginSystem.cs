using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.Services.Authentication;
using Unity.Services.Authentication.Components;
using Unity.Services.Authentication.PlayerAccounts;
using Unity.Services.CloudSave;
using Unity.Services.Core;
using Unity.Services.Multiplayer;
using UnityEngine;
using UnityEngine.UI;

public class LoginSystem : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private GameObject nameInputPanel;
    [SerializeField] private TMPro.TMP_InputField nameInput;
    [SerializeField] private Button submitNameButton;
    [SerializeField] private GameObject deckSelectPannel;
    [SerializeField] private GameObject deckConfrimPannel;
    [SerializeField] private Button deckConfrimButton;
    [SerializeField] private DeckSets[] fristDeck;
    [SerializeField] private GameObject mainScreenPannel;

    private int deckMode=0;

    private async void Start()
    {
        
        await UnityServices.InitializeAsync();
        nameInputPanel.SetActive(false);
        deckSelectPannel.SetActive(false);
        deckConfrimPannel.SetActive(false);
        mainScreenPannel.SetActive(false);
        PlayerAccountService.Instance.SignedIn -= SignInWithUnity;
        PlayerAccountService.Instance.SignedIn += SignInWithUnity;
    }
    async Task SignInWithUnityAsync(string accessToken)
    {
        try
        {
            await AuthenticationService.Instance.SignInWithUnityAsync(accessToken);
            Debug.Log("SignIn is successful.");
            await CheckPlayerName();
        }
        catch (AuthenticationException ex)
        {
            Debug.LogException(ex);
        }
        catch (RequestFailedException ex)
        {
            Debug.LogException(ex);
        }
    }
    private async void SignInWithUnity()
    {
        try
        {

            var accessToken = PlayerAccountService.Instance.AccessToken;
            Debug.Log(accessToken);
            await SignInWithUnityAsync(accessToken);

        }
        catch
        {
            Debug.Log(":(");
        }
    }
    public void UnityLogined()
    {
        PlayerAccountService.Instance.StartSignInAsync();

    }
    private async Task CheckPlayerDeck()
    {
        try
        {
            var data = await CloudSaveService.Instance.Data.Player.LoadAsync(new HashSet<string> { "PlayerDeck" ,"PlayerCards"});
            if (data.TryGetValue("PlayerDeck", out var playerDeck))
            {
                Debug.Log("플레이어 덱 불러오기 성공!");
                mainScreenPannel.SetActive(true);
                PlayerInfo.Instance.playerDeck =  playerDeck.Value.GetAs<Dictionary<string, int>>();
                data.TryGetValue("PlayerCards", out var playerCards);
                PlayerInfo.Instance.playerCards = playerCards.Value.GetAs<Dictionary<string, int>>();
            }
            else
            {
                ShowDeckSelectPannel();
            }
        }
        catch (System.Exception ex)
        {
            Debug.LogError($"플레이어 덱 불러오기 중 오류 발생: {ex.Message}");
        }
    }
    private async Task CheckPlayerName()
    {
        
        try
        {
            var data = await CloudSaveService.Instance.Data.Player.LoadAsync(new HashSet<string> { "PlayerName","Gold","Stage" });

            if (data.TryGetValue("PlayerName", out var playerName))
            {
                PlayerInfo.Instance.PlayerName = playerName.Value.GetAs<string>();
                data.TryGetValue("Gold", out var gold);
                data.TryGetValue("Stage", out var stage);
                PlayerInfo.Instance.Gold = gold.Value.GetAs<int>();
                PlayerInfo.Instance.Stage = stage.Value.GetAs<int>();
                await CheckPlayerDeck();
                Debug.Log($"플레이어 이름: {PlayerInfo.Instance.PlayerName}");

            }
            else
            {
                Debug.Log("플레이어 이름이 설정되지 않았습니다.");
                ShowNameInputPanel();
            }
        }
        catch (System.Exception ex)
        {
            Debug.LogError($"플레이어 이름 확인 중 오류 발생: {ex.Message}");
        }
    }
    private void ShowNameInputPanel()
    {
        nameInputPanel.SetActive(true);
        submitNameButton.onClick.AddListener(OnSubmitName);
    }
    private void ShowDeckSelectPannel()
    {
        deckSelectPannel.SetActive(true);
    }
    public void OnDeckSelect(int mode)
    {
        deckMode = mode;
        deckConfrimPannel.SetActive(true);
        deckConfrimButton.onClick.AddListener(OnSubmitDeck);
    }
    
    public void ExitConfrim()
    {
        deckConfrimPannel.SetActive(false);
    }
    private async void OnSubmitDeck()
    {
        var deckSets = fristDeck[deckMode];
        var newDeck = new Dictionary<string, int>();
        var cards = new Dictionary<string, int>();
        foreach(var deckCard in deckSets.CardList)
        {
            newDeck.Add(deckCard.card.CardCode, deckCard.count);
        }
        try
        {
            var data = new Dictionary<string, object>
            {
                {"PlayerDeck",newDeck},
                {"PlayerCards",cards}
            };
            await CloudSaveService.Instance.Data.Player.SaveAsync(data);
            deckConfrimPannel.SetActive(false);
            deckSelectPannel.SetActive(false);
            mainScreenPannel.SetActive(true);
        }
        catch (System.Exception ex)
        {
            Debug.LogError($"플레이어 이름 설정 중 오류 발생: {ex.Message}");
        }
    }

    private async void OnSubmitName()
    {
        string newName = nameInput.text;

        if (!string.IsNullOrEmpty(newName))
        {
            try
            {
                var data = new Dictionary<string, object>
                {
                    { "PlayerName", newName },{"Gold",1000},{"Stage",0}
                };

                await CloudSaveService.Instance.Data.Player.SaveAsync(data);
                PlayerInfo.Instance.PlayerName = newName;
                PlayerInfo.Instance.Gold = 1000;
                PlayerInfo.Instance.Stage = 0;
                Debug.Log($"플레이어 이름이 {newName}(으)로 설정되었습니다.");
                nameInputPanel.SetActive(false);
                await CheckPlayerDeck();
            }
            catch (System.Exception ex)
            {
                Debug.LogError($"플레이어 이름 설정 중 오류 발생: {ex.Message}");
            }
        }
        else
        {
            Debug.LogWarning("이름을 입력하세요.");
        }
    }
}

