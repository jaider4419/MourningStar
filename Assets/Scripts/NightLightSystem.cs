using UnityEngine;

public class ClickHoldCubeLightControl : MonoBehaviour
{
    private Light[] allLights; // This will include cube's own light + any child lights

    void Start()
    {
        // Get Light on the cube itself
        Light ownLight = GetComponent<Light>();

        // Get all child Light components (excluding the cube's own Light)
        Light[] childLights = GetComponentsInChildren<Light>(includeInactive: true);

        // Combine both into one array
        if (ownLight != null)
        {
            allLights = new Light[childLights.Length];
            int i = 0;
            foreach (Light l in childLights)
            {
                allLights[i++] = l;
            }
        }
        else
        {
            allLights = childLights;
        }

        // Start with all lights off
        SetLights(false);
    }

    void Update()
    {
        if (Input.GetMouseButton(0)) // Mouse held down
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (hit.collider.gameObject == gameObject)
                {
                    SetLights(true);
                    return;
                }
            }
        }

        // Not clicking or clicked off
        SetLights(false);
    }

    void SetLights(bool state)
    {
        foreach (Light l in allLights)
        {
            l.enabled = state;
        }
    }
}
