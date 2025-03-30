using ScriptableObject;
using UnityEngine;

[CreateAssetMenu(fileName = "AbilityScriptableObject", menuName = "Scriptable Objects/AbilityScriptableObject")]
public class AbilityScriptableObject : UnityEngine.ScriptableObject
{
    [SerializeField]
    private _CardType cardType;
    public _CardType CardType { get => cardType; set => cardType = value; }

    [SerializeField]
    private MonsterEffects monsterEffects;
    public MonsterEffects MonsterEffects { get => monsterEffects; set => monsterEffects = value; }

    [SerializeField]
    private MagicEffects magicEffects;
    public MagicEffects MagicEffects { get => magicEffects; set => magicEffects = value; }

    [SerializeField]
    private PassiveEffects passiveEffects;
    public PassiveEffects PassiveEffects { get => passiveEffects; set => passiveEffects = value; }

    [SerializeField]
    private string abilityName;
    public string AbilityName { get => abilityName; set => abilityName = value; }

    [TextArea]
    public string abilityDesc;
}
