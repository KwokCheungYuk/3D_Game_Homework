using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SolarSystem : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		//Rotation
		float rotationStandarTime = 20.0f;
		GameObject.Find ("Mercury").transform.Rotate (Vector3.down * Time.deltaTime * (rotationStandarTime / 58.65f));
		GameObject.Find ("Venus").transform.Rotate (Vector3.up * Time.deltaTime * (rotationStandarTime / 224.7f));
		GameObject.Find ("Earth").transform.Rotate (Vector3.down * Time.deltaTime * (rotationStandarTime / 1.0f));
		GameObject.Find ("Moon").transform.Rotate (Vector3.down * Time.deltaTime * (rotationStandarTime / 27.32f));
		GameObject.Find ("Mars").transform.Rotate (Vector3.down * Time.deltaTime * (rotationStandarTime / 1.0f));
		GameObject.Find ("Jupiter").transform.Rotate (Vector3.down * Time.deltaTime * (rotationStandarTime / 0.24f));
		GameObject.Find ("Saturn").transform.Rotate (Vector3.down * Time.deltaTime * (rotationStandarTime / 0.24f));
		GameObject.Find ("Uranus").transform.Rotate (Vector3.up * Time.deltaTime * (rotationStandarTime / 0.71f));
		GameObject.Find ("Neptune").transform.Rotate (Vector3.down * Time.deltaTime * (rotationStandarTime / 0.67f));

		//Orbital revolution
		Transform center = this.transform;
		float reStandarTime = 360.0f;
		GameObject.Find ("Mercury").transform.RotateAround(center.position, new Vector3(0.1f, -1, 0), reStandarTime / 88.0f);
		GameObject.Find ("Venus").transform.RotateAround(center.position, new Vector3(0.2f, -1, 0), reStandarTime / 224.7f);
		GameObject.Find ("Earth").transform.RotateAround(center.position, new Vector3(0.3f, -1, 0), reStandarTime / 365.25f);
		GameObject.Find ("EarthClone").transform.RotateAround(center.position, new Vector3(0.3f, -1, 0), reStandarTime / 365.25f);
		GameObject.Find ("Moon").transform.RotateAround(GameObject.Find("EarthClone").transform.position, new Vector3(0, -1, 0), reStandarTime / 27.32f);
		GameObject.Find ("Mars").transform.RotateAround(center.position, new Vector3(0.5f, -1, 0), reStandarTime / 686.98f);
		GameObject.Find ("Jupiter").transform.RotateAround(center.position, new Vector3(0.6f, -1, 0), reStandarTime / 4328.9f);
		GameObject.Find ("Saturn").transform.RotateAround(center.position, new Vector3(0.7f, -1, 0), reStandarTime / 10767.5f);
		GameObject.Find ("Uranus").transform.RotateAround(center.position, new Vector3(0.8f, -1, 0), reStandarTime / 30776.8f);
		GameObject.Find ("Neptune").transform.RotateAround(center.position, new Vector3(0.9f, -1, 0), reStandarTime / 60152.0f);
	}
}
