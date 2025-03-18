using JetBrains.Annotations;
using ScriptableObject;
using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class DeckSet
{
    public CardScriptableObject card;
    public int count;
}

[CreateAssetMenu(fileName = "DeckSets", menuName = "Scriptable Objects/DeckSets")]
public class DeckSets : UnityEngine.ScriptableObject
{
    [SerializeField] private List<DeckSet> deckSets;
    public IReadOnlyList<DeckSet> CardList => deckSets;
}
