using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
	public GameObject car;
    public int height;

	private Vector3 offset;
    private GameController gc;
    private Vector3 distancePerFrame;
    private int noOfFrameToMove = 60;

	void Start () {
        height = -4;
		offset = new Vector3 (0, 0, height);
        transform.position = new Vector3(12.5f, 10, -15);
        gc = GameController.instance;
        distancePerFrame = (car.transform.position + offset - transform.position) / noOfFrameToMove;
    }

	// Update is called once per frame
	void LateUpdate () {
        if (gc.GetGameStatus() == GameController.GameStatus.prepare) { return;  }
        if (gc.GetGameStatus() == GameController.GameStatus.starting && this.noOfFrameToMove > 0)
        {
            transform.position = transform.position + distancePerFrame;
            this.noOfFrameToMove--;
            return;
        }
        else if(gc.GetGameStatus() == GameController.GameStatus.starting)
        {
            gc.StartGame();
            return;
        }

		transform.position = car.transform.position + offset;
	}
}
