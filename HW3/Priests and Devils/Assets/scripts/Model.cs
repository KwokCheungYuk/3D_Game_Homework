using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using game;

public class Model : MonoBehaviour, ISceneController
{
    public MySceneActionManager actionManager;   //动作管理
    //存放左边的牧师
    Stack<GameObject> leftPriests = new Stack<GameObject>();
    //存放右边的牧师
    Stack<GameObject> rightPriests = new Stack<GameObject>();
    //存放左边的恶魔
    Stack<GameObject> leftDevils = new Stack<GameObject>();
    //存放右边的恶魔
    Stack<GameObject> rightDevils = new Stack<GameObject>();
    Judgment judegement;

    //船上的两个位置
    GameObject[] ship = new GameObject[2];
    GameObject ship_obj;
    public float speed = 5.0f;

    SSDirector s1;

    Vector3 shipLeftPostion = new Vector3(-3f, 0.5f, 0.0f);
    Vector3 shipRightPostion = new Vector3(3f, 0.5f, 0);
    Vector3 sideLeftPostion = new Vector3(-14f, -5f, 0);
    Vector3 sideRightPostion = new Vector3(14f, -5f, 0);
    Vector3 priestsLeftPostion = new Vector3(-19f, 2.2f, 0);
    Vector3 priestsRightPostion = new Vector3(16f, 2.2f, 0);
    Vector3 DevilsLeftPostion = new Vector3(-12f, 2.0f, 0);
    Vector3 DevilsRightPostion = new Vector3(7f, 2.0f, 0);
    Vector3 waterPostion = new Vector3(0f, -5f, 0);

    public void load()
    {
        //加载两岸
        Instantiate(Resources.Load("Side"), sideLeftPostion, Quaternion.identity);
        Instantiate(Resources.Load("Side"), sideRightPostion, Quaternion.identity);

        //加载水
        Instantiate(Resources.Load("Water"), waterPostion, Quaternion.identity);

        //加载船
        ship_obj = Instantiate(Resources.Load("Ship"), shipLeftPostion, Quaternion.identity) as GameObject;

        //加载牧师和恶魔
        for (int i = 0; i < 3; i++)
        {
            leftPriests.Push(Instantiate(Resources.Load("Priest")) as GameObject);
            leftDevils.Push(Instantiate(Resources.Load("Devil")) as GameObject);
        }
    }

    //用于设置牧师和恶魔的位置
    void setTargetPosition(Stack<GameObject> target, Vector3 position)
    {
        GameObject[] temp = target.ToArray();
        for (int i = 0; i < target.Count; i++)
        {
            temp[i].transform.position = position + new Vector3(2.0f * i, 0f, 0f);
        }
    }

    //判断船上有没有空位以及空位的位置
    int shipEmpty()
    {
        int empty = 0;
        for (int i = 0; i < 2; i++)
        {
            if (ship[i] == null)
            {
                empty++;
            }
        }
        return empty;
    }

    //上船操作
    void getOnShip(GameObject obj)
    {
        obj.transform.parent = ship_obj.transform;
        if (shipEmpty() != 0)
        {
            if (ship[0] == null)
            {
                ship[0] = obj;
                if (obj.name == "Priest(Clone)")
                {
                    //obj.transform.localPosition = new Vector3(-0.3f, 2.9f, 0);
                    actionManager.onShip(obj, new Vector3(-0.3f, 2.9f, 0));  //动作分离版本改变
                }
                else
                {
                    //obj.transform.localPosition = new Vector3(-0.4f, 2.5f, 0);
                    actionManager.onShip(obj, new Vector3(-0.4f, 2.5f, 0));  //动作分离版本改变
                }
            }
            else
            {
                ship[1] = obj;
                if (obj.name == "Priest(Clone)")
                {
                    //obj.transform.localPosition = new Vector3(0.3f, 2.9f, 0);
                    actionManager.onShip(obj, new Vector3(0.3f, 2.9f, 0));  //动作分离版本改变
                }
                else
                {
                    obj.transform.localPosition = new Vector3(0.4f, 2.5f, 0);
                    actionManager.onShip(obj, new Vector3(0.4f, 2.5f, 0));  //动作分离版本改变
                }
            }
        }
    }

    //船移动
    public void shipMove()
    {
        //有人在船上才能移动
        if (shipEmpty() != 2)
        {
            if (s1.state == State.Left)
            {
                s1.state = State.LTR;
                actionManager.moveBoat(ship_obj, s1, shipRightPostion, speed);//动作分离
            }
            else if (s1.state == State.Right)
            {
                s1.state = State.RTL;
                actionManager.moveBoat(ship_obj, s1,shipLeftPostion, speed);//动作分离
            }
        }
    }

    //下船
    public void offShip(int side)
    {
        if (ship[side] != null)
        {
            ship[side].transform.parent = null;
            if (s1.state == State.Left)
            {
                if (ship[side].name == "Priest(Clone)")
                {
                    //leftPriests.Push(ship[side]);
                    actionManager.offShip(ship[side], leftPriests); //动作分离
                }
                else
                {
                    //leftDevils.Push(ship[side]);
                    actionManager.offShip(ship[side], leftDevils); //动作分离
                }
            }
            else if (s1.state == State.Right)
            {
                if (ship[side].name == "Priest(Clone)")
                {
                    //rightPriests.Push(ship[side]);
                    actionManager.offShip(ship[side], rightPriests); //动作分离
                }
                else
                {
                    //rightDevils.Push(ship[side]);
                    actionManager.offShip(ship[side], rightDevils); //动作分离
                }
            }
            ship[side] = null;
        }
    }

    public void restart()
    {
        s1.state = State.Left;
        ship_obj.transform.position = shipLeftPostion;
        int rightNumPriest = rightPriests.Count;
        int rightNumDevil = rightDevils.Count;
        for (int i = 0; i < rightNumPriest; i++)
        {
            leftPriests.Push(rightPriests.Pop());
        }
        for (int i = 0; i < rightNumDevil; i++)
        {
            leftDevils.Push(rightDevils.Pop());
        }
        offShip(0);
        offShip(1);
    }

    //从岸到船
    public void priestLeftOnBoat()
    {
        if (leftPriests.Count != 0 && shipEmpty() != 0 && s1.state == State.Left)
        {
            getOnShip(leftPriests.Pop());
        }
    }

    public void priestRightOnBoat()
    {
        if (rightPriests.Count != 0 && shipEmpty() != 0 && s1.state == State.Right)
        {
            getOnShip(rightPriests.Pop());
        }
    }

    public void devilLeftOnBoat()
    {
        if (leftDevils.Count != 0 && shipEmpty() != 0 && s1.state == State.Left)
        {
            getOnShip(leftDevils.Pop());
        }
    }

    public void devilRightOnBoat()
    {
        if (rightDevils.Count != 0 && shipEmpty() != 0 && s1.state == State.Right)
        {
            getOnShip(rightDevils.Pop());
        }
    }
    
    // Use this for initialization
    void Start()
    { 
        s1 = SSDirector.getInstance();
        s1.setModel(this);
        load();
        judegement = new Judgment(leftPriests, rightPriests, leftDevils, rightDevils, ship, s1);
        actionManager = gameObject.AddComponent<MySceneActionManager>() as MySceneActionManager;
    }

    // Update is called once per frame
    void Update()
    {
        setTargetPosition(leftPriests, priestsLeftPostion);
        setTargetPosition(rightPriests, priestsRightPostion);
        setTargetPosition(leftDevils, DevilsLeftPostion);
        setTargetPosition(rightDevils, DevilsRightPostion);
        s1.state = judegement.Judge();
    }
}
