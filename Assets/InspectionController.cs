using UnityEngine;
using UnityEngine.UI;

public class InspectionController : MonoBehaviour
{
    public static InspectionController Instance;
    public GameObject inspectionPanel;
    public Text captionText;

    void Awake()
    {
        Debug.Log("[InspectionController] Initializing...");
        Instance = this;
        if (inspectionPanel == null) Debug.LogError("Assign inspectionPanel in Inspector!");
        if (captionText == null) Debug.LogError("Assign captionText in Inspector!");
    }

    public void Inspect(Transform obj, string caption)
    {
        Debug.Log($"[InspectionController] Inspecting: {obj.name}");

        obj.SetParent(Camera.main.transform);
        obj.localPosition = new Vector3(0, 0, 2f);

        captionText.text = caption;
        inspectionPanel.SetActive(true);

        Debug.Log("[InspectionController] Inspection started");
    }

    public void EndInspection()
    {
        Debug.Log("[InspectionController] Ending inspection");
        inspectionPanel.SetActive(false);
    }
}