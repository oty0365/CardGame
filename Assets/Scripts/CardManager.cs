using ScriptableObject;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    public CardSets cardSets;
    public Dictionary<string, CardScriptableObject> CardDict = new Dictionary<string, CardScriptableObject>();
    void Start()
    {
        foreach(var i in cardSets.CardList)
        {
            CardDict.Add(i.CardCode, i);
        }
    }
    void Update()
    {
        
    }
}
