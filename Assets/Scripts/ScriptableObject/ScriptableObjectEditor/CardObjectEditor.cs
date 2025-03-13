using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Vector2 = System.Numerics.Vector2;

namespace ScriptableObject.ScriptableObjectEditor
{
    public class CardObjectEditor : EditorWindow
    {
        private List<CardScriptableObject> cards = new List<CardScriptableObject>();
        private UnityEngine.Vector2 scrollPos;
        private CardScriptableObject selectedCard;

        [MenuItem("Tools/Create Card ScriptableObject")]
        public static void ShowWindow()
        {
            GetWindow<CardObjectEditor>("Card Data Manager");
        }

        private void OnGUI()
        {
            EditorGUILayout.LabelField("Card Data Manager", EditorStyles.boldLabel);

            if (GUILayout.Button("Load All Cards"))
            {
                LoadAllCards();
            }

            scrollPos = EditorGUILayout.BeginScrollView(scrollPos);

            foreach (var card in cards)
            {
                if (GUILayout.Button(card.name))
                {
                    selectedCard = card;
                }
            }

            EditorGUILayout.EndScrollView();

            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Card ScriptableObject", EditorStyles.boldLabel);

            // selectedCard가 null인지 확인
            if (selectedCard != null)
            {
                SerializedObject serializedObject = new SerializedObject(selectedCard);
                serializedObject.Update();

                EditorGUILayout.PropertyField(serializedObject.FindProperty("cardName"));
                EditorGUILayout.PropertyField(serializedObject.FindProperty("cardDescription"));
                EditorGUILayout.PropertyField(serializedObject.FindProperty("sprite"));
                EditorGUILayout.PropertyField(serializedObject.FindProperty("damage"));
                EditorGUILayout.PropertyField(serializedObject.FindProperty("health"));
                EditorGUILayout.PropertyField(serializedObject.FindProperty("cost"));

                serializedObject.ApplyModifiedProperties();

                if (GUILayout.Button("Save Changes"))
                {
                    EditorUtility.SetDirty(selectedCard);
                    AssetDatabase.SaveAssets();
                }
            }
            else
            {
                EditorGUILayout.HelpBox("카드를 선택하세요.", MessageType.Warning);
            }
        }


        private void LoadAllCards()
        {
            cards.Clear();
            string[] guids = AssetDatabase.FindAssets("t:CardScriptableObject");

            foreach (var guid in guids)
            {
                string path = AssetDatabase.GUIDToAssetPath(guid);
                CardScriptableObject cardScriptableObject = AssetDatabase.LoadAssetAtPath<CardScriptableObject>(path);
                cards.Add(cardScriptableObject);
            }
            
            if (cards.Count > 0 && selectedCard == null)
            {
                selectedCard = cards[0];
            }
        }
    }
}