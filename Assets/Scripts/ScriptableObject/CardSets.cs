using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "CardSets", menuName = "Scriptable Objects/CardSets")]
public class CardSets : UnityEngine.ScriptableObject
{
    [SerializeField] private List<ScriptableObject.CardScriptableObject> cardSets;
    public IReadOnlyList<ScriptableObject.CardScriptableObject> CardList => cardSets;
}
