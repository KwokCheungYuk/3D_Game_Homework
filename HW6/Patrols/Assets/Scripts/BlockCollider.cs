using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockCollider : MonoBehaviour
{
    public int sign = 0;
    SceneController sceneController;

    // Start is called before the first frame update
    void Start()
    {
        sceneController = SSDirector.getInstance().currentSceneController as SceneController;
    }

    // Update is called once per frame
    void Update()
    {
        sceneController = SSDirector.getInstance().currentSceneController as SceneController;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            sceneController.areaSign = sign;
        }
    }
}
