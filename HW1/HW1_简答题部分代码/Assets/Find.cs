using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Find : MonoBehaviour {

	// Use this for initialization
	void Start () {
		GameObject find1 = GameObject.Find ("chair1");
		if (find1 == null) {
			Debug.Log ("Can't find chair1");
		} 
		else {
			Debug.Log ("Find chair1");
		}
		GameObject find2 = GameObject.Find ("chair5");
		if (find2 == null) {
			Debug.Log ("Can't find chair5");
		} 
		else {
			Debug.Log ("Find chair5");
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
