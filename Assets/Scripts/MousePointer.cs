using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public interface IInteracter
{
    public void OnHover();
    public void ExitHover();
    public void OnClick();
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
    private RaycastHit2D[] raycastHits2D;
    public GameObject currentHitObject;
    public PointerMode pointer;

    void Awake()
    {
        instance = this;
    }

    void Update()
    {
        switch (pointer)
        {
            case PointerMode.Draw:
                break;
            case PointerMode.CardSelectMode:
                HandleCardSelection();
                break;
            case PointerMode.CardSetOrFireMode:
                break;
            case PointerMode.NotTurn:
                break;
        }
    }

    private void HandleCardSelection()
    {
        float maxZPos = float.MinValue;
        mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        raycastHits2D = Physics2D.RaycastAll(mousePos, Vector2.zero, 1000f);

        GameObject newHitObject = null;

        if (raycastHits2D.Length > 0)
        {
            foreach (var hit in raycastHits2D)
            {
                if (hit.collider.CompareTag("card") && hit.collider.gameObject.transform.position.z > maxZPos)
                {
                    maxZPos = hit.collider.gameObject.transform.position.z;
                    newHitObject = hit.collider.gameObject;
                }
            }
        }

        if (newHitObject != currentHitObject)
        {
            if (currentHitObject != null && currentHitObject.TryGetComponent<IInteracter>(out var oldInteracter))
            {
                oldInteracter.ExitHover();
            }

            currentHitObject = newHitObject;

            if (currentHitObject != null && currentHitObject.TryGetComponent<IInteracter>(out var newInteracter))
            {
                newInteracter.OnHover();
            }
        }
    }
}




