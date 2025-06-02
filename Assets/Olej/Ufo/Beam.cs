using UnityEngine;
using System.Collections;

public class Beam : MonoBehaviour
{
    public float rotationSpeed = 180.0f;
    private Vector3 rotationAxis = Vector3.forward;

    public float growDuration = 4f;
    Vector3 smallScale;
    Vector3 largeScale;

    void Start()
    {
        smallScale = new Vector3(0f, 0f, transform.localScale.z);
        largeScale = new Vector3(100f, 100f, transform.localScale.z);
    }

    void Update()
    {
        transform.Rotate(rotationAxis * rotationSpeed * Time.deltaTime); // rotation
    }

    #region signals

    public void IsBeam()
    {
        StartCoroutine(BeamGrow());
    }

    // size increase 
    private IEnumerator BeamGrow()
    {
        float timer = 0f;

        while (timer < growDuration)
        {
            float t = timer / growDuration;
            transform.localScale = Vector3.Lerp(smallScale, largeScale, t);
            timer += Time.deltaTime;
            yield return null;
        }

        transform.localScale = largeScale;
    }

    #endregion
}
