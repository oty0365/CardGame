using UnityEngine;

public class MousePointer : MonoBehaviour
{
    public static MousePointer instance;
    [SerializeField] private Camera mainCamera;
    private Vector2 mousePos;
    private RaycastHit2D raycastHit2D;
    
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
    }
}
