using UnityEngine;

public class OrthoSmoothFollow : MonoBehaviour
{
    public Transform target;
    public float smoothTime = 0.3f;
    public Vector3 offset = new Vector3(0, 5, -10); // Adjust this offset to your liking

    private Vector3 velocity = Vector3.zero;

    void Update()
    {
        // Apply the offset to the camera's position relative to the target.
        Vector3 goalPos = target.position + offset;
        transform.position = Vector3.SmoothDamp(transform.position, goalPos, ref velocity, smoothTime);
    }
}
