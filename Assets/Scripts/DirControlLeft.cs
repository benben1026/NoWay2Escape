using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirControlLeft : MonoBehaviour {
    public GameObject car;

    private Vector3 offset;
    private bool ifInit;

	// Use this for initialization
	void Start () {
        offset = new Vector3(-3f, -1f, 0);
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
			//offset = new Vector3 (-1 * Screen.width * 0.3f, -1 * Screen.height * 0.3f, 0);
            ifInit = true;
        }

		if (Input.touchCount > 0) 
		{
			for (int i = 0; i < Input.touchCount; i++) {
				Touch touch = Input.GetTouch (i);
				if (gameObject.GetComponent<Collider2D>() == Physics2D.OverlapPoint (new Vector2(touch.position.x, touch.position.y))) {
					car.GetComponent<Car>().TurnLeft();
				}
			}
		}

        transform.position = car.transform.position + offset;
    }

//    void OnMouseDrag()
//    {
//        car.GetComponent<Car>().TurnLeft();
//    }
}
