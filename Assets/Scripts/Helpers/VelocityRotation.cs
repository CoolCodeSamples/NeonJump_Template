using UnityEngine;

public class VelocityRotation : MonoBehaviour
{
    
    [SerializeField] private float positiveRotation = 30f;
    [SerializeField] private float negativeRotation = -30f;
    [SerializeField] private float rotationSpeed = 30f;

    private Rigidbody rb;
    private Quaternion targetRotation;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        float velocityY = rb.velocity.y;
        float targetRotationAngle = velocityY > 0 ? positiveRotation : negativeRotation;
        float rotationAmount = rotationSpeed * Time.deltaTime;


        targetRotation = Quaternion.Euler(0f, 0f, targetRotationAngle);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationAmount);
    }
}
