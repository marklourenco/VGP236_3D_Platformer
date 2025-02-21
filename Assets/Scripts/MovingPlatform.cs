using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public float speed = 4f;
    public float lifespan = 5f;
    public Vector3 direction = Vector3.right;

    // private float timer;

    void Start()
    {
        Destroy(gameObject, lifespan);
    }

    void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime);
    }
}
