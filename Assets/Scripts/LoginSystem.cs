using System;
using Unity.Services.Authentication;
using Unity.Services.Authentication.Components;
using Unity.Services.Core;
using Unity.Services.Multiplayer;
using UnityEngine;

public class LoginSystem : MonoBehaviour
{
    private async void Start()
    {
        await UnityServices.InitializeAsync();
    }
    public async void SignInWithUnity()
    {
        try { await AuthenticationService.Instance.SignInWithUnityAsync(AuthenticationService.Instance.PlayerId); }
        catch(Exception e) { Debug.Log(e); }
    }
}
