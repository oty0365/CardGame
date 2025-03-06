using System;
using System.Threading.Tasks;
using Unity.Services.Authentication;
using Unity.Services.Authentication.Components;
using Unity.Services.Authentication.PlayerAccounts;
using Unity.Services.Core;
using Unity.Services.Multiplayer;
using UnityEngine;

public class LoginSystem : MonoBehaviour
{
    private async void Start()
    {
        
        await UnityServices.InitializeAsync();
        PlayerAccountService.Instance.SignedIn -= SignInWithUnity;
        PlayerAccountService.Instance.SignedIn += SignInWithUnity;
    }
    async Task SignInWithUnityAsync(string accessToken)
    {
        try
        {
            await AuthenticationService.Instance.SignInWithUnityAsync(accessToken);
            Debug.Log("SignIn is successful.");
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
}
