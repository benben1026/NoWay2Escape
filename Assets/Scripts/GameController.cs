using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {
	public bool isSuccess;
	public bool isFailed;
	public static GameController instance;
	// Use this for initialization
	void Awake(){
		if (instance == null) {
			instance = this;
		} else if (instance != this) {

			Destroy (gameObject);
		}
	}

	void Start () {
//		Instantiate (enemy);
		isSuccess = false;
		isFailed = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
