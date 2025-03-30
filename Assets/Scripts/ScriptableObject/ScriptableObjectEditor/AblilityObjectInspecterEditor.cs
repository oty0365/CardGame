using ScriptableObject;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(AbilityScriptableObject))]
public class AblilityObjectInspecterEditor : Editor
{
    private AbilityScriptableObject abilityScriptableObject;
    private GUILayoutOption[] options;

    public _CardType cardType;

    public override void OnInspectorGUI()
    {
        abilityScriptableObject = (AbilityScriptableObject)target;

        EditorGUILayout.LabelField("능력", EditorStyles.boldLabel);
        abilityScriptableObject.CardType = (ScriptableObject._CardType)EditorGUILayout.EnumPopup(abilityScriptableObject.CardType);
        abilityScriptableObject.AbilityName = EditorGUILayout.TextField("능력 이름", abilityScriptableObject.AbilityName);
        EditorGUILayout.LabelField("능력 설명");
        abilityScriptableObject.abilityDesc = EditorGUILayout.TextArea(abilityScriptableObject.abilityDesc, GUILayout.Height(60));
        EditorGUILayout.Space();
        switch (abilityScriptableObject.CardType)
        {
            case ScriptableObject._CardType.Monster:
                abilityScriptableObject.MonsterEffects = (MonsterEffects)(object)EditorGUILayout.EnumPopup("몬스터 능력", abilityScriptableObject.MonsterEffects);
                break;
            case ScriptableObject._CardType.Magic:
                abilityScriptableObject.MagicEffects = (MagicEffects)(object)EditorGUILayout.EnumPopup("마법 효과", abilityScriptableObject.MagicEffects);
                break;
            case ScriptableObject._CardType.Passive:
                abilityScriptableObject.PassiveEffects = (PassiveEffects)(object)EditorGUILayout.EnumPopup("패시브 스킬", abilityScriptableObject.PassiveEffects);
                break;
        }

        if (GUI.changed)
        {
            EditorUtility.SetDirty(abilityScriptableObject);
        }
    }
}
