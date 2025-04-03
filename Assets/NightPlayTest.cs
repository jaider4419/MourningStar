using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Animations;
using UnityEngine;

public class NightPlayTest : MonoBehaviour
{
    public GameObject TextAppear;
    public GameObject Monster;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(NightCycle());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator NightCycle()
    {
        TextAppear.SetActive(true);

        yield return new WaitForSeconds(5);
        TextAppear.SetActive(false);

        yield return new WaitForSeconds(5);

        Monster.SetActive(true);
    }
}
