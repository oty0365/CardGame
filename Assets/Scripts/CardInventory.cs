using System.Threading.Tasks;
using UnityEngine;

public class CardInventory : MonoBehaviour
{
    public int cardCount;
    public GameObject cardBase;

    private void Awake()
    {
        for(var i = 0; i < cardCount; i++)
        {
            var o = Instantiate(cardBase, gameObject.transform); 
            o.transform.localScale = new Vector3(1.1f, 1.1f, 1);
        }
    }
    private void Start()
    {
        //gameObject.SetActive(false);
    }
    public async Task OnEnableCardLoad()
    {
        await DataManager.Instance.LoadPlayerCards();
        Debug.Log("카드 로드");
    }
}
