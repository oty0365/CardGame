using UnityEngine;
public interface IInteracter
{
    public void OnHover()
    {

    }
    public void ExitHover()
    {

    }
    public void OnClick()
    {

    }
}
public class MousePointer : MonoBehaviour
{
    public static MousePointer instance;
    [SerializeField] private Camera mainCamera;
    private Vector2 mousePos;
    private RaycastHit2D raycastHit2D;
    public GameObject currentHitObject;
    
    void Awake()
    {
        instance = this;
    }
    void Start()
    {
        
    }
    void Update()
    {
        mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        raycastHit2D = Physics2D.Raycast(mousePos, Vector2.zero, 1000f);
        Debug.Log(raycastHit2D.collider);
        if (raycastHit2D.collider == null)
        {
            if (currentHitObject != null)
            {
                currentHitObject.GetComponent<IInteracter>().ExitHover();
            }
            currentHitObject = null;
        }
        else
        {
            if (raycastHit2D.collider.gameObject != currentHitObject)
            {
                if (currentHitObject != null)
                {
                    currentHitObject.GetComponent<IInteracter>().ExitHover();
                }
                currentHitObject = raycastHit2D.collider.gameObject;
                currentHitObject.GetComponent<IInteracter>().OnHover();
            }
        }

    }
}
