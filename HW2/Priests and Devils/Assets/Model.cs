using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using game;

public class Model : MonoBehaviour {
	//存放左边的牧师
	Stack<GameObject> leftPriests = new Stack<GameObject>();
	//存放右边的牧师
	Stack<GameObject> rightPriests = new Stack<GameObject>();
	//存放左边的恶魔
	Stack<GameObject> leftDevils = new Stack<GameObject>();
	//存放右边的恶魔
	Stack<GameObject> rightDevils = new Stack<GameObject>();

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

	void load(){
		//加载两岸
		Instantiate(Resources.Load("Side"), sideLeftPostion, Quaternion.identity);
		Instantiate(Resources.Load("Side"), sideRightPostion, Quaternion.identity);

		//加载水
		Instantiate(Resources.Load("Water"), waterPostion, Quaternion.identity);

		//加载船
		ship_obj = Instantiate(Resources.Load("Ship"), shipLeftPostion, Quaternion.identity) as GameObject;

		//加载牧师和恶魔
		for (int i = 0; i < 3; i++) {
			leftPriests.Push (Instantiate (Resources.Load ("Priest")) as GameObject);
			leftDevils.Push (Instantiate (Resources.Load ("Devil")) as GameObject);
		}
	}

	//用于设置牧师和恶魔的位置
	void setTargetPosition(Stack<GameObject> target, Vector3 position){
		GameObject[] temp = target.ToArray ();
		for (int i = 0; i < target.Count; i++) {
			temp [i].transform.position = position + new Vector3 (2.0f * i, 0f, 0f);
		}
	}

	//判断船上有没有空位以及空位的位置
	int shipEmpty(){
		int empty = 0;
		for (int i = 0; i < 2; i++) {
			if (ship [i] == null) {
				empty++;
			}
		}
		return empty;
	}

	//上船操作
	void getOnShip(GameObject obj){
		obj.transform.parent = ship_obj.transform;
		if (shipEmpty () != 0) {
			if (ship [0] == null) {
				ship [0] = obj;
				if (obj.name == "Priest(Clone)") {
					obj.transform.localPosition = new Vector3 (-0.3f, 2.9f, 0);
				} 
				else {
					obj.transform.localPosition = new Vector3 (-0.4f, 2.5f, 0);
				}
			} 
			else {
				ship [1] = obj;
				if (obj.name == "Priest(Clone)") {
					obj.transform.localPosition = new Vector3 (0.3f, 2.9f, 0);
				} 
				else {
					obj.transform.localPosition = new Vector3 (0.4f, 2.5f, 0);
				}
			}
		}
	}

	//船移动
	public void shipMove(){
		//有人在船上才能移动
		if (shipEmpty () != 2) {
			if (s1.state == State.Left) {
				s1.state = State.LTR;
			}
			else if (s1.state == State.Right) {
				s1.state = State.RTL;
			}
		}
	}

	//下船
	public void offShip(int side){
		if (ship [side] != null) {
			ship [side].transform.parent = null;
			if (s1.state == State.Left) {
				if (ship [side].name == "Priest(Clone)") {
					leftPriests.Push (ship [side]);
				}
				else{
					leftDevils.Push (ship [side]);
				}
			}
			else if(s1.state == State.Right){
				if (ship [side].name == "Priest(Clone)") {
					rightPriests.Push (ship [side]);
				}
				else{
					rightDevils.Push (ship [side]);
				}
			}
			ship [side] = null;
		}
	}

	public void restart(){
		s1.state = State.Left;
		ship_obj.transform.position = shipLeftPostion;
		int rightNumPriest = rightPriests.Count;
		int rightNumDevil = rightDevils.Count;
		for (int i = 0; i < rightNumPriest; i++) {
			leftPriests.Push (rightPriests.Pop ());
		}
		for (int i = 0; i < rightNumDevil; i++) {
			leftDevils.Push (rightDevils.Pop ());
		}
		offShip (0);
		offShip (1);
	}

	//检查游戏是否结束
	void check(){
		//全都达到右岸
		if (rightPriests.Count == 3 && rightDevils.Count == 3) {
			s1.state = State.Win;
			return;
		}

		//某一边恶魔多余牧师
		int numShipPriest = 0;
		int numLeftPriest = 0;
		int numRightPriest = 0;
		int numShipDevil = 0;
		int numLeftDevil = 0;
		int numRightDevil = 0;
		for (int i = 0; i < 2; i++) {
			if (ship [i] != null && ship [i].name == "Priest(Clone)") {
				numShipPriest++;
			}
			else if (ship [i] != null && ship [i].name == "Devil(Clone)") {
				numShipDevil++;
			}
		}
		if (s1.state == State.Left) {
			numLeftPriest = leftPriests.Count + numShipPriest;
			numRightPriest = rightPriests.Count;
			numLeftDevil = leftDevils.Count + numShipDevil;
			numRightDevil = rightDevils.Count;
		}
		else if(s1.state == State.Right) {
			numLeftPriest = leftPriests.Count;
			numRightPriest = rightPriests.Count + numShipPriest;
			numLeftDevil = leftDevils.Count;
			numRightDevil = rightDevils.Count + numShipDevil;
		}
		if ((numLeftPriest != 0 && numLeftPriest < numLeftDevil) || (numRightPriest != 0 && numRightPriest < numRightDevil)) {
			s1.state = State.Lose;
		}
	}

	//从岸到船
	public void priestLeftOnBoat (){
		if (leftPriests.Count != 0 && shipEmpty() != 0 && s1.state == State.Left) {
			getOnShip (leftPriests.Pop ());
		}
	}

	public void priestRightOnBoat (){
		if (rightPriests.Count != 0 && shipEmpty() != 0 && s1.state == State.Right) {
			getOnShip (rightPriests.Pop ());
		}
	}

	public void devilLeftOnBoat (){
		if (leftDevils.Count != 0 && shipEmpty() != 0 && s1.state == State.Left) {
			getOnShip (leftDevils.Pop ());
		}
	}

	public void devilRightOnBoat (){
		if (rightDevils.Count != 0 && shipEmpty() != 0 && s1.state == State.Right) {
			getOnShip (rightDevils.Pop ());
		}
	}

	// Use this for initialization
	void Start () {
		s1 = SSDirector.getInstance ();
		s1.setModel (this);
		load ();
	}
	
	// Update is called once per frame
	void Update () {
		setTargetPosition (leftPriests, priestsLeftPostion);
		setTargetPosition (rightPriests, priestsRightPostion);
		setTargetPosition (leftDevils, DevilsLeftPostion);
		setTargetPosition (rightDevils, DevilsRightPostion);

		if (s1.state == State.LTR) {
			ship_obj.transform.position = Vector3.MoveTowards (ship_obj.transform.position, shipRightPostion, Time.deltaTime * speed);
			if (ship_obj.transform.position == shipRightPostion) {
				s1.state = State.Right;
			}
		} 
		else if (s1.state == State.RTL) {
			ship_obj.transform.position = Vector3.MoveTowards (ship_obj.transform.position, shipLeftPostion, Time.deltaTime * speed);
			if (ship_obj.transform.position == shipLeftPostion) {
				s1.state = State.Left;
			}
		}
		else {
			check ();
		}
	}
}
