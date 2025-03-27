using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCliffCor : MonoBehaviour
{
    public GameObject AnimCam;
    public GameObject MainCam;
    public GameObject KeepUi;

    void Start()
    {
        StartCoroutine(CameraAnim());
    }

    IEnumerator CameraAnim()
    {
        yield return new WaitForSeconds(6);
        AnimCam.SetActive(false);
        MainCam.SetActive(true);
        Debug.Log("Transition happened.");
        KeepUi.SetActive(true);

    }

    

}

