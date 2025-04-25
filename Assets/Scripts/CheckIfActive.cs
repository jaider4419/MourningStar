using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckIfActive : MonoBehaviour
{
    public GameObject GameObjectA;
    public GameObject GameObjectB;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameObjectA.activeInHierarchy)
        {
            GameObjectB.SetActive(true);

            GameObjectA.SetActive(false);

        }

    }
}
