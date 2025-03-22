using ScriptableObject;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using UnityEngine;

public class DeckInventory : MonoBehaviour
{

    public async Task OnEnableDecks()
    {
        await DataManager.Instance.LoadPlayerDeck();
    }
    public void InitializeDeckInventory()
    {
        var index = 0;
        foreach (var i in PlayerInfo.Instance.playerDeck)
        {
            Debug.Log(CardManager.Instance.CardDict[i.Key] + "," + i.Value);
            PlayerInfo.Instance.cardsInDeck[index].card = CardManager.Instance.CardDict[i.Key];
            PlayerInfo.Instance.cardsInDeck[index].count = i.Value;
            index++;
        }
    }


    void Update()
    {


    }
}
