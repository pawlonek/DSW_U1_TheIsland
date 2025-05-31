using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    [HideInInspector]
    public CharacterController cc;
    public float walkSpeed = 5;
    public float runSpeed = 7.5f;
    public float jumpForce = 3;
    private Vector3 moveVector;
    private Vector3 playerVelocity;
    public float gravityValue = -9.81f;
    public float beamValue = 4.905f;
    private Vector3 speedAtJump;
    private Vector3 input;
    private Vector3 airMomentum;

    public bool canMove = true;
    public float airSlow = 0.5f;

    [HideInInspector]
    public Camera cam;
    public LayerMask buttonMask;

    private AudioSource footsteps;
    public AudioClip[] clips;

    void Start()
    {
        cc = GetComponent<CharacterController>();
        cam = transform.Find("Camera").gameObject.GetComponent<Camera>();
        footsteps = GetComponent<AudioSource>();
    }

    void Update()
    {
        moveVector = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        moveVector = Vector3.ClampMagnitude(moveVector, 1f);
        moveVector = transform.TransformDirection(moveVector);

        if (canMove)
        {
            if(cc.isGrounded)
            {
                if(Input.GetKey(KeyCode.LeftShift))
                {
                    if(moveVector != Vector3.zero && !footsteps.isPlaying)
                    {
                        int randomIndex = Random.Range(0, clips.Length);
                        AudioClip randomClip = clips[randomIndex];

                        float randomPitch = Random.Range(0.96f, 1.04f);
                        footsteps.pitch = randomPitch;
                        footsteps.clip = randomClip;
                        footsteps.Play();
                    }
                    moveVector = moveVector * runSpeed;
                }
                else
                {
                    if (moveVector != Vector3.zero && !footsteps.isPlaying)
                    {
                        int randomIndex = Random.Range(0, clips.Length);
                        AudioClip randomClip = clips[randomIndex];

                        float randomPitch = Random.Range(0.71f, 0.79f);
                        footsteps.pitch = randomPitch;
                        footsteps.clip = randomClip;
                        footsteps.Play();
                    }
                    moveVector = moveVector * walkSpeed;
                }
            }
            else
            {
                moveVector = moveVector * walkSpeed;
            }

            if (Input.GetButtonDown("Jump") && cc.isGrounded)
            {
                speedAtJump = moveVector;
                playerVelocity.y = jumpForce;

                Vector3 desiredVelocity = moveVector * speedAtJump.magnitude;
                airMomentum = Vector3.MoveTowards(airMomentum, desiredVelocity, airSlow * Time.deltaTime);
                moveVector = new Vector3(airMomentum.x, 0, airMomentum.z);
            }

            playerVelocity.y += gravityValue * Time.deltaTime;

            Vector3 finalMoveVector = moveVector + (playerVelocity.y * Vector3.up);
            cc.Move(finalMoveVector * Time.deltaTime);

            if (cc.isGrounded && moveVector != Vector3.zero && !footsteps.isPlaying)
            {
                int randomIndex = Random.Range(0, clips.Length);
                AudioClip randomClip = clips[randomIndex];

                footsteps.clip = randomClip;
                footsteps.Play();
            }

            if (Input.GetMouseButtonDown(0))
            {
                RaycastHit hit;
                Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));

                if (Physics.Raycast(ray, out hit, Mathf.Infinity, buttonMask))
                {
                    if(hit.transform.tag == "Button")
                    {
                        hit.transform.GetComponent<Button>().Pressed();
                    }
                }
            }
        }
    }
    public void StopMoving()
    {
        canMove = false;
    }
}
