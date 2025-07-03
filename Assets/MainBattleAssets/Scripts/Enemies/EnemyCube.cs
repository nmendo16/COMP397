using UnityEngine;

public class EnemyCube : MonoBehaviour
{
    public float rotationSpeed = 100f; // Speed for continuous rotation

    void Start()
    {
    }

    void Update()
    {
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);

    }

}