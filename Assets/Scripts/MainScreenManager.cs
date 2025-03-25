using UnityEngine;

public class MainScreenManager : MonoBehaviour
{
    public static MainScreenManager Instance;
    public GameObject loadingPannel;
    private void Awake()
    {
        Instance = this;     
    }
    void Start()
    {
        loadingPannel.SetActive(false);
    }
    public void StartLoadingPannel()
    {
        loadingPannel.SetActive(true);
    }
    public void EndLoadingPannel()
    {
        loadingPannel.SetActive(false);
    }
}
