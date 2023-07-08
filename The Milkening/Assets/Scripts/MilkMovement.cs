using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class MilkMovement : MonoBehaviour
{
    [Header ("Sounds")]
    public AudioClip _MilkSlosh;
    public AudioClip _MilkLunge;

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

    private float nextActionTime = 0.0f;
    public float period = 7f;

    private float nextSoundTime = 0.0f;
    private float soundperiod = 1f;


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
        if (Time.time > nextSoundTime && (horizontal > 0 || vertical > 0))
        {
            SoundManager.instance.PlaySound(_MilkSlosh);
            Debug.Log("asd");
            nextSoundTime += soundperiod;
        }
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded &&  Time.time > nextActionTime )
        {

            SoundManager.instance.PlaySound(_MilkLunge);
            nextActionTime += period;
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
