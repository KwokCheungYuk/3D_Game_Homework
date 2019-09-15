using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using game;

public class View : MonoBehaviour {

	SSDirector s1;
	Action action;
	// Use this for initialization
	void Start () {
		s1 = SSDirector.getInstance ();
		action = SSDirector.getInstance () as Action;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnGUI(){
		GUI.skin.label.fontSize = 30;
		if (s1.state == State.Win) {
			if (GUI.Button (new Rect (235, 150, 300, 100), "You win!(*^_^*)\nClick here to restart")) {
				action.restart ();
			}
		}
		if (s1.state == State.Lose) {
			if (GUI.Button (new Rect (235, 150, 300, 100), "You lose./(ㄒoㄒ)/~~\nClick here to restart")) {
				action.restart ();
			}
		}
		if (GUI.Button (new Rect (330, 40, 100, 50), "GO")) {
			action.shipMove ();
		}
		if (GUI.Button (new Rect (300, 250, 80, 40), "Left OFF") && s1.state != State.LTR && s1.state != State.RTL) {
			action.offShipLeft ();
		}
		if (GUI.Button (new Rect (400, 250, 80, 40), "Right OFF") && s1.state != State.LTR && s1.state != State.RTL) {
			action.offShipRight ();
		}
		if (GUI.Button (new Rect (50, 90, 75, 40), "Priest ON")) {
			action.priestLeftOnBoat ();
		}
		if (GUI.Button (new Rect (650, 90, 75, 40), "Priest ON")) {
			action.priestRightOnBoat ();
		}
		if (GUI.Button (new Rect (190, 90, 75, 40), "Devil ON")) {
			action.devilLeftOnBoat ();
		}
		if (GUI.Button (new Rect (500, 90, 75, 40), "Devil ON")) {
			action.devilRightOnBoat ();
		}
	}
}
