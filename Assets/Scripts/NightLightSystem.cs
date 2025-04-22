using UnityEngine;

public class ClickHoldCubeLightControl : MonoBehaviour
{
    public MonsterController.WindowPosition windowPosition;

    private Light[] lights;
    public bool IsLightOn { get; private set; }
    private bool wasLightOnLastFrame;

    void Start()
    {
        lights = GetComponentsInChildren<Light>(true);
        SetLights(false);
    }

    void Update()
    {
        wasLightOnLastFrame = IsLightOn;

        if (Input.GetMouseButton(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit) && hit.collider.gameObject == gameObject)
            {
                SetLights(true);
                NightManager.Instance.DrainEnergy();

                // Notify the monster if this is a new light activation
                if (!wasLightOnLastFrame)
                {
                    MonsterController monster = FindObjectOfType<MonsterController>();
                    if (monster != null) monster.OnLightExposed(windowPosition);
                }
                return;
            }
        }

        SetLights(false);

        // Notify monster if light was just turned off
        if (wasLightOnLastFrame && !IsLightOn)
        {
            MonsterController monster = FindObjectOfType<MonsterController>();
            if (monster != null) monster.OnLightRemoved();
        }
    }

    public void SetLights(bool state)
    {
        IsLightOn = state;
        foreach (Light l in lights)
        {
            if (l != null) l.enabled = state;
        }
    }
}