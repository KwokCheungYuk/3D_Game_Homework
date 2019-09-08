using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Traverse : MonoBehaviour {

	// Use this for initialization
	void Start () {
		foreach(Transform child in transform){
			Debug.Log(child.name);
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
