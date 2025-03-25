using ScriptableObject;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardDescriptionPannel : MonoBehaviour
{
    public static CardDescriptionPannel Instance;
    public TextMeshProUGUI cardName;
    public TextMeshProUGUI cardDamage;
    public TextMeshProUGUI cardHealth;
    public TextMeshProUGUI cardDesc;
    public Image cardImage;
    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        gameObject.SetActive(false);
    }
    public void PutCardInfo(CardScriptableObject card)
    {
        cardName.text = card.CardName;
        cardDesc.text = card.CardDescription;
        cardImage.sprite = card.Sprite;
        cardDamage.text = null;
        cardHealth.text = null;
        if (card.CardType == _CardType.Monster)
        {
            cardDamage.text = card.Damage.ToString();
            cardHealth.text = card.Health.ToString();
        }

    }
    public void QuitCardInfo()
    {
        gameObject.SetActive(false);
    }
}
