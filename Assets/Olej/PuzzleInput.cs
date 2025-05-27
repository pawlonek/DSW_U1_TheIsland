using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class PuzzleInput : MonoBehaviour
{
    int[][] solution = new int[][]{
        new int[] {0, 1, 0, 0, 0},
        new int[] {0, 1, 0, 0, 0},
        new int[] {0, 1, 1, 1, 0},
        new int[] {0, 0, 0, 1, 0},
        new int[] {0, 0, 0, 1, 0}
    };

    public List<GameObject> row1 = new List<GameObject>();
    public List<GameObject> row2 = new List<GameObject>();
    public List<GameObject> row3 = new List<GameObject>();
    public List<GameObject> row4 = new List<GameObject>();
    public List<GameObject> row5 = new List<GameObject>();

    List<List<GameObject>> lists = new List<List<GameObject>>();

    public bool success;

    void Start()
    {
        lists.Add(row1);
        lists.Add(row2);
        lists.Add(row3);
        lists.Add(row4);
        lists.Add(row5);
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                if(Convert.ToBoolean(solution[i][j]) != lists[i][j].GetComponent<Button>().pressed)
                {
                    return;
                }
            }
        }

        success = true;
    }
}
