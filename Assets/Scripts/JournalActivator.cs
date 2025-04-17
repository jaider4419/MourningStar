using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class JournalActivator : MonoBehaviour
{

    public GameObject JournalChange;

    public GameObject Book;
    public AudioSource AcquiredJournal;
    public GameObject HideUi;
    public GameObject AnotherActive;

    public void Activate()
    {
        {

            {
                JournalChange.SetActive(true);
                Book.SetActive(false);
                AcquiredJournal.Play();
                HideUi.SetActive(false);
                AnotherActive.SetActive(true);
            }
        }
    }


}


