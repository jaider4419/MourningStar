using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NightCam : MonoBehaviour
{
    //The variables
    public Transform playerCam;
    public int rotateSpeed, clampValue;
    public bool ADKeys, movingLeft, movingRight;
    public int targetFramerate;

    //Sets frame rate of game
    void Start()
    {
        Application.targetFrameRate = targetFramerate;
    }

    void Update()
    {
        //If ADKeys is == true, you can rotate the camera left and right with the A and D keys. Leave false if you want to use regular FNAF camera style.
        if (ADKeys == true)
        {
            //When you hold A, the camera will rotate left.
            if (Input.GetKey(KeyCode.A))
            {
                if (playerCam.localRotation == Quaternion.Euler(0, -clampValue, 0))
                {

                }
                else
                {
                    playerCam.Rotate(0, -rotateSpeed, 0);
                }
            }
            //When you hold D, the camera will rotate right.
            if (Input.GetKey(KeyCode.D))
            {
                if (playerCam.localRotation == Quaternion.Euler(0, clampValue, 0))
                {

                }
                else
                {
                    playerCam.Rotate(0, rotateSpeed, 0);
                }
            }
        }
        //If ADKeys is == false, the normal FNAF camera rotation will be in play.
        if (ADKeys == false)
        {
            //If either movingLeft or movingRight is == true, the camera will rotate left or right.
            if (movingLeft == true)
            {
                if (playerCam.localRotation == Quaternion.Euler(0, -clampValue, 0))
                {

                }
                else
                {
                    playerCam.Rotate(0, -rotateSpeed, 0);
                }
            }
            if (movingRight == true)
            {
                if (playerCam.localRotation == Quaternion.Euler(0, clampValue, 0))
                {

                }
                else
                {
                    playerCam.Rotate(0, rotateSpeed, 0);
                }
            }
        }
    }

    public void rotateLeft()
    {
        movingLeft = true;
    }
    public void rotateRight()
    {
        movingRight = true;
    }
    public void stopRotateLeft()
    {
        movingLeft = false;
    }
    public void stopRotateRight()
    {
        movingRight = false;
    }
}

