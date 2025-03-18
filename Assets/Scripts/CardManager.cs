using ScriptableObject;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    public static CardManager Instance { get; private set; }
    public CardSets cardSets;
    public Dictionary<string, CardScriptableObject> CardDict = new Dictionary<string, CardScriptableObject>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); 
        }
        else
        {
            Destroy(gameObject); 
        }
    }
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
