using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowDirection : MonoBehaviour {
    public Transform target;
	public GameObject car;

	private Vector3 offset;

	// Use this for initialization
	void Start () {
		offset = new Vector3(0, 1.5f, 0);
	}
	
	// Update is called once per frame
	void Update () {
        //Vector3 relativePos = target.position - transform.position;
        //Quaternion rotation = Quaternion.LookRotation(relativePos);
        //transform.rotation = rotation;
        transform.right = target.position - transform.position;
		transform.position = car.transform.position + offset;
    }
}
