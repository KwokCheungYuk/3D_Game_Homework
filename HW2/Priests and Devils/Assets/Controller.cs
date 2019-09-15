using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using game;

namespace game{
	public enum State{Left, Right, LTR, RTL, Win, Lose};

	public interface Action{
		//船移动
		void shipMove ();
		//角色在岸左边下船
		void offShipLeft ();
		//角色在岸右边下船
		void offShipRight ();
		//重新开始
		void restart ();
		//牧师在岸左边上船
		void priestLeftOnBoat ();
		//牧师在岸右边上船
		void priestRightOnBoat ();
		//恶魔在岸左边上船
		void devilLeftOnBoat ();
		//恶魔在岸右边上船
		void devilRightOnBoat ();
	}

	public class SSDirector : System.Object, Action{
		private static SSDirector instance;
		private Model obj;
		public Controller controller;
		public State state = State.Left;

		public static SSDirector getInstance(){
			if (instance == null) {
				instance = new SSDirector ();
			}
			return instance;
		}

		public Model getModel(){
			return obj;
		}

		internal void setModel(Model obj2){
			if(obj == null){
				obj = obj2;
			}
		}

		public void shipMove (){
			obj.shipMove ();
		}

		public void offShipLeft (){
			obj.offShip (0);
		}

		public void offShipRight (){
			obj.offShip (1);
		}

		public void restart(){
			obj.restart ();
		}

		public void priestLeftOnBoat (){
			obj.priestLeftOnBoat ();
		}

		public void priestRightOnBoat (){
			obj.priestRightOnBoat ();
		}

		public void devilLeftOnBoat (){
			obj.devilLeftOnBoat ();
		}

		public void devilRightOnBoat (){
			obj.devilRightOnBoat ();
		}
	}
}

public class Controller : MonoBehaviour {

	// Use this for initialization
	void Start () {
		SSDirector s1 = SSDirector.getInstance ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
