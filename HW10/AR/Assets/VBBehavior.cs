using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

using System;

public class VBBehavior : MonoBehaviour, IVirtualButtonEventHandler
{
  
    public GameObject gm;
    public Animator ani;
    public VirtualButtonBehaviour[] vbs;
    // Start is called before the first frame update
    void Start()
    {
        vbs = GetComponentsInChildren<VirtualButtonBehaviour>();
        for (int i = 0; i < vbs.Length; i++)
        {
            vbs[i].RegisterEventHandler(this);
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnButtonPressed(VirtualButtonBehaviour vb)
    {
        switch (vb.VirtualButtonName)
        {
            case "fly":
                Debug.Log("fly pressed");
                ani.SetTrigger("Fly");
                break;
            case "run":
                Debug.Log("run pressed");
                ani.SetTrigger("Run");
                break;

        }
    }

    public void OnButtonReleased(VirtualButtonBehaviour vb)
    {
        switch (vb.VirtualButtonName)
        {
            case "fly":
                Debug.Log("fly released");
                ani.SetTrigger("Land");
                break;
            case "run":
                Debug.Log("run released");
                ani.SetTrigger("Walk");
                break;

        }
    }

}
