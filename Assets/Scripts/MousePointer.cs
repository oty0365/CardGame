using System.Collections.Generic;
using System.Linq;
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

public enum PointerMode
{
    Draw,
    CardSelectMode,
    CardSetOrFireMode,
    NotTurn
}

public class MousePointer : MonoBehaviour
{
    public static MousePointer instance;
    [SerializeField] private Camera mainCamera;
    private Vector2 mousePos;
    private RaycastHit2D raycastHit2D;
    private RaycastHit2D[] raycastHits2D;
    public GameObject currentHitObject;
    public PointerMode pointer;
    
    void Awake()
    {
        instance = this;
    }
    void Start()
    {
        
    }
    void Update()
    {
        switch (pointer)
        {
            case PointerMode.Draw:
                break;
            case PointerMode.CardSelectMode:
                float maxZPos=-1;
                mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
                raycastHits2D = Physics2D.RaycastAll(mousePos, Vector2.zero, 1000f);
                if(raycastHits2D.Length<=0)
                {
                    if (currentHitObject != null)
                    {
                        currentHitObject.GetComponent<IInteracter>().ExitHover();
                    }
                }
                else
                {
                    foreach (var hit in raycastHits2D)
                    {
                        if (hit.collider.CompareTag("card") && maxZPos < hit.collider.gameObject.transform.position.z)
                        {
                            maxZPos = hit.collider.gameObject.transform.position.z;
                            raycastHit2D = hit;
                        }
                    }
                    //hitCards.Sort((a, b) => a.collider.gameObject.transform.position.z.CompareTo(b.collider.gameObject.transform.position.z));
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
                break;
            case PointerMode.CardSetOrFireMode:
                break;
            case PointerMode.NotTurn:
                break;
        }

    }
}
