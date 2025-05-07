using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TriggerDestroy : MonoBehaviour
{
    public GameObject chosenText;

    private void OnTriggerEnter(Collider other)
    {
       if (other.tag == "Player")
        {
            StartCoroutine(TimeDisplay());
        }
    }

    IEnumerator TimeDisplay()
    {
        chosenText.SetActive(true);
        yield return new WaitForSeconds(6);
        Destroy(chosenText );
        Debug.Log("Its destroyed");
        chosenText.SetActive(false);

    }
}
