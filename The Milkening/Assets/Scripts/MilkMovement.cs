using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class MilkMovement : MonoBehaviour
{
    [Header("Attributes")]
    [SerializeField] private float speed;
    [SerializeField] private float lungeAngularVelocity;
    [SerializeField] private float lungeForce;
    [SerializeField] private float lungeHeightBias;
    [SerializeField] private float lungeEscapeTime;
    [SerializeField] private float groundCheckRadius;
    [SerializeField] private LayerMask groundCheckMask;
    private float currentEscapeTime;
    private bool isLunging;

    [Header("References")]
    private Rigidbody rb;
    private Transform mainCam;
    private Transform groundCheck;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        mainCam = Camera.main.transform;
        groundCheck = transform.parent.Find("Ground Check");

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

        bool isGrounded = IsGrounded();

        rb.angularVelocity = mainCam.rotation * new Vector3(vertical * speed, 0, -horizontal * speed);

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Vector3 dir = mainCam.forward;
            dir.y += lungeHeightBias;
            rb.AddForce(dir * lungeForce);
            isLunging = true;
            currentEscapeTime = 0;
        }

        if (isLunging)
        {
            rb.angularVelocity = mainCam.forward * lungeAngularVelocity;
            if (isGrounded && currentEscapeTime >= lungeEscapeTime)
                isLunging = false;
            currentEscapeTime += Time.deltaTime;
        }

        transform.parent.position = transform.position;
        transform.localPosition = Vector3.zero;
    }

    private bool IsGrounded()
    {
        return Physics.CheckSphere(groundCheck.position, groundCheckRadius, groundCheckMask);
    }
}