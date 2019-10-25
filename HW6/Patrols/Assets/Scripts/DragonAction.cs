using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonAction : SSAction
{
    private enum Direction { East, North, West, South};
    //移动前坐标
    private float posX, posZ;
    //移动速度
    private float speed = 1.2f;
    //移动距离
    private float moveDistance;
    //是否到达目标
    private bool isArrived = true;
    //移动方向
    private Direction direction = Direction.East;
    //龙的数据
    private DragonData data;

    private DragonAction() { }

    public static DragonAction getSSAction(Vector3 location)
    {
        DragonAction action = CreateInstance < DragonAction>();
        action.posX = location.x;
        action.posZ = location.z;
        action.moveDistance = UnityEngine.Random.Range(4, 10);
        return action;
    }

    private void goDragon()
    {
        if(isArrived == true)
        {
            switch (direction)
            {
                case Direction.East:
                    posX -= moveDistance;
                    break;
                case Direction.North:
                    posZ += moveDistance;
                    break;
                case Direction.West:
                    posX += moveDistance;
                    break;
                case Direction.South:
                    posZ -= moveDistance;
                    break;
            }
            isArrived = false;
        }
        this.transform.LookAt(new Vector3(posX, 0, posZ));
        float distance = Vector3.Distance(transform.position, new Vector3(posX, 0, posZ));
        if(distance > 0.9)
        {
            transform.position = Vector3.MoveTowards(this.transform.position, new Vector3(posX, 0, posZ), speed * Time.deltaTime);
        }
        else
        {
            direction += 1;
            if (direction > Direction.South)
            {
                direction = Direction.East;
            }
            isArrived = true;
        }
    }

    public override void Start()
    {
        this.gameObject.GetComponent<Animator>().SetBool("run", true);
        data = this.gameObject.GetComponent<DragonData>();
    }

    public override void Update()
    {
        //防止碰撞后旋转
        if(transform.localEulerAngles.x != 0 || transform.localEulerAngles.z != 0)
        {
            transform.localEulerAngles = new Vector3(0, transform.localEulerAngles.y, 0);
        }
        if(transform.position.y != 0)
        {
            transform.position = new Vector3(transform.position.x, 0, transform.position.z);
        }

        goDragon();

        if(data.ifFollowPlayer == true && data.areaSign == data.sign)
        {
            Debug.Log("Detect !!!");
            this.destroy = true;
            this.callback.SSActoinEvent(this, 0, this.gameObject);
        }
    }

}
