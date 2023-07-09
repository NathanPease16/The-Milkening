using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class MilkMovement : MonoBehaviour
{
    [Header ("Sounds")]
    public AudioClip _MilkLunge;
    private AudioSource source;

    [Header("Attributes")]
    [SerializeField] private float speed;
    [SerializeField] private float lungeAngularVelocity;
    [SerializeField] private float lungeForce;
    [SerializeField] private float lungeCoolDown;
    [SerializeField] private float lungeHeightBias;
    [SerializeField] private float lungeEscapeTime;
    [SerializeField] private float groundCheckRadius;
    [SerializeField] private LayerMask groundCheckMask;
    private float currentEscapeTime;
    private float currentLungeTime;
    public bool isLunging;

    private float nextSoundTime = 0.0f;
    private float soundperiod = 1f;


    [Header("References")]
    private Rigidbody rb;
    private Transform mainCam;
    private Transform groundCheck;
    public Vector3 lungeDir;



    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        mainCam = Camera.main.transform;
        groundCheck = transform.parent.Find("Ground Check");

        rb.maxAngularVelocity = speed;
        currentLungeTime = lungeCoolDown;

        source = GetComponent<AudioSource>();
    }

    private void Update()
    {
        currentLungeTime += Time.deltaTime;
        MoveMilk();
    }

    private void MoveMilk()
    {
        
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");


        bool isGrounded = IsGrounded();

        rb.angularVelocity = mainCam.rotation * new Vector3(vertical * speed, 0, -horizontal * speed);
        if ((horizontal > 0 || vertical > 0) && !source.isPlaying)
            source.Play();
        else if (horizontal == 0 && vertical == 0)
            source.Stop();

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded && currentLungeTime >= lungeCoolDown)
        {

            SoundManager.instance.PlaySound(_MilkLunge);
            Vector3 dir = mainCam.forward;
            dir.y += lungeHeightBias;
            rb.AddForce(dir * lungeForce);
            isLunging = true;
            currentLungeTime = 0;
            currentEscapeTime = 0;

            lungeDir = dir;
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

    public void Launch()
    {
        Vector3 dir = -mainCam.forward;
        dir.y = Mathf.Abs(dir.y);
        dir.y += lungeHeightBias;

        rb.velocity = dir;
        rb.angularVelocity = dir * lungeAngularVelocity;
    }

    private bool IsGrounded()
    {
        return Physics.CheckSphere(groundCheck.position, groundCheckRadius, groundCheckMask);
    }
}
