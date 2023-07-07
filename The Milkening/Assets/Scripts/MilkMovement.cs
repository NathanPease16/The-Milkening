using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class MilkMovement : MonoBehaviour
{
    [Header("Attributes")]
    [SerializeField] private float speed;

    [Header("References")]
    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.maxAngularVelocity = speed;
    }

    private void Update()
    {
        MoveMilk();
    }

    private void MoveMilk()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        rb.angularVelocity = Camera.main.transform.rotation * new Vector3(vertical * speed, 0, -horizontal * speed);

        transform.parent.position = transform.position;
        transform.localPosition = Vector3.zero;
    }
}
