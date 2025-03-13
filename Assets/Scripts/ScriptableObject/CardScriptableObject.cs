    using UnityEngine;
    using UnityEngine.Serialization;

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
        private string cardCode;
        public string CardCode { get => cardCode; set => cardCode = value; }

        [SerializeField]
        private _CardType cardType;
        public _CardType CardType { get => cardType; set => cardType = value; }

        [SerializeField]
        private string cardName;
        public string CardName { get => cardName; set => cardName = value; }

        [FormerlySerializedAs("description")]
        [SerializeField]
        [TextArea] private string cardDescription;
        [TextArea] public string CardDescription { get => cardDescription; set => cardDescription = value; }

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
        private bool canLook;
        public bool CanLook  {get => canLook; set => canLook = value; }
        public CardScriptableObject(string cardDescription)
        {
            this.cardDescription = cardDescription;
        }



    }
}