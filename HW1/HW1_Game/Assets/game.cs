using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class game : MonoBehaviour
{

	private int empty = 9;
	private int turn = 1;
	private int [,]chess = new int[3, 3]; //三种形式：0：没下； 1：X，2：O

	// Use this for initialization
	void Start()
	{
		reset();
	}

	//重新开始
	void reset()
	{

		empty = 9;
		turn = 1;
		for (int i = 0; i < 3; i++)
		{
			for (int j = 0; j < 3; j++)
			{
				chess[i, j] = 0;
			}
		}
	}

	//判断who wins
	int isWin()
	{
		//取中心点
		int temp = chess[1, 1];
		if(temp != 0)
		{
			//左上到右下斜
			if (temp == chess[0, 0] && temp == chess[2, 2])
			{
				return temp;
			}
			//右上到左下斜
			if (temp == chess[0, 2] && temp == chess[2, 0])
			{
				return temp;
			}
			//↓
			if (temp == chess[0, 1] && temp == chess[2, 1])
			{
				return temp;
			}
			//横
			if (temp == chess[1, 0] && temp == chess[1, 2])
			{
				return temp;
			}
		}

		//取第一个点
		temp = chess[0, 0];
		if(temp != 0)
		{
			//第一行
			if(temp == chess[0,1] && temp == chess[0,2])
			{
				return temp;
			}
			//第一列
			if (temp == chess[1, 0] && temp == chess[2, 0])
			{
				return temp;
			}
		}

		//取最后一个点
		temp = chess[2, 2];
		if(temp != 0)
		{
			//第三行
			if(temp == chess[2,0] && temp == chess[2,1])
			{
				return temp;
			}
			//第三列
			if(temp == chess[0,2] && temp == chess[1,2])
			{
				return temp;
			}
		}

		//棋局是否空
		if(empty == 0)
		{
			return 3;//打平
		}
		else
		{
			return 0;//continue
		}
	}

	private void OnGUI()
	{
		//字大小
		GUI.skin.button.fontSize = 20;
		GUI.skin.label.fontSize = 30;
		//点reset
		if(GUI.Button(new Rect(0,0,200,80), "Reset"))
		{
			reset();
		}

		int result = isWin();

		if (result == 1) {
			GUI.Label (new Rect (400, 20, 100, 50), "O wins");
		} 
		else if (result == 2) {
			GUI.Label (new Rect (400, 20, 100, 50), "X wins");
		} 
		else if (result == 3) {
			GUI.Label (new Rect (370, 20, 200, 50), "no one wins");
		} 
		else {
			if (turn == 1) {
				GUI.Label (new Rect (310, 10, 400, 50), "playing......It's O turn");
			}
			else if (turn == 2) {
				GUI.Label (new Rect (310, 10, 400, 50), "playing......It's X turn");
			}
		}
		for(int i = 0; i < 3; i++)
		{
			for(int j = 0; j < 3; j++)
			{
				if (chess[i, j] == 1) GUI.Button(new Rect(i * 100 + 300, j * 100 + 80, 100, 100), "O");
				if (chess[i, j] == 2) GUI.Button(new Rect(i * 100 + 300, j * 100 + 80, 100, 100), "X");
				if(GUI.Button(new Rect(i * 100 + 300, j * 100 + 80, 100, 100), ""))
				{
					if(result == 0)
					{
						if (turn == 1) chess[i, j] = 1;
						if (turn == 2) chess[i, j] = 2;
						empty--;
						if(empty%2 == 1)
						{
							turn = 1;
						}
						else
						{
							turn = 2;
						}
					}
				}
			}
		}
	}
}
