using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testBehaviour : MonoBehaviour {

	// Start is called before the first frame update
	void Start()
	{
		Debug.Log("Start");
	}

	void Awake()
	{
		Debug.Log("Awake");
	}

	// Update is called once per frame
	void Update()
	{
		Debug.Log("Update");
	}

	void FixedUpdate()
	{
		Debug.Log("FixedUpdate");
	}

	void LateUpdate()
	{
		Debug.Log("LateUpdate");
	}

	void OnGUI()
	{
		Debug.Log("OnGUI");
	}

	void OnDisable()
	{
		Debug.Log("OnDisable");
	}

	void OnEnable()
	{
		Debug.Log("OnEnable");
	}
}
