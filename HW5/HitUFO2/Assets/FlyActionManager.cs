using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyActionManager : SSActionManager
{

    public UFOFlyAction fly;                            //飞碟飞行的动作
    //public FirstController scene_controller;             //当前场景的场景控制器

    protected void Start()
    {
        //scene_controller = (FirstController)SSDirector.GetInstance().CurrentScenceController;
       // scene_controller.action_manager = this;
    }

    public void UFOFly(GameObject UFO, float angle, float power)
    {
        fly = UFOFlyAction.GetSSAction(UFO.GetComponent<DiskData>().direction, angle, power);
        this.RunAction(UFO, fly, this);
    }
}
