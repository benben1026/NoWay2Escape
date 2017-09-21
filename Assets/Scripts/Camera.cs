using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour {
	public GameObject car;

	private Vector3 offset;
	void Start () {
		offset = transform.position - car.transform.position;
	}

	// Update is called once per frame
	void LateUpdate () {
		transform.position = car.transform.position + offset;
	}
}
