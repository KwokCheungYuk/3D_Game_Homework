using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPM2 : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	private float verticalSpeed = 0.00f;
	public float horizontalSpeed = 5.00f;
	const float g = 0.098f;
	// Update is called once per frame
	void Update () {
		Vector3 change = new Vector3 (Time.deltaTime * horizontalSpeed, -Time.deltaTime * verticalSpeed, 0.0f);
		this.transform.position += change;
		verticalSpeed += g;
	}
}
