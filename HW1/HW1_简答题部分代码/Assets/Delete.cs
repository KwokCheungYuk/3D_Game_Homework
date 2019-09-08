using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Delete : MonoBehaviour {

	// Use this for initialization
	void Start () {
		foreach (Transform child in transform) {  
			Destroy(child.gameObject);  
		} 
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
