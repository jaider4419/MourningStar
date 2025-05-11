using UnityEngine;

public class Click3DObject : MonoBehaviour
{
    public GameObject uiPanel;
    [SerializeField] private AudioSource audioSource; 

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
                    PlayClickSound(); 
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

    void PlayClickSound()
    {
        if (audioSource != null && audioSource.clip != null)
        {
            audioSource.Play();
        }
    }
}