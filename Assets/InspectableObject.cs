using UnityEngine;

public class ClickableObject : MonoBehaviour
{
    public string itemDescription = "Object info here";

    void Start()
    {
        Debug.Log($"[ClickableObject] Ready: {gameObject.name}", gameObject);
    }

    void OnMouseDown()
    {
        Debug.Log($"[ClickableObject] MouseDown on {gameObject.name}");

        if (!IsClickValid()) return;
        Debug.Log($"[ClickableObject] Valid click on {gameObject.name}");
        InspectionController.Instance.Inspect(transform, itemDescription);
    }

    bool IsClickValid()
    {
        // Check EventSystem
        if (UnityEngine.EventSystems.EventSystem.current == null)
        {
            Debug.LogError("No EventSystem in scene!");
            return false;
        }

        // Check UI blocking
        if (UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
        {
            Debug.Log("[ClickableObject] Click blocked by UI");
            return false;
        }

        // Raycast check
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            Debug.Log($"[ClickableObject] Raycast hit: {hit.transform.name}");
            return hit.transform == transform;
        }

        Debug.Log("[ClickableObject] Raycast missed everything");
        return false;
    }

    void OnMouseEnter() => Debug.Log($"[ClickableObject] Mouse ENTER {gameObject.name}");
    void OnMouseExit() => Debug.Log($"[ClickableObject] Mouse EXIT {gameObject.name}");
}