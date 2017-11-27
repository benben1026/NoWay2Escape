using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezCoin : MonoBehaviour {

	private int effectCount = 0;
	private int limite = 10;
	private bool used = false;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		effectCount++;
		if (effectCount == limite)
			used = true;
	}
	private void OnTriggerEnter2D(Collider2D other){
		if (other.GetComponent<Car> () != null && used) {
			GameController.instance.freezAll ();

			FindObjectOfType<AudioManager>().Play("Freeze");

			used = false;
			Destroy (this.gameObject);
		}
	}

}
	