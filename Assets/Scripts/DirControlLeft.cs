using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirControlLeft : MonoBehaviour {
    public GameObject car;

    private Vector3 offset;
    private bool ifInit;

	// Use this for initialization
	void Start () {
        offset = new Vector3(-4f, -1f, 0);
        gameObject.GetComponent<Renderer>().enabled = false;
        ifInit = false;
    }
	
	// Update is called once per frame
	void Update () {
        if (GameController.instance.GetGameStatus() != GameController.GameStatus.start)
        {
            return;
        }
        if (!ifInit)
        {
            gameObject.GetComponent<Renderer>().enabled = true;
            ifInit = true;
        }
        transform.position = car.transform.position + offset;
    }

    void OnMouseDrag()
    {
        //print("left");
        car.GetComponent<Car>().TurnLeft();
    }
}
