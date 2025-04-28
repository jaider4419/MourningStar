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
        yield return new WaitForSeconds(2);
        Dialogues[0].SetActive(true);
        Debug.Log("Its active");

        yield return new WaitForSeconds(4);
        Dialogues[0].SetActive(false);
        yield return new WaitForSeconds(2);
        Dialogues[1].SetActive(true);
        yield return new WaitForSeconds(2);
        Dialogues[1].SetActive(false);
        yield return new WaitForSeconds(2);
        Dialogues[2].SetActive(true);
        yield return new WaitForSeconds(5);
        Dialogues[3].SetActive(true);
        yield return new WaitForSeconds(3);
        Dialogues[3].SetActive(false);
        yield return new WaitForSeconds(3);
        Dialogues[4].SetActive(true);
        yield return new WaitForSeconds(3);
        StopCoroutine(Dialogue());
    }
}
