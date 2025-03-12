using System.Collections.Generic;
using System.IO;
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
        private bool isCreateCard = false;
        private CardScriptableObject newCard;
        private string newCardPath = "";

        [MenuItem("Tools/Create Card ScriptableObject")]
        public static void ShowWindow()
        {
            GetWindow<CardObjectEditor>("Card Data Manager");
        }

        private void OnGUI()
        {
            EditorGUILayout.LabelField("Card Data Manager", EditorStyles.boldLabel);

            if (!isCreateCard) // 카드 생성 모드가 아닐 때
            {
                if (GUILayout.Button("Load All Cards"))
                {
                    LoadAllCards();
                }

                if (GUILayout.Button("Create New Card"))
                {
                    isCreateCard = true;
                    CreateNewCard();
                }

                scrollPos = EditorGUILayout.BeginScrollView(scrollPos);
                {
                    foreach (var card in cards)
                    {
                        if (card != null && GUILayout.Button(card.name))
                        {
                            selectedCard = card;
                        }
                    }
                }
                EditorGUILayout.EndScrollView();

                EditorGUILayout.Space();
                EditorGUILayout.LabelField("Card ScriptableObject", EditorStyles.boldLabel);

                if (selectedCard != null)
                {
                    if (AssetDatabase.Contains(selectedCard))
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
                        selectedCard = null;
                    }
                }
            }
            else // 카드 생성 모드일 때
            {
                EditorGUILayout.LabelField("Create New Card", EditorStyles.boldLabel);

                if (newCard == null)
                {
                    CreateNewCard();
                }

                SerializedObject serializedObject = new SerializedObject(newCard);
                serializedObject.Update();

                EditorGUILayout.PropertyField(serializedObject.FindProperty("cardName"));
                EditorGUILayout.PropertyField(serializedObject.FindProperty("cardDescription"));
                EditorGUILayout.PropertyField(serializedObject.FindProperty("sprite"));
                EditorGUILayout.PropertyField(serializedObject.FindProperty("damage"));
                EditorGUILayout.PropertyField(serializedObject.FindProperty("health"));
                EditorGUILayout.PropertyField(serializedObject.FindProperty("cost"));

                serializedObject.ApplyModifiedProperties();

                EditorGUILayout.Space();
                if (GUILayout.Button("Save New Card"))
                {
                    SaveNewCard();
                }

                if (GUILayout.Button("Cancel"))
                {
                    isCreateCard = false;
                    newCard = null;
                }
            }
        }

        private void CreateNewCard()
        {
            newCard = UnityEngine.ScriptableObject.CreateInstance<CardScriptableObject>();

            string directoryPath = "Assets/Resources/Cards/";
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
                AssetDatabase.Refresh();
            }

            newCardPath = AssetDatabase.GenerateUniqueAssetPath(directoryPath + "NewCard.asset");
            AssetDatabase.CreateAsset(newCard, newCardPath);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }

        private void SaveNewCard()
        {
            if (newCard == null)
            {
                Debug.LogError("Error: newCard is null!");
                return;
            }

            if (string.IsNullOrEmpty(newCardPath))
            {
                Debug.LogError("Error: newCardPath is empty!");
                return;
            }

            EditorUtility.SetDirty(newCard);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();

            cards.Add(newCard);
            selectedCard = newCard;
            isCreateCard = false;
            newCard = null;
            newCardPath = "";
        }

        private void LoadAllCards()
        {
            cards.Clear();
            string[] guids = AssetDatabase.FindAssets("t:CardScriptableObject");

            foreach (var guid in guids)
            {
                string path = AssetDatabase.GUIDToAssetPath(guid);
                CardScriptableObject cardScriptableObject = AssetDatabase.LoadAssetAtPath<CardScriptableObject>(path);
                if (cardScriptableObject != null) 
                {
                    cards.Add(cardScriptableObject);
                }
            }

            if (cards.Count > 0 && selectedCard == null)
            {
                selectedCard = cards[0];
            }
        }
    }
}
