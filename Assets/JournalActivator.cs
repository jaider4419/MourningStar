using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class JournalActivator : MonoBehaviour
{

    public GameObject JournalChange;
    public GameObject Dialo;
    public GameObject Book;
    // Start is called before the first frame update


    // Update is called once per frame

    public void ActivateObject()
    {

        {
            JournalChange.SetActive(true);
            Debug.Log("Touch Detected");
            Dialo.SetActive(true);
        }
    }

}
