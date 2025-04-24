using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezeTrigger : MonoBehaviour
{

    public GameObject panel;




    public void freeze()
    {
        Time.timeScale = 0f;
    }

    public void unfreeze()
    {
        Time.timeScale = 1f;
    }
}
