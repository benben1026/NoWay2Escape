using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dash : MonoBehaviour {
//    public GameObject car;
//    public GameObject rocket;
//    public Animator anim;
	public Image rocket;

//    private int dashHash = Animator.StringToHash("Dash");
//    private Vector3 offset;

	// Use this for initialization
	void Start () {
//        ifInit = false;
//        offset = new Vector3(3f, -1f, 0);
//        gameObject.GetComponent<Renderer>().enabled = false;
//        rocket.GetComponent<Renderer>().enabled = false;
		rocket.enabled = false;
		rocket.color = new Color(1f, 1f, 1f, 0.5f);
    }
	
	// Update is called once per frame
	void Update () {
		if (GameController.instance.GetGameStatus() != GameController.GameStatus.start)
		{
			return;
		}
		if (Car.instance.IsDashReady ()) {
			rocket.enabled = true;
		} else {
			rocket.enabled = false;
		}
//        if (GameController.instance.GetGameStatus() == GameController.GameStatus.start && !ifInit)
//        {
//            gameObject.GetComponent<Renderer>().enabled = true;
//            rocket.GetComponent<Renderer>().enabled = true;
//            gameObject.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0.5f);
//            rocket.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0.5f);
//        }
//		if (Input.touchCount > 0) 
//		{
//			for (int i = 0; i < Input.touchCount; i++) {
//				Touch touch = Input.GetTouch (i);
//				if (gameObject.GetComponent<Collider2D>() == Physics2D.OverlapPoint (new Vector2(touch.position.x, touch.position.y), LayerMask.GetMask("controller"))) {
//					Car.instance.Dash();
//					anim.SetTrigger(dashHash);
//				}
//			}
//		}
//        transform.position = car.transform.position + offset;
	}

//    void OnMouseDown()
//    {
//		print ("mouse down");
//		if (Car.instance.Dash ()) {
//			anim.SetTrigger(dashHash);
//		}
//    }

	public void StartDash(){
		Car.instance.Dash ();
	}
}
