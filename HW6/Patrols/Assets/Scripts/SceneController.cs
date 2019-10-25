using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour, IUserAction, ISceneController
{
    //player随处在的区域
    public int areaSign = -1;
    //龙工厂
    public DragonFactory dragonFactory;
    //水晶工厂
    public CrystalFactory crystalFactory;
    //动作管理器
    public DragonActionManager actionManager;
    // 主相机
    public Camera mainCamera;
    //玩家
    public GameObject player;
    //记分员
    public ScoreController scoreController;
    //龙列表
    private List<GameObject> dragons;
    //水晶列表
    private List<GameObject> crystals;


    private float playerSpeed = 5.0f;
    private float rotateSpeed = 135.0f;
    private bool isGameOver = false;

    // Start is called before the first frame update
    void Start()
    {
        SSDirector director = SSDirector.getInstance();
        director.currentSceneController = this;
        dragonFactory = Singleton<DragonFactory>.GetInstance;
        crystalFactory = Singleton<CrystalFactory>.GetInstance;
        actionManager = gameObject.AddComponent<DragonActionManager>() as DragonActionManager;
        LoadResources();
        mainCamera.GetComponent<CameraMove>().target = player;
        scoreController = Singleton<ScoreController>.GetInstance;
    }

    // Update is called once per frame
    void Update()
    {
        for(int i = 0; i < dragons.Count; i++)
        {
            dragons[i].GetComponent<DragonData>().areaSign = areaSign;
        }
        if(scoreController.crystal_num == 0)
        {
            GameOver();
        }
    }

    private void GameOver()
    {
        isGameOver = true;
        dragonFactory.stopDragon();
        actionManager.DestroyAll();
    }

    public int GetCrystalNum()
    {
        return scoreController.crystal_num;
    }

    public bool GetGameOver()
    {
        return isGameOver;
    }

    public int GetScore()
    {
        return scoreController.score;
    }

    public void MovePlayer(int dir)
    {
        if (!isGameOver)
        {
            player.transform.rotation = Quaternion.Euler(new Vector3(0, dir * 90, 0));
            player.GetComponent<Animator>().SetBool("run", true);
            switch (dir)
            {
                case Diretion.UP:
                    player.transform.position += new Vector3(0, 0, 0.1f);
                    break;
                case Diretion.DOWN:
                    player.transform.position += new Vector3(0, 0, -0.1f);
                    break;
                case Diretion.LEFT:
                    player.transform.position += new Vector3(-0.1f, 0, 0);
                    break;
                case Diretion.RIGHT:
                    player.transform.position += new Vector3(0.1f, 0, 0);
                    break;
            }
        }
    }

    public void Restart()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void LoadResources()
    {
        player = Instantiate(Resources.Load("player"), new Vector3(35, 9, -36), Quaternion.identity) as GameObject;
        crystals = crystalFactory.getCrystals();
        dragons = dragonFactory.getDragons();
        for (int i = 0; i < dragons.Count; i++)
        {
            actionManager.GoDragon(dragons[i]);
        }
    }

    void OnEnable()
    {
        GameEventManager.ScoreChange += AddScore;
        GameEventManager.GameOverChange += GameOver;
        GameEventManager.CrystalChange += ReduceCrystalNumber;
    }

    private void ReduceCrystalNumber()
    {
        scoreController.crystal_num--;
    }

    private void AddScore()
    {
        scoreController.score++;
    }

    void OnDisable()
    {
        GameEventManager.ScoreChange -= AddScore;
        GameEventManager.GameOverChange -= GameOver;
        GameEventManager.CrystalChange -= ReduceCrystalNumber;
    }
}
