using ScriptableObject;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public enum CardState
{
    None,
    Hover,
    Selected,
}
public class InGameCard : MonoBehaviour,IInteracter
{
    [Header("카드 프레임")]
    [SerializeField] private Sprite[] cardFrame;
    [Header("카드형식")]
    public SpriteRenderer currentCardFrame;
    [SerializeField] private TextMeshPro cardName;
    [SerializeField] private TextMeshPro cardCoast;
    [SerializeField] private TextMeshPro cardDesc;
    [SerializeField] private TextMeshPro damage;
    [SerializeField] private TextMeshPro hp;
    [SerializeField] private SpriteRenderer cardImage;
    [Header("카드 상태")]
    public CardState cardState;
    [SerializeField] private float multiplyer;
    [SerializeField] private float changeSpeed;
    [Header("카드 스크립터블 오브젝트")]
    [SerializeField] private CardScriptableObject card;
    public CardScriptableObject Card
    {
        get => card;
        set
        {
            card = value;
            CardUpdate();
        }
    }

    private SortingGroup sortingGroup;
    private Vector3 originalScale;
    private Vector3 changedScale;
    private Coroutine scalingFlow;
    private int currentLayer;

    void Start()
    {
        sortingGroup = GetComponent<SortingGroup>();
        InitCard();
        /*card.CanLook = true;
        Card = card;
        card = CardManager.Instance.CardDict["dog"];
        card.CanLook = true;
        Card = card;*/
        //CardUpdate();
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
                    cardImage.sprite = card.Sprite;
                    break;
                case _CardType.Magic:
                    currentCardFrame.sprite = cardFrame[1];
                    cardCoast.text = card.Cost.ToString();
                    cardDesc.text = card.MagicEffects.ToString();
                    cardImage.sprite = card.Sprite;
                    break;
                case _CardType.Passive:
                    currentCardFrame.sprite = cardFrame[2];
                    cardDesc.text = card.PassiveEffects.ToString();
                    cardImage.sprite = card.Sprite;
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
    public void CardScaling(Vector3 targetScale) 
    {
        gameObject.transform.localScale = Vector3.Lerp(gameObject.transform.localScale, targetScale, changeSpeed * Time.deltaTime);
    }
    private IEnumerator ChangeScaleFlow(Vector3 targetScale)
    {
        while (Vector3.Distance(targetScale, gameObject.transform.localScale) > 0.01f)
        {
            CardScaling(targetScale);
            yield return null;
        }
    }
    public void OnHover()
    {

        if (scalingFlow != null)
        {
            StopCoroutine(scalingFlow);
        }
        scalingFlow = StartCoroutine(ChangeScaleFlow(changedScale));
        currentLayer = sortingGroup.sortingOrder;
        sortingGroup.sortingOrder = 20;
    }
    public void ExitHover()
    {

        if (scalingFlow != null)
        {
            StopCoroutine(scalingFlow);
        }
        scalingFlow = StartCoroutine(ChangeScaleFlow(originalScale));
        sortingGroup.sortingOrder = currentLayer;
    }
    public void OnClick()
    {

    }
}
