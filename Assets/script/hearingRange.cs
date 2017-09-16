using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hearingRange : MonoBehaviour {
	public bool isCarFound;

	void Start () {
		isCarFound = false;
	}
	private void OnTriggerEnter2D(Collider2D other){

		if (other.GetComponent<car> () != null) {
			isCarFound = true;
		}
	}
}
