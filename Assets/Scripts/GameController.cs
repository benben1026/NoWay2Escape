using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {
	public static GameController instance;
	public GameObject gameOverText;
	public bool isSuccess;
	public bool isFailed;
	// Use this for initialization
	void Awake () {
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
		gameOverText.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
//		if (instance == null) {
//			instance = this;
//			print ("instance is null");
//		}
	}
	public void gameOver(){
		isFailed = true;
//		print ("failed");
		gameOverText.SetActive (true);
	}
}
