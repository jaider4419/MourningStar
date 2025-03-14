using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoverObj : MonoBehaviour
{
    public GameObject Camera;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = Camera.GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (GetComponent<Collider>().Raycast(ray, out hit, 100f))
        {
            print("hover on " + gameObject.name);
        }
    }
}
