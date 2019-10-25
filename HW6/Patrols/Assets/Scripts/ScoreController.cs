using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreController : MonoBehaviour
{
    public SceneController sceneController;
    public int score = 0;
    public int crystal_num = 12;
    // Start is called before the first frame update
    void Start()
    {
        sceneController = (SceneController)SSDirector.getInstance().currentSceneController;
        sceneController.scoreController = this;
    }

    void AddScore()
    {
        score++;
    }
}
