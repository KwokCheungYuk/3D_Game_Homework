//Actions.cs

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using game;

public class SSAction : ScriptableObject            //动作
{

    public bool enable = true;                      //是否正在进行此动作
    public bool destroy = false;                    //是否需要被销毁

    public GameObject gameobject { get; set; }                   //动作对象
    public Transform transform { get; set; }                     //动作对象的transform
    public ISSActionCallback callback { get; set; }              //回调函数

    protected SSAction() { }                        //保证SSAction不会被new

    public virtual void Start()                    //子类可以使用这两个函数
    {
        throw new System.NotImplementedException();
    }

    public virtual void Update()
    {
        throw new System.NotImplementedException();
    }
}

public class SSMoveToAction : SSAction                        //移动
{
    public SSDirector s1;
    public Vector3 target;        //移动到的目的地
    public float speed;           //移动的速度

    private SSMoveToAction() { }
    public static SSMoveToAction GetSSAction(SSDirector s1, Vector3 target, float speed)
    {
        SSMoveToAction action = ScriptableObject.CreateInstance<SSMoveToAction>();//让unity自己创建一个MoveToAction实例，并自己回收
        action.target = target;
        action.speed = speed;
        action.s1 = s1;
        return action;
    }

    public override void Update()
    {
        this.transform.position = Vector3.MoveTowards(this.transform.position, target, speed * Time.deltaTime);
        if (this.transform.position == target)
        {
            if(this.s1.state == State.LTR)
            {
                this.s1.state = State.Right;
            }
            else if(this.s1.state == State.RTL)
            {
                this.s1.state = State.Left;
            }
            this.destroy = true;
            this.callback.SSActionEvent(this);      //告诉动作管理或动作组合这个动作已完成
        }
    }

    public override void Start()
    {
        //移动动作建立时候不做任何事情
    }
}

public class SSPushAction : SSAction                        //上船
{
    public Vector3 target;        //移动到的目的地

    private SSPushAction() { }
    public static SSPushAction GetSSAction(Vector3 target)
    {
        SSPushAction action = ScriptableObject.CreateInstance<SSPushAction>();//让unity自己创建一个MoveToAction实例，并自己回收
        action.target = target;
        return action;
    }

    public override void Update()
    {
        this.transform.localPosition = target;
        this.destroy = true;
        this.callback.SSActionEvent(this);      //告诉动作管理或动作组合这个动作已完成
    }

    public override void Start()
    {
        //移动动作建立时候不做任何事情
    }
}

public class SSPopAction : SSAction                        //下船
{
    private SSPopAction() { }
    public static SSPopAction GetSSAction(Stack<GameObject> target, GameObject obj)
    {
        SSPopAction action = ScriptableObject.CreateInstance<SSPopAction>();//让unity自己创建一个MoveToAction实例，并自己回收
        target.Push(obj);
        return action;
    }

    public override void Update()
    {
        this.destroy = true;
        this.callback.SSActionEvent(this);      //告诉动作管理或动作组合这个动作已完成
    }

    public override void Start()
    {
        //移动动作建立时候不做任何事情
    }
}

public class SequenceAction : SSAction, ISSActionCallback
{
    public List<SSAction> sequence;    //动作的列表
    public int repeat = -1;            //-1就是无限循环做组合中的动作
    public int start = 0;              //当前做的动作的索引

    public static SequenceAction GetSSAcition(int repeat, int start, List<SSAction> sequence)
    {
        SequenceAction action = ScriptableObject.CreateInstance<SequenceAction>();//让unity自己创建一个SequenceAction实例
        action.repeat = repeat;
        action.sequence = sequence;
        action.start = start;
        return action;
    }

    public override void Update()
    {
        if (sequence.Count == 0) return;
        if (start < sequence.Count)
        {
            sequence[start].Update();     //一个组合中的一个动作执行完后会调用接口,所以这里看似没有start++实则是在回调接口函数中实现
        }
    }

    public void SSActionEvent(SSAction source, SSActionEventType events = SSActionEventType.Competeted,
        int intParam = 0, string strParam = null, Object objectParam = null)
    {
        source.destroy = false;          //先保留这个动作，如果是无限循环动作组合之后还需要使用
        this.start++;
        if (this.start >= sequence.Count)
        {
            this.start = 0;
            if (repeat > 0) repeat--;
            if (repeat == 0)
            {
                this.destroy = true;               //整个组合动作就删除
                this.callback.SSActionEvent(this); //告诉组合动作的管理对象组合做完了
            }
        }
    }

    public override void Start()
    {
        foreach (SSAction action in sequence)
        {
            action.gameobject = this.gameobject;
            action.transform = this.transform;
            action.callback = this;                //组合动作的每个小的动作的回调是这个组合动作
            action.Start();
        }
    }

    void OnDestroy()
    {
        //如果组合动作做完第一个动作突然不要它继续做了，那么后面的具体的动作需要被释放
    }
}

public enum SSActionEventType : int { Started, Competeted }

public interface ISSActionCallback
{
    void SSActionEvent(SSAction source, SSActionEventType events = SSActionEventType.Competeted,
        int intParam = 0, string strParam = null, Object objectParam = null);
}

public class SSActionManager : MonoBehaviour, ISSActionCallback                      //action管理器
{

    private Dictionary<int, SSAction> actions = new Dictionary<int, SSAction>();    //将执行的动作的字典集合,int为key，SSAction为value
    private List<SSAction> waitingAdd = new List<SSAction>();                       //等待去执行的动作列表
    private List<int> waitingDelete = new List<int>();                              //等待删除的动作的key                

    protected void Update()
    {
        foreach (SSAction ac in waitingAdd)
        {
            actions[ac.GetInstanceID()] = ac;                                      //获取动作实例的ID作为key
        }
        waitingAdd.Clear();

        foreach (KeyValuePair<int, SSAction> kv in actions)
        {
            SSAction ac = kv.Value;
            if (ac.destroy)
            {
                waitingDelete.Add(ac.GetInstanceID());
            }
            else if (ac.enable)
            {
                ac.Update();
            }
        }

        foreach (int key in waitingDelete)
        {
            SSAction ac = actions[key];
            actions.Remove(key);
            DestroyObject(ac);
        }
        waitingDelete.Clear();
    }

    public int waitListNum()
    {
        Debug.Log(this.waitingAdd.Count);
        return this.waitingAdd.Count;
    }
    public void RunAction(GameObject gameobject, SSAction action, ISSActionCallback manager)
    {
        action.gameobject = gameobject;
        action.transform = gameobject.transform;
        action.callback = manager;
        waitingAdd.Add(action);
        action.Start();
    }

    public void SSActionEvent(SSAction source, SSActionEventType events = SSActionEventType.Competeted,
        int intParam = 0, string strParam = null, Object objectParam = null)
    {
        //牧师与魔鬼的游戏对象移动完成后就没有下一个要做的动作了，所以回调函数为空
    }
}

public class MySceneActionManager : SSActionManager  //本游戏管理器
{

    private SSMoveToAction moveBoatToEndOrStart;     //移动船到结束岸，移动船到开始岸
    private SSPushAction moveRoleOnShip;     //移动角色到船上
    private SSPopAction moveRoleOffShip;     //移动角色到岸上

    public Model sceneController;

    protected new void Start()
    {
        sceneController = (Model)SSDirector.getInstance().getModel();
        sceneController.actionManager = this;
    }
    public void moveBoat(GameObject boat, SSDirector s1, Vector3 target, float speed)
    {
        moveBoatToEndOrStart = SSMoveToAction.GetSSAction(s1, target, speed);
        this.RunAction(boat, moveBoatToEndOrStart, this);
    }

    public void onShip(GameObject role, Vector3 end_pos)
    {
       moveRoleOnShip = SSPushAction.GetSSAction(end_pos);
       this.RunAction(role, moveRoleOnShip, this);
    }

    public void offShip(GameObject role, Stack<GameObject> target)
    {
        moveRoleOffShip = SSPopAction.GetSSAction(target, role);
        this.RunAction(role, moveRoleOffShip, this);
    }
}