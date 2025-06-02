using UnityEngine;

public class Ufo : MonoBehaviour
{
    private Transform mesh;
    public float rotationSpeed = 180.0f;
    private Vector3 rotationAxis = Vector3.up;

    public GameObject player;
    public Vector3 offset;
    private Vector3 endPos;
    public float speed;
    public float stopDistance = 1;
    private bool arrived;

    void Start()
    {
        mesh = transform.Find("UfoMesh").gameObject.transform;
        endPos = player.transform.position + offset; // the position the UFO will stop at, relative to the player
    }

    void Update()
    {
        mesh.Rotate(rotationAxis * rotationSpeed * Time.deltaTime);

        if (arrived) // only move when didn't reach destination
            return;

        transform.position = Vector3.MoveTowards(transform.position, endPos, speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, endPos) <= stopDistance)
            arrived = true;
    }
}
