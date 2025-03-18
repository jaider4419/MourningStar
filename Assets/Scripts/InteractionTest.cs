using UnityEngine;
using UnityEngine.EventSystems;

public class Click3DObject : MonoBehaviour
{
    public GameObject uiPanel; 

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) 
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject == gameObject)
                {
                    TriggerUIPanel();
                }
            }
        }
    }

    void TriggerUIPanel()
    {
        if (uiPanel != null)
        {
            uiPanel.SetActive(true);
        }
    }
}
