using UnityEngine;

public class Button : MonoBehaviour
{
    private Material material;
    private Color defaultColor = Color.white;
    private Color pressedColor = Color.blue;
    private Color correctColor = Color.green;

    public float intensity = 8f;

    public bool pressed;
    public bool success;
    PuzzleInput parentScript;

    void Start()
    {
        parentScript = transform.parent.gameObject.GetComponent<PuzzleInput>();
        material = GetComponent<Renderer>().material;
        material.SetColor("_EmissionColor", defaultColor * intensity);
    }

    private void Update()
    {
        if (parentScript.success) // only true when all button are correct
            Correct();
    }

    // swaping color when pressed
    public void Pressed()
    {
        if(!success)
        {
        if(!pressed)
            material.SetColor("_EmissionColor", pressedColor * intensity);
        else
            material.SetColor("_EmissionColor", defaultColor * intensity);
        }

        pressed = !pressed;
    }

    // changing color to green
    public void Correct()
    {
        material.SetColor("_EmissionColor", correctColor * intensity);
        success = true;
    }
}
