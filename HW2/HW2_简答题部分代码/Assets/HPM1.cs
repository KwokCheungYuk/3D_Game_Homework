using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPM1 : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	private float verticalSpeed = 0.00f;
	public float horizontalSpeed = 5.00f;
	const float g = 0.098f;
	// Update is called once per frame
	void Update () {
		this.transform.position += Vector3.right * Time.deltaTime * horizontalSpeed;
		this.transform.position += Vector3.down * Time.deltaTime * verticalSpeed;
		verticalSpeed += g;
		Debug.Log (verticalSpeed);
	}
}
