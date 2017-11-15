using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DirControlLeft : MonoBehaviour {
	public GameObject car;
	public Image leftArrow;

    //private Vector3 offset;
    private bool ifInit;
	private bool ifTurn;

	// Use this for initialization
	void Start () {
		leftArrow.enabled = false;
        ifInit = false;
		ifTurn = false;
    }
	
	// Update is called once per frame
	void Update () {
        if (GameController.instance.GetGameStatus() != GameController.GameStatus.start)
        {
            return;
        }
        if (!ifInit)
        {
			leftArrow.enabled = true;
            ifInit = true;
        }
		if (ifTurn){
			car.GetComponent<Car>().TurnLeft();
		}
    }

	public void StartTurnLeft(){
		ifTurn = true;
	}

	public void EndTurnLeft(){
		ifTurn = false;
	}

}
