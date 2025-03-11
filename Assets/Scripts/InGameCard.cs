using ScriptableObject;
using TMPro;
using UnityEngine;

public enum CardState
{
    None,
    Hover,
    Selected,
}
public class InGameCard : MonoBehaviour
{
    [Header("ī�� ������")]
    public Sprite[] cardFrame;
    [Header("ī�� ��ũ���ͺ� ������Ʈ")]
    public CardScriptableObject card;
    [Header("ī������")]
    public SpriteRenderer currentCardFrame;
    public TextMeshPro cardName;
    public TextMeshPro cardCoast;
    public TextMeshPro cardDesc;
    public TextMeshPro damage;
    public TextMeshPro hp;
    [Header("ī�� ����")]
    public CardState cardState;
    public float multiplyer;
    public float changeSpeed;

    private Vector3 originalScale;
    private Vector3 changedScale;

    void Start()
    {
        InitCard();
        CardUpdate();
    }
    private void Update()
    {
        switch (cardState)
        {
            case CardState.None:
                ReturnToOrigin();
                break;
            case CardState.Hover:
                CardOnHover();
                break;
        }
    }
    public void CardUpdate()
    {
        cardName.text = card.CardName;
        if (!card.CanLook)
        {
            currentCardFrame.sprite = cardFrame[3];
        }
        else
        {
            switch (card.CardType)
            {
                case _CardType.Monster:
                    currentCardFrame.sprite = cardFrame[0];
                    cardCoast.text = card.Cost.ToString();
                    damage.text = card.Damage.ToString();
                    hp.text = card.Health.ToString();
                    cardDesc.text = card.MonsterEffects.ToString();
                    break;
                case _CardType.Magic:
                    currentCardFrame.sprite = cardFrame[1];
                    cardCoast.text = card.Cost.ToString();
                    cardDesc.text = card.MagicEffects.ToString();
                    break;
                case _CardType.Passive:
                    currentCardFrame.sprite = cardFrame[2];
                    cardDesc.text = card.PassiveEffects.ToString();
                    break;
            }
        }

    }
    public void InitCard()
    {
        cardState = CardState.None;
        originalScale = new Vector3(gameObject.transform.localScale.x, gameObject.transform.localScale.y, gameObject.transform.localScale.z);
        changedScale = originalScale * multiplyer;
    }
    public void CardOnHover()
    {
        gameObject.transform.localScale = Vector3.Lerp(gameObject.transform.localScale, changedScale, changeSpeed * Time.deltaTime);
    }
    public void ReturnToOrigin()
    {
        gameObject.transform.localScale=Vector3.Lerp(gameObject.transform.localScale, originalScale, changeSpeed * Time.deltaTime);
    }
}
