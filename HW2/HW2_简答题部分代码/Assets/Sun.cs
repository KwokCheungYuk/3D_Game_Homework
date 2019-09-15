using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sun : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}

	// Update is called once per frame
	void Update () {
		float rotationStandarTime = 20.0f;
		this.transform.Rotate (Vector3.down * Time.deltaTime * (rotationStandarTime / 25.05f));
	}
}
