using UnityEngine;

public class MonsterController : MonoBehaviour
{
    public enum WindowPosition { Left, Right, Front, Ocean }

    public Transform leftWindowPos;
    public Transform rightWindowPos;
    public Transform frontWindowPos;
    public Transform oceanPos;

    public float minTimeAtWindow = 3f;
    public float maxTimeAtWindow = 7f;
    public float lightReactionTime = 2f;

    private WindowPosition currentWindow = WindowPosition.Ocean;
    private float windowTimer;
    private float lightExposureTimer;
    private bool isExposedToLight;

    private void Start()
    {
        ReturnToOcean();
    }

    private void Update()
    {
        if (currentWindow == WindowPosition.Ocean)
        {
            windowTimer -= Time.deltaTime;
            if (windowTimer <= 0)
            {
                MoveToRandomWindow();
            }
        }
        else if (isExposedToLight)
        {
            lightExposureTimer -= Time.deltaTime;
            if (lightExposureTimer <= 0)
            {
                ReturnToOcean();
            }
        }
        else
        {
            windowTimer -= Time.deltaTime;
            if (windowTimer <= 0)
            {
                ReturnToOcean();
            }
        }
    }

    public void OnLightExposed(WindowPosition window)
    {
        if (currentWindow == window)
        {
            isExposedToLight = true;
            lightExposureTimer = lightReactionTime;
        }
    }

    public void OnLightRemoved()
    {
        isExposedToLight = false;
    }

    private void MoveToRandomWindow()
    {
        WindowPosition newWindow;
        do
        {
            newWindow = (WindowPosition)Random.Range(0, 3); 
        } while (newWindow == currentWindow);

        currentWindow = newWindow;
        windowTimer = Random.Range(minTimeAtWindow, maxTimeAtWindow);

        switch (currentWindow)
        {
            case WindowPosition.Left:
                transform.position = leftWindowPos.position;
                break;
            case WindowPosition.Right:
                transform.position = rightWindowPos.position;
                break;
            case WindowPosition.Front:
                transform.position = frontWindowPos.position;
                break;
        }
    }

    private void ReturnToOcean()
    {
        currentWindow = WindowPosition.Ocean;
        windowTimer = Random.Range(minTimeAtWindow, maxTimeAtWindow);
        isExposedToLight = false;
        transform.position = oceanPos.position;
    }
}