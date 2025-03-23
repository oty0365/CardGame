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
        Debug.Log("덱 초기화 중");
        var index = 0;
        Debug.Log(PlayerInfo.Instance.cardsInDeck.Length);
        foreach (var i in PlayerInfo.Instance.playerDeck)
        {
            //Debug.Log(CardManager.Instance.CardDict[i.Key] + "," + i.Value);
            try
            {
                PlayerInfo.Instance.cardsInDeck[index].card = CardManager.Instance.CardDict[i.Key];
                PlayerInfo.Instance.cardsInDeck[index].count = i.Value;
                index++;
            }
            catch(System.Exception ex)
            {
                Debug.LogException(ex);
            }

        }
        Debug.Log("덱 초기화 완료");
    }


    void Update()
    {


    }
}
