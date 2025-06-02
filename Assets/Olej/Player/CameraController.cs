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
        Cursor.lockState = CursorLockMode.Locked; // locks the cursor to the centre
        playerTransform = transform.parent.gameObject.transform;
    }

    void Update()
    {
        if(!isLooking) // cutscene flag, no aiming when it's being played
        {
            mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
            mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);

            transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

            playerTransform.Rotate(Vector3.up * mouseX);
        }
        else // looks at the volcano at the begining of the cutscene, then the cinemachine takes over
        {
            Vector3 direction = (target.position - transform.position).normalized;
            Quaternion targetRotation = Quaternion.LookRotation(direction);

            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotateSpeed);
        }
    }

    #region signals
    public void StartLooking()
    {
        isLooking = true;
    }

    public void StopLooking()
    {
        isLooking = false;
    }
    #endregion
}
