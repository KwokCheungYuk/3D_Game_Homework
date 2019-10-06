using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreRecorder : MonoBehaviour
{
    public int score;                  
    void Start()
    {
        score = 0;
    }

    public void Record(GameObject disk)
    {
        int temp = disk.GetComponent<DiskData>().score;
        score = temp + score;
        //Debug.Log(score);
    }

    public void Reset()
    {
        score = 0;
    }
}
