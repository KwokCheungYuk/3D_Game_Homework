using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicalActionManager : SSActionManager
{
    public PhysicalUFOAction fly;                            //飞碟飞行的动作
    protected void Start()
    {
    }
    //飞碟飞行
    public void UFOFly(GameObject UFO, float angle, float power)
    {
        fly = PhysicalUFOAction.GetSSAction(UFO.GetComponent<DiskData>().direction, angle, power);
        this.RunAction(UFO, fly, this);
    }
}
