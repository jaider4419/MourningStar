using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NightFlash : MonoBehaviour
{
    public GameObject ActiveLight;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseEnter()
    {
        ActiveLight.SetActive(true);
    }

    private void OnMouseExit()
    {

        ActiveLight.SetActive(false);
    }
}
