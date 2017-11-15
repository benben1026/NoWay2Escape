using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DirControlRight : MonoBehaviour {
    public GameObject car;
	public Image rightArrow;

    private bool ifInit;
	private bool ifTurn;

    // Use this for initialization
    void Start () {
		rightArrow.enabled = false;
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
			rightArrow.enabled = true;
            ifInit = true;
        }
		if (ifTurn) {
			car.GetComponent<Car> ().TurnRight ();
		}
    }

	public void StartTurningRight(){
		ifTurn = true;
	}

	public void EndTurningRight(){
		ifTurn = false;
	}
}
