using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneController : MonoBehaviour, IUserAction {

    public GameObject player;
    private bool gameOver = false;
    private int enemyCount = 6;
    private GameObjectFactory mf;


    private void Awake()
    {
        GameDirector director = GameDirector.getInstance();
        director.currentSceneController = this;
        mf = Singleton<GameObjectFactory>.Instance;
        player = mf.getPlayer();
    }

    // Use this for initialization
    void Start () {
	    for(int i = 0; i < enemyCount; i++)
        {
            GameObject gb = mf.getTank();
        }
        Player.destroyEvent += setGameOver;
	}

	void Update () {
   
	}

    public Vector3 getPlayerPos()
    {
        return player.transform.position;
    }

    public bool isGameOver()
    {
        return gameOver;
    }
    public void setGameOver()
    {
        gameOver = true;
    }

    public void moveForward()
    {
        player.GetComponent<Rigidbody>().velocity = player.transform.forward * 20;
    }
    public void moveBackWard()
    {
        player.GetComponent<Rigidbody>().velocity = player.transform.forward * -20;
    }

    public void turn(float offsetX)
    {
        float y = player.transform.localEulerAngles.y + offsetX * 5;
        float x = player.transform.localEulerAngles.x;
        player.transform.localEulerAngles = new Vector3(x, y, 0);
    }

    public void shoot()
    {
        GameObject bullet = mf.getBullet(tankType.Player);
        // 设置子弹位置
        bullet.transform.position = new Vector3(player.transform.position.x, 1.5f, player.transform.position.z) + player.transform.forward * 1.5f;
        // 设置子弹方向
        bullet.transform.forward = player.transform.forward;

        // 发射子弹
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        rb.AddForce(bullet.transform.forward * 20, ForceMode.Impulse);
    }
}
