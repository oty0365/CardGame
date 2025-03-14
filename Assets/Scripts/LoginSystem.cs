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


    private async void Start()
    {
        
        await UnityServices.InitializeAsync();
        nameInputPanel.SetActive(false);
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

    private async Task CheckPlayerName()
    {
        try
        {
            var data = await CloudSaveService.Instance.Data.Player.LoadAsync(new HashSet<string> { "PlayerName" });

            if (data.TryGetValue("PlayerName", out var playerName))
            {
                string name = playerName.Value.GetAs<string>();
                Debug.Log($"플레이어 이름: {name}");
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

    private async void OnSubmitName()
    {
        string newName = nameInput.text;

        if (!string.IsNullOrEmpty(newName))
        {
            try
            {
                var data = new Dictionary<string, object>
                {
                    { "PlayerName", newName }
                };

                await CloudSaveService.Instance.Data.Player.SaveAsync(data);
                Debug.Log($"플레이어 이름이 {newName}(으)로 설정되었습니다.");

                nameInputPanel.SetActive(false);
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

