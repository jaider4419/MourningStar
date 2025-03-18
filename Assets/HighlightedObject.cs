using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Highlight : MonoBehaviour
{
    public GameObject HighLightObject;

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
        HighLightObject.SetActive(true);
    }

    private void OnMouseExit()
    {
        HighLightObject.SetActive(false);
    }
}
