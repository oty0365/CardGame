using System.Threading.Tasks;
using UnityEngine;

public class CardInventory : MonoBehaviour
{
    public GameObject cardBase;

    private void Awake()
    {
    }
    private void Start()
    {
        //gameObject.SetActive(false);
        for (var i = 0; i < CardManager.Instance.cardSets.CardList.Count; i++)
        {
            var o = Instantiate(cardBase, gameObject.transform);
            o.transform.localScale = new Vector3(1.1f, 1.1f, 1);
        }
    }
    public async Task OnEnableCardLoad()
    {
        await DataManager.Instance.LoadPlayerCards();
        Debug.Log("카드 로드");
    }
}
