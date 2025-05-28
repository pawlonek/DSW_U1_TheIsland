using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class PuzzleInput : MonoBehaviour
{
    public GameObject button;
    public float zOffset;

    public float sizeX = 1f;
    public float sizeY = 1f;
    public float thiccness = 0.1f;
    public float gap = 0.02f;
    private float buttonXSize;
    private float buttonYSize;

    [HideInInspector]
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

        buttonXSize = (sizeX - 4 * gap) / 5;
        buttonYSize = (sizeY - 4 * gap) / 5;
        float startXpos = -sizeX/2 + buttonXSize/2;
        float startYpos = sizeY/2 - buttonYSize/2;
        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                GameObject buttonObj = Instantiate(button,
                    new Vector3(transform.position.x + startXpos + j * (gap + buttonXSize),
                                transform.position.y + startYpos - i * (gap + buttonYSize),
                                transform.position.z - zOffset),
                    Quaternion.identity);
                buttonObj.transform.localScale = new Vector3(buttonXSize, buttonYSize, thiccness);
                buttonObj.transform.SetParent(transform);
                buttons.Add(buttonObj);
            }
        }
    }

    void Update()
    {
        for (int i = 0; i < 25; i++)
        {
            if(Convert.ToBoolean(solution[i]) != buttons[i].GetComponent<Button>().pressed)
            {
                return;
            }
        }

        if (!success)
            Success();
    }

    void Success()
    {
        successSound.Play();
        success = true;
    }
}
