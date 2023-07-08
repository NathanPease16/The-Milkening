using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [Header("Camera Position")]
    [SerializeField] private Vector3 offset;
    [SerializeField] private float distFromClip;

    [Header("Camera Rotation")]
    [SerializeField] private float rotateSpeed;
    [SerializeField] private LayerMask sanityLayerMask;
    private Transform pivot;
    private float originalDistance;


    [Header("References")]
    private Transform mainCam;

    private void Awake()
    {
        pivot = transform.Find("Pivot");

        mainCam = Camera.main.transform;
        mainCam.parent = pivot;
        mainCam.localPosition = offset;

        originalDistance = Vector3.Distance(transform.position, mainCam.transform.position);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        RotateCamera();
        CameraSanityCheck();
    }

    private void RotateCamera()
    {
        float mouseX = Input.GetAxisRaw("Mouse X") * rotateSpeed * Time.deltaTime;
        float mouseY = Input.GetAxisRaw("Mouse Y") * rotateSpeed * Time.deltaTime;

        Vector3 rotationAngles = new Vector3(-mouseY, mouseX, 0);
        Vector3 newRotation = pivot.rotation.eulerAngles + rotationAngles;

        if (newRotation.x > 180f)
            newRotation.x -= 360f;

        newRotation.x = Mathf.Clamp(newRotation.x, -90f, 90f);

        pivot.rotation = Quaternion.Euler(newRotation);
    }

    private void CameraSanityCheck()
    {
        Vector3 direction = (mainCam.position - transform.position).normalized;

        if (Physics.Raycast(transform.position, direction, out RaycastHit hit, originalDistance, sanityLayerMask, QueryTriggerInteraction.Ignore))
        {
            Debug.DrawRay(transform.position, direction * originalDistance, Color.red);
            mainCam.position = hit.point - direction * distFromClip;
        }
        else
        {
            Debug.DrawRay(transform.position, direction * originalDistance, Color.green);
            mainCam.localPosition = offset;
        }
    }
}
