using UnityEngine;

public class Portal : MonoBehaviour
{
    public GameObject puzzle;
    private PuzzleInput puzzleScript;
    private AudioSource woosh;
    
    private BoxCollider box;
    private MeshCollider mesh;

    private GameObject effect;
    public float vanishSpeed = 1f;
    private Vector3 vanishStartSize;
    private Vector3 vanishStep;

    private bool success;
    private bool vanish;
    private bool set;

    void Start()
    {
        box = GetComponent<BoxCollider>();
        mesh = GetComponent<MeshCollider>();
        woosh = GetComponent<AudioSource>();
        puzzleScript = puzzle.GetComponent<PuzzleInput>(); // the puzzle that opens it
        effect = transform.Find("PortalEffect").gameObject;
        vanishStartSize = effect.transform.localScale;
        vanishStep = vanishStartSize * vanishSpeed / 100;
    }

    void Update()
    {
        if (puzzleScript.success && !set)
        {
            success = true;
            vanish = true;
            set = true;
        }

        if(success)
        {
            Success();
        }
    }
    void FixedUpdate()
    {
        if (vanish) // decreasing the vfx size
        {
            effect.transform.localScale -= vanishStep;
            if (effect.transform.localScale.x < 0)
            {
                Destroy(effect);
                vanish = false;
            }
        }
    }

    // on success, play sounds, disabled collider
    void Success()
    {
        box.enabled = false;
        woosh.Play();
        success = false; // flag for calling the function only once
    }

}
