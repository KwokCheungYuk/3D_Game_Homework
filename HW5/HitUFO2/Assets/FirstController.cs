using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstController : MonoBehaviour, ISceneController, IUserAction
{
    public MyActionManager action_manager;  //改变
    public DiskFactory UFO_factory;
    public UserGUI user_gui;
    public ScoreRecorder score_recorder;

    public bool isPhysical = false;  //新增

    private Queue<GameObject> UFO_queqe = new Queue<GameObject>();          //游戏场景中的飞碟队列
    private List<GameObject> UFO_notshot = new List<GameObject>();          //没有被打中的飞碟队列
    private int round = 1;                                                   //回合
    private float speed = 1.5f;                                              //发射一个飞碟的时间间隔
    private bool playing_game = false;                                       //游戏中
    private bool game_over = false;                                          //游戏结束
    private bool game_start = false;                                         //游戏开始
    private int score_round2 = 10;                                           //去到第二回合所需分数
    private int score_round3 = 25;                                           //去到第三回合所需分数

    void Start()
    {
        SSDirector director = SSDirector.GetInstance();
        director.CurrentScenceController = this;
        UFO_factory = Singleton<DiskFactory>.Instance;
        score_recorder = Singleton<ScoreRecorder>.Instance;
        action_manager = gameObject.AddComponent<Adapter>() as MyActionManager;  //改变
        user_gui = gameObject.AddComponent<UserGUI>() as UserGUI;
    }

    void Update()
    {
        if (game_start)
        {
            //游戏结束，取消定时发送飞碟
            if (game_over)
            {
                CancelInvoke("LoadResources");
            }
            //设定一个定时器，发送飞碟，游戏开始
            if (!playing_game)
            {
                user_gui.round = round;
                InvokeRepeating("LoadResources", 1f, speed);
                playing_game = true;
            }
            //发送飞碟
            SendUFO();
            //回合升级
            if (score_recorder.score >= score_round2 && round == 1)
            {
                round = 2;
                user_gui.round = round;
                speed = speed - 0.6f;
                CancelInvoke("LoadResources");
                playing_game = false;
            }
            else if (score_recorder.score >= score_round3 && round == 2)
            {
                round = 3;
                user_gui.round = round;
                speed = speed - 0.3f;
                CancelInvoke("LoadResources");
                playing_game = false;
            }
        }
    }

    public void LoadResources()
    {
        UFO_queqe.Enqueue(UFO_factory.GetDisk(round));
    }

    private void SendUFO()
    {
        float position_x = 16;
        if (UFO_queqe.Count != 0)
        {
            GameObject UFO = UFO_queqe.Dequeue();
            UFO_notshot.Add(UFO);
            UFO.SetActive(true);
            float ran_y = Random.Range(1f, 4f);
            float ran_x = Random.Range(-1f, 1f) < 0 ? -1 : 1;
            UFO.GetComponent<DiskData>().direction = new Vector3(ran_x, ran_y, 0);
            Vector3 position = new Vector3(-UFO.GetComponent<DiskData>().direction.x * position_x, ran_y, 0);
            UFO.transform.position = position;
            float power = Random.Range(10f, 15f);
            float angle = Random.Range(15f, 28f);
            action_manager.playDisk(UFO, angle, power, isPhysical); //改变
        }

        for (int i = 0; i < UFO_notshot.Count; i++)
        {
            GameObject temp = UFO_notshot[i];
            if (temp.transform.position.y < -10 && temp.gameObject.activeSelf == true)
            {
                UFO_factory.FreeDisk(UFO_notshot[i]);
                UFO_notshot.Remove(UFO_notshot[i]);
                user_gui.ReduceBlood();
            }
        }
    }

    public void Hit(Vector3 pos)
    {
        Ray ray = Camera.main.ScreenPointToRay(pos);
        RaycastHit[] hits;
        hits = Physics.RaycastAll(ray);
        bool not_hit = false;
        for (int i = 0; i < hits.Length; i++)
        {
            RaycastHit hit = hits[i];
            if (hit.collider.gameObject.GetComponent<DiskData>() != null && game_over == false)
            {
                for (int j = 0; j < UFO_notshot.Count; j++)
                {
                    if (hit.collider.gameObject.GetInstanceID() == UFO_notshot[j].gameObject.GetInstanceID())
                    {
                        not_hit = true;
                    }
                }
                if (!not_hit)
                {
                    return;
                }
                UFO_notshot.Remove(hit.collider.gameObject);
                //记分员记录分数
                score_recorder.Record(hit.collider.gameObject);
                StartCoroutine(WaitingParticle(0.08f, hit, UFO_factory, hit.collider.gameObject));
                break;
            }
        }
    }

    public int GetScore()
    {
        return score_recorder.score;
    }

    public void ReStart()
    {
        game_over = false;
        playing_game = false;
        score_recorder.score = 0;
        round = 1;
        speed = 2f;
    }
 
    public void GameOver()
    {
        game_over = true;
    }

    //回收飞碟
    IEnumerator WaitingParticle(float wait_time, RaycastHit hit, DiskFactory disk_factory, GameObject obj)
    {
        yield return new WaitForSeconds(wait_time);
        hit.collider.gameObject.transform.position = new Vector3(0, -9, 0);
        disk_factory.FreeDisk(obj);
    }
    public void BeginGame()
    {
        game_start = true;
    }
}

