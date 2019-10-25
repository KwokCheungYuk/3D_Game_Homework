using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonActionManager : SSActionManager
{

    private DragonAction dragonAction;

    public void GoDragon(GameObject dragon)
    {
        dragonAction = DragonAction.getSSAction(dragon.transform.position);
        this.RunAction(dragon, dragonAction, this);
    }

}