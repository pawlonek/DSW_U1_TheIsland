using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float mouseSensitivity = 100f;
    private Transform playerTransform;

    private float mouseX;
    private float mouseY;

    float xRotation = 0f;

    public Transform target;         
    public float rotateSpeed = 2f;
    private bool isLooking = false;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        playerTransform = transform.parent.gameObject.transform;
    }

    void Update()
    {
        if(!isLooking)
        {
            mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
            mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);

            transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

            playerTransform.Rotate(Vector3.up * mouseX);
        }
        else
        {
            Vector3 direction = (target.position - transform.position).normalized;
            Quaternion targetRotation = Quaternion.LookRotation(direction);

            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotateSpeed);
        }
    }


    public void StartLooking()
    {
        isLooking = true;
    }

    public void StopLooking()
    {
        isLooking = false;
    }
}
