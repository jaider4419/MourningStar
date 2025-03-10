using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InteractionPromptUI : MonoBehaviour
{


    [SerializeField] private GameObject uiPanel;
    [SerializeField] private TextMeshProUGUI _promptText;

    // Start is called before the first frame update
    void Start()
    {
        uiPanel.SetActive(false);
    }

    // Update is called once per frame

    public bool IsDisplayed = false;

    public void SetUp(string promptText)

    {
        _promptText.text = promptText;
        uiPanel.SetActive(true);
        IsDisplayed = true;
    }

    public void Close()
    {
        uiPanel.SetActive(false);
        IsDisplayed = false;

    }
}