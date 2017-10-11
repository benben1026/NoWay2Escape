using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
	public GameObject car;
    public int height;

	private Vector3 offset;
	void Start () {
		offset = new Vector3 (0, 0, height);
	}

	// Update is called once per frame
	void LateUpdate () {
		transform.position = car.transform.position + offset;
	}
}
