using System;
using UnityEditor;
using UnityEngine;

namespace ScriptableObject.ScriptableObjectEditor
{
    public enum _CardType
    {
        Monster,
        Magic,
        Passive
    }
    
    [CustomEditor(typeof(CardScriptableObject))]
    public class CardObjectInspectEditor : Editor
    {
        private CardScriptableObject cardScriptableObject;
        private Sprite sprite;
        private GUILayoutOption[] options;

        public _CardType cardType;
        
        public override void OnInspectorGUI()
        {
            cardScriptableObject = (CardScriptableObject)target;
            
            EditorGUILayout.LabelField("카드 설정", EditorStyles.boldLabel);
            cardScriptableObject.CardType = (ScriptableObject._CardType)EditorGUILayout.EnumPopup(cardScriptableObject.CardType);
            cardScriptableObject.CardName = EditorGUILayout.TextField("카드 이름", cardScriptableObject.CardName);
            EditorGUILayout.LabelField("카드 설명");
            cardScriptableObject.CardDescription = EditorGUILayout.TextArea(cardScriptableObject.CardDescription, GUILayout.Height(60));
            cardScriptableObject.Sprite = (Sprite)EditorGUILayout.ObjectField("카드 이미지", cardScriptableObject.Sprite, typeof(Sprite), false);
            EditorGUILayout.Space();
            switch (cardScriptableObject.CardType)
            {
                case ScriptableObject._CardType.Monster:
                    cardScriptableObject.Damage = EditorGUILayout.IntField("공격력", cardScriptableObject.Damage);
                    cardScriptableObject.Health = EditorGUILayout.IntField("체력", cardScriptableObject.Health);
                    cardScriptableObject.Cost = EditorGUILayout.IntField("가격", cardScriptableObject.Cost);
                    cardScriptableObject.MonsterEffects = (MonsterEffects)(object)EditorGUILayout.EnumFlagsField("몬스터 능력",cardScriptableObject.MonsterEffects);
                    break;
                case ScriptableObject._CardType.Magic:
                    cardScriptableObject.Cost = EditorGUILayout.IntField("가격", cardScriptableObject.Cost);
                    cardScriptableObject.MagicEffects = (MagicEffects)(object)EditorGUILayout.EnumFlagsField("마법 효과", cardScriptableObject.MagicEffects);
                    break;
                case ScriptableObject._CardType.Passive:
                    cardScriptableObject.Cost = EditorGUILayout.IntField("가격", cardScriptableObject.Cost);
                    cardScriptableObject.PassiveEffects = (PassiveEffects)(object)EditorGUILayout.EnumFlagsField("패시브 스킬", cardScriptableObject.PassiveEffects);
                    break;
            }

            if (GUI.changed)
            {
                EditorUtility.SetDirty(cardScriptableObject);
            }
        }
    }
}