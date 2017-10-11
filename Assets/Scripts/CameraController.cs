using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
	public GameObject car;

	private Vector3 offset;
	void Start () {
//		transform.position = car.transform.position + new Vector3 (0, 0, -4);
//		offset = transform.position - car.transform.position;
		offset = new Vector3 (0, 0, -4);
	}

	// Update is called once per frame
	void LateUpdate () {
		transform.position = car.transform.position + offset;
	}
}
