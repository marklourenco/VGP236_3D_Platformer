using UnityEngine;

public class RotatingPlatform : MonoBehaviour
{
    public float rotationSpeed = 45f;
    public Vector3 rotationAxis = Vector3.up;

    void Update()
    {
        transform.Rotate(rotationAxis, rotationSpeed * Time.deltaTime);
    }
}
