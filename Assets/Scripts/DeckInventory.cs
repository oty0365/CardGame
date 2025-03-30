using ScriptableObject;
using System;
using System.Collections;
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
        Debug.Log("덱 초기화 중");
        foreach (var i in PlayerInfo.Instance.playerDeck)
        {
            Debug.Log(i);
        }
        var index = 0;
        for(int i= 0; i < PlayerInfo.Instance.cardsInDeck.Length; i++)
        {
            PlayerInfo.Instance.cardsInDeck[i].card = null;
            PlayerInfo.Instance.cardsInDeck[i].count = 0;
        }
        foreach (DictionaryEntry i in PlayerInfo.Instance.playerDeck)
        {
                PlayerInfo.Instance.cardsInDeck[index].card = CardManager.Instance.CardDict[(string)i.Key];
                PlayerInfo.Instance.cardsInDeck[index].count = (int)i.Value;
                index++;
        }
        Debug.Log("덱 초기화 완료");
    }


    void Update()
    {


    }
}
