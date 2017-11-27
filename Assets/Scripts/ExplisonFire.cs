using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplisonFire : MonoBehaviour {
	private int expCount;

	// Use this for initialization
	void Start () {
		FindObjectOfType<AudioManager>().Play("Explosion");
		expCount = 40;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		expCount--;
		if (expCount == 0) {
		
			Destroy (this.gameObject);
		}
	}
}
