using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserInterface : MonoBehaviour
{
    private IUserAction action;
    private GUIStyle scoreStyle = new GUIStyle();
    private GUIStyle textStyle = new GUIStyle();
    private GUIStyle overStyle = new GUIStyle();
    private int showTime = 8;

    private void Start()
    {
        action = SSDirector.getInstance().currentSceneController as IUserAction;
        scoreStyle.normal.textColor = new Color(255, 0, 0, 1);
        scoreStyle.fontSize = 16;
        textStyle.normal.textColor = new Color(0, 255, 0, 1);
        textStyle.fontSize = 16;
        overStyle.normal.textColor = new Color(255, 0, 0, 1);
        overStyle.fontSize = 25;
        StartCoroutine(showUsage());

    }

    private IEnumerator showUsage()
    {
        while (showTime >= 0)
        {
            yield return new WaitForSeconds(1);
            showTime--;
        }
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            action.MovePlayer(Diretion.UP);
        }
        if (Input.GetKey(KeyCode.S))
        {
            action.MovePlayer(Diretion.DOWN);
        }
        if (Input.GetKey(KeyCode.A))
        {
            action.MovePlayer(Diretion.LEFT);
        }
        if (Input.GetKey(KeyCode.D))
        {
            action.MovePlayer(Diretion.RIGHT);
        }
    }

    private void OnGUI()
    {
        GUI.Label(new Rect(10, 5, 200, 50), "分数:", textStyle);
        GUI.Label(new Rect(55, 5, 200, 50), action.GetScore().ToString(), scoreStyle);
        GUI.Label(new Rect(Screen.width - 170, 5, 50, 50), "剩余水晶数", textStyle);
        GUI.Label(new Rect(Screen.width - 80, 5, 50, 50), action.GetCrystalNum().ToString(), scoreStyle);
        if (action.GetGameOver() && action.GetCrystalNum() != 0)
        {
            GUI.Label(new Rect(Screen.width / 2 - 100, Screen.width / 2 - 250, 100, 100), "您已阵亡!游戏结束!", overStyle);
            if (GUI.Button(new Rect(Screen.width / 2 - 50, Screen.width / 2 - 150, 100, 50), "重新开始"))
            {
                action.Restart();
                return;
            }
        }
        else if (action.GetCrystalNum() == 0)
        {
            GUI.Label(new Rect(Screen.width / 2 - 150, Screen.width / 2 - 250, 100, 100), "恭喜收集完全部水晶！", overStyle);
            if (GUI.Button(new Rect(Screen.width / 2 - 100, Screen.width / 2 - 150, 100, 50), "重新开始"))
            {
                action.Restart();
                return;
            }
        }
        if (showTime > 0)
        {
            GUI.Label(new Rect(Screen.width / 2 - 80, 10, 100, 100), "WSAD控制玩家移动", textStyle);
            GUI.Label(new Rect(Screen.width / 2 - 87, 30, 100, 100), "躲避巡逻兵追捕加1分", textStyle);
            GUI.Label(new Rect(Screen.width / 2 - 90, 50, 100, 100), "寻找水晶以通过游戏！", textStyle);
        }
    }
}
