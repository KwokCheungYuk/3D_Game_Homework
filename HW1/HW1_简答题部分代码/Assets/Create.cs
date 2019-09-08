using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Create : MonoBehaviour {
	public GameObject table;
	// Use this for initialization
	void Start () {
		GameObject createCube = GameObject.CreatePrimitive(PrimitiveType.Cube);
		createCube.name = "cube2";
		createCube.transform.position = new Vector3(0, Random.Range(0, 5), 0);
		createCube.transform.parent = this.transform;
		GameObject table2 = GameObject.Instantiate (table);
		table2.name = "table2";
		table2.transform.position = new Vector3(3, Random.Range(0, 5), 0);
		table2.transform.parent = this.transform;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
