using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : MonoBehaviour {
    public GameObject car;
    public GameObject rocket;
    public Animator anim;

    private int dashHash = Animator.StringToHash("Dash");
    private Vector3 offset;
    private bool ifInit;

	// Use this for initialization
	void Start () {
        ifInit = false;
        offset = new Vector3(3.5f, -1f, 0);
        gameObject.GetComponent<Renderer>().enabled = false;
        rocket.GetComponent<Renderer>().enabled = false;
    }
	
	// Update is called once per frame
	void Update () {
        if (GameController.instance.GetGameStatus() == GameController.GameStatus.start && !ifInit)
        {
            gameObject.GetComponent<Renderer>().enabled = true;
            rocket.GetComponent<Renderer>().enabled = true;
            gameObject.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0.5f);
            rocket.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0.5f);
        }
		if (Input.touchCount > 0) 
		{
			for (int i = 0; i < Input.touchCount; i++) {
				Touch touch = Input.GetTouch (i);
				if (gameObject.GetComponent<Collider2D>() == Physics2D.OverlapPoint (new Vector2(touch.position.x, touch.position.y))) {
					Car.instance.Dash();
					anim.SetTrigger(dashHash);
				}
			}
		}
        transform.position = car.transform.position + offset;
	}

//    void OnMouseDown()
//    {
//        Car.instance.Dash();
//        anim.SetTrigger(dashHash);
//    }
}
