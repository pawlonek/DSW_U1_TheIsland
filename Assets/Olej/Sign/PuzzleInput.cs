using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.Playables;

public class PuzzleInput : MonoBehaviour
{
    public GameObject button; // button prefab
    
    public float zOffset;
    public float sizeX = 1f;
    public float sizeY = 1f;
    public float thiccness = 0.1f;
    public float gap = 0.02f;
    private float buttonXSize;
    private float buttonYSize;
    public GameObject sound;

    public GameObject cutscene;

    [HideInInspector] // the solution, editable in inspector
    public bool[] solution = new bool[]{
        false, true, false, false, false,
        false, true, false, false, false,
        false, true, true, true, false,
        false, false, false, true, false,
        false, false, false, true, false,
    };

    private List<GameObject> buttons = new List<GameObject>();

    private AudioSource successSound;

    [HideInInspector]
    public bool success;

    void Start()
    {
        successSound = GetComponent<AudioSource>();

        // some variables for the button spawning
        buttonXSize = (sizeX - 4 * gap) / 5;
        buttonYSize = (sizeY - 4 * gap) / 5;
        float startXpos = -sizeX/2 + buttonXSize/2;
        float startYpos = sizeY/2 - buttonYSize/2;
        //
        for (int i = 0; i < 5; i++) // button spawning relative to the sign position they're on
        {
            for (int j = 0; j < 5; j++) 
            {
                GameObject buttonObj = Instantiate(button,
                    transform.position,
                    transform.rotation);
                buttonObj.transform.SetParent(transform);
                buttonObj.transform.localPosition = new Vector3(
                                startXpos + j * (gap + buttonXSize),
                                startYpos - i * (gap + buttonYSize),
                                - zOffset
                                );
                buttonObj.transform.localScale = new Vector3(buttonXSize, buttonYSize, thiccness);
                buttons.Add(buttonObj); // subscribing to the list of buttons for solution check
            }
        }
    }

    void Update()
    {
        for (int i = 0; i < 25; i++) // checking weather all buttons are correct
        {
            if(Convert.ToBoolean(solution[i]) != buttons[i].GetComponent<Button>().pressed)
            {
                return; // doesn't let the function continue if not solved
            }
        }

        if (!success) // technically not neccessary, just defensive programming
        {
            Success();
            if (cutscene) // only true on the sign with cutscene GameObject set
            {
                sound.GetComponent<AudioSource>().Stop();
                cutscene.GetComponent<PlayableDirector>().Play();
            }
        }
    }

    // playes the sounds, and sets the flag for buttons
    void Success()
    {
        successSound.Play();
        success = true;
    }
}
