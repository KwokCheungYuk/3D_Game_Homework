using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEventManager : MonoBehaviour
{
    //发布分数变化事件给订阅者
    public delegate void ScoreEvent();
    public static event ScoreEvent ScoreChange;
    //发送游戏结束事件给订阅者
    public delegate void GameOverEvent();
    public static event GameOverEvent GameOverChange;
    //发布水晶减少事件给订阅者
    public delegate void CrystalEvent();
    public static event CrystalEvent CrystalChange;

    public void playerEscape()
    {
        if(ScoreChange != null)
        {
            ScoreChange();
        }
    }

    public void gameOver()
    {
        if (GameOverChange != null)
        {
            GameOverChange();
        }
    }

    public void ReduceCrystalNum()
    {
        if (CrystalChange != null)
        {
            CrystalChange();
        }
    }
}
