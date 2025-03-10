using UnityEngine;

namespace ScriptableObject
{
    public enum _CardType
    {
        Monster,
        Magic,
        Passive
    }
    
    [CreateAssetMenu(fileName = "Card", menuName = "ScriptableObjects/Card")]
    public class CardScriptableObject : UnityEngine.ScriptableObject
    {
        [SerializeField]
        private _CardType cardType;
        public _CardType CardType { get => cardType; set => cardType = value; }
    
        [SerializeField]
        private string name;
        public string Name { get => name; set => name = value; }
        
        [SerializeField]
        [TextArea]private string description;
        [TextArea]public string Description { get => description; set => description = value; }

        [SerializeField]
        private Sprite sprite;
        public Sprite Sprite { get => sprite; set => sprite = value; }

        [SerializeField]
        private int damage;
        public int Damage { get => damage; set => damage = value; }

        [SerializeField]
        private int health;
        public int Health { get => health; set => health = value; }
        
        [SerializeField]
        private int cost;
        public int Cost { get => cost; set => cost = value; }
        
    }
}