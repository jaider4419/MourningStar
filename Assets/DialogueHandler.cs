using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueHandler : MonoBehaviour
{
    public GameObject[] Dialogues;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Dialogue());
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator Dialogue()
    {
        yield return new WaitForSeconds(10);
        Dialogues[0].SetActive(true);
        Debug.Log("Its active");

        yield return new WaitForSeconds(10);
        Dialogues[0].SetActive(false);
    }
}
