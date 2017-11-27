using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour {

	private int effectCount = 0;
	private int limite = 10;
	private bool used = false;
	public AudioClip[] stings;
    public AudioSource stingSource;

	// Use this for initialization
	void Start () {
	stingSource = GetComponent<AudioSource>();
	stingSource.clip = stings[0];
	
	}
	
	// Update is called once per frame
	void Update () {
		effectCount++;
		if (effectCount == limite)
			used = true;
	}
	private void OnTriggerEnter2D(Collider2D other){
	     	
		if (other.GetComponent<Car> () != null && used) {
	    	stingSource.Play();	
			GameController.instance.bonusTime ();
			used = false;
			this.gameObject.GetComponent<Renderer>().enabled = false;
			//Destroy (this.gameObject);
		}
	}
		
}
