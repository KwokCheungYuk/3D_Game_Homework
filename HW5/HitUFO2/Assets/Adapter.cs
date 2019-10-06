using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Adapter : MonoBehaviour, MyActionManager
{
    public FlyActionManager flyManager;
    public PhysicalActionManager physicalManager;
    public void playDisk(GameObject disk, float angle, float power, bool isPhysical)
    {
        if (isPhysical)
        {
            physicalManager.UFOFly(disk, angle, power);
        }
        else
        {
            flyManager.UFOFly(disk, angle, power);
        }
    }
    // Use this for initialization
    void Start()
    {
        flyManager = gameObject.AddComponent<FlyActionManager>() as FlyActionManager;
        physicalManager = gameObject.AddComponent<PhysicalActionManager>() as PhysicalActionManager;
    }

}
