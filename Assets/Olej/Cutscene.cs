using UnityEngine;
using Unity.Cinemachine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;
using TMPro;

public class Cutscene : MonoBehaviour
{
    public CinemachineCamera fpsCam;
    public CinemachineCamera volcanoCam;
    public CinemachineCamera ufoCam1;
    public CinemachineCamera ufoCam2;
    public CinemachineCamera ufoCam3;

    private CinemachineBasicMultiChannelPerlin fpsCamShake;
    private CinemachineBasicMultiChannelPerlin volcanoCamShake;
    private CinemachineBasicMultiChannelPerlin ufoCamShake1;
    private CinemachineBasicMultiChannelPerlin ufoCamShake2;
    private CinemachineBasicMultiChannelPerlin ufoCamShake3;

    public GameObject volcanoVFX;

    public float blinkDuration = 2f;
    private float halfDuration = 2f;

    public float growDuration = 2f;

    Vector3 smallScale;
    Vector3 mediumScale;
    Vector3 largeScale;

    public GameObject player;

    public GameObject ufo1;
    public GameObject ufo2;
    public GameObject ufo3;

    public GameObject cutsceneEnd;
    private bool credits;
    List<TMP_Text> texts;
    public float textAppearSpeed;

    void Start()
    {
        fpsCamShake = fpsCam.GetComponent<CinemachineBasicMultiChannelPerlin>();
        volcanoCamShake = volcanoCam.GetComponent<CinemachineBasicMultiChannelPerlin>();
        ufoCamShake1 = ufoCam1.GetComponent<CinemachineBasicMultiChannelPerlin>();
        ufoCamShake2 = ufoCam2.GetComponent<CinemachineBasicMultiChannelPerlin>();
        ufoCamShake3 = ufoCam3.GetComponent<CinemachineBasicMultiChannelPerlin>();

        //volcano VFX sizes
        smallScale = new Vector3(0f, 0f, volcanoVFX.transform.localScale.z);
        mediumScale = new Vector3(4f, 4f, volcanoVFX.transform.localScale.z);
        largeScale = new Vector3(8f, 8f, volcanoVFX.transform.localScale.z);

        halfDuration = blinkDuration / 2f;

        texts = new List<TMP_Text>();
    }

    void Update()
    {
        if(credits) // credits flag
        {
            foreach (var text in cutsceneEnd.GetComponentsInChildren<TMP_Text>())
            {
                texts.Add(text); // subscrive credits to the lsit
            }
        }

        foreach (var tmptext in texts) // decreasing the opacity of the credits
        {
            tmptext.color += new Color(0, 0, 0, textAppearSpeed * Time.deltaTime);
        }
    }

    #region signals

    // small camera shake
    public void SmallShake()
    {
        fpsCamShake.enabled = true;
        volcanoCamShake.enabled = true;
        ufoCamShake1.enabled = true;
        ufoCamShake2.enabled = true;
        ufoCamShake3.enabled = true;

        fpsCamShake.AmplitudeGain = 2;
        volcanoCamShake.AmplitudeGain = 2;
        ufoCamShake1.AmplitudeGain = 2;
        ufoCamShake2.AmplitudeGain = 2;
        ufoCamShake3.AmplitudeGain = 2;
    }

    // big camera shake
    public void BigShake()
    {
        fpsCamShake.enabled = true;
        volcanoCamShake.enabled = true;
        ufoCamShake1.enabled = true;
        ufoCamShake2.enabled = true;
        ufoCamShake3.enabled = true;

        fpsCamShake.AmplitudeGain = 4;
        volcanoCamShake.AmplitudeGain = 4;
        ufoCamShake1.AmplitudeGain = 4;
        ufoCamShake2.AmplitudeGain = 4;
        ufoCamShake3.AmplitudeGain = 4;
    }

    // shaking stop
    public void StopShake()
    {
        fpsCamShake.enabled = false;
        volcanoCamShake.enabled = false;
        ufoCamShake1.enabled = false;
        ufoCamShake2.enabled = false;
        ufoCamShake3.enabled = false;
    }

    public void IsVolcanoBlink()
    {
        StartCoroutine(VolcanoBlink());
    }

    private IEnumerator VolcanoBlink()
    {
        float t = 0;
        while (t < halfDuration) // increases the size first
        {
            float lerpFactor = t / halfDuration;
            volcanoVFX.transform.localScale = Vector3.Lerp(smallScale, mediumScale, lerpFactor);
            t += Time.deltaTime;
            yield return null;
        }

        volcanoVFX.transform.localScale = mediumScale;

        t = 0;
        while (t < halfDuration) // decreases the size later
        {
            float lerpFactor = t / halfDuration;
            volcanoVFX.transform.localScale = Vector3.Lerp(mediumScale, smallScale, lerpFactor);
            t += Time.deltaTime;
            yield return null;
        }

        volcanoVFX.transform.localScale = smallScale; // sets scale back to 0, for the big bang
    }

    public void IsVolcanoPillar()
    {
        StartCoroutine(VolcanoPillar());
    }

    private IEnumerator VolcanoPillar()
    {
        Vector3 startScale = Vector3.zero;
        float timer = 0f;

        while (timer < growDuration) // same as whith the short blink/burst, increases the size
        {
            float t = timer / growDuration;
            volcanoVFX.transform.localScale = Vector3.Lerp(startScale, largeScale, t);
            timer += Time.deltaTime;
            yield return null;
        }

        volcanoVFX.transform.localScale = largeScale; // leaves the big size for the rest of the cutscene
    }

    #region UFO spawns
    public void Ufo1()
    {
        ufo1.SetActive(true);
    }
    public void Ufo2()
    {
        ufo2.SetActive(true);
    }
    public void Ufo3()
    {
        ufo3.SetActive(true);
    }
    #endregion

    public void Beam()
    {
        ufo3.transform.Find("ufoLaser").GetComponent<Beam>().IsBeam();
    }

    public void BeamUp()
    {
        player.GetComponent<PlayerController>().gravityValue = 0; // gets rid of gravity first
        StartCoroutine(BeamedUp());
    }

    private IEnumerator BeamedUp()
    {
        float playerVelocity = 0;
        while (true)
        {
            playerVelocity += player.GetComponent<PlayerController>().beamValue * Time.deltaTime;
            Vector3 beamVector = playerVelocity * Vector3.up;
            player.GetComponent<PlayerController>().cc.Move(beamVector * Time.deltaTime); // moves the player upwards
            yield return null;
        }

    }

    // turns all the audosources off
    public void End()
    {
        cutsceneEnd.GetComponent<Image>().enabled = true;
        AudioSource[] allAudioSources1 = ufo1.GetComponents<AudioSource>();
        AudioSource[] allAudioSources2 = ufo2.GetComponents<AudioSource>();
        AudioSource[] allAudioSources3 = ufo3.GetComponents<AudioSource>();

        foreach (AudioSource audioSource in allAudioSources1)
        {
            audioSource.Stop();
        }
        foreach (AudioSource audioSource in allAudioSources2)
        {
            audioSource.Stop();
        }
        foreach (AudioSource audioSource in allAudioSources3)
        {
            audioSource.Stop();
        }
    }

    public void Credits()
    {
        credits = true;
    }
    #endregion
}
