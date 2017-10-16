using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System; 

public class explosionZombie : MonoBehaviour {
	public float turningAngle;
	private Rigidbody2D rigi;
	private int timeToChangeDir;
	private float currentDir;
	private System.Random rnd;
	public int threadHold = 5;
	private bool isTurning;
	private int Turningcount;
	public bool isCarFound;
	public float targetX;
	public float targetY;
	public float constantV;
	private int chasingCount;
	public int explosionCount;
	private Vector2 chasingVelocity;
	public GameObject expPrefab;
	public GameObject zombiePrefab;
	public float centerx = 12.5f;
	public float centery = 9.36f;

	private void Awake()
	{
		rigi = GetComponent<Rigidbody2D>();
	}

	// Use this for initialization
	void Start () {
		chasingCount = 80;
		constantV = 0.5f;
		this.rigi.velocity = new Vector2(0f, constantV);
		chasingVelocity = new Vector2 (0f, 2 * constantV);
		this.turningAngle = 0.02f; // Radian

		rnd = new System.Random(Guid.NewGuid().GetHashCode());

		currentDir = 0;
		randomDir();
		isTurning = false;
		Turningcount = 0;
		isCarFound = false;
	}

	// Update is called once per frame
	void FixedUpdate () {
		if (this.rigi.velocity.magnitude < constantV) {
			this.rigi.velocity = new Vector2(0f, constantV);
			//			transform.Rotate(0, 0, 30 / Mathf.PI);
		}



		if (isCarFound) {
//			this.rigi.velocity = chasingVelocity;
			chasing ();
			if (chasingCount > 0) {
				
				chasingCount--;
			} else {
				this.rigi.velocity = Vector2.zero;
				explosion ();


			}

			//			print ("carfound");
		} else {
			randomMove ();
			//			print ("carnofound");
		}
	}
	void explosion(){
		this.rigi.velocity = Vector2.zero;
		Instantiate (expPrefab, new Vector2 (this.transform.position.x, this.transform.position.y), Quaternion.identity);
		Destroy (this.gameObject);
	}
	void chasing(){
		if (GameController.instance.GetGameStatus() != GameController.GameStatus.start)
		{
			return;
		}
		//		Turningcount++;
		//		if (Turningcount == 40) {
		//			Turningcount = 0;
		//		} else
		//			return;
		float selfX = this.transform.position.x;
		float selfY = this.transform.position.y;
		Vector2 dir = (new Vector2(targetX,targetY)) - (new Vector2(selfX,selfY));
		float cosangle = Vector2.Dot (dir, this.rigi.velocity)/(dir.magnitude * this.rigi.velocity.magnitude);
		float angle = Mathf.Acos (cosangle);
		Vector2 currV = this.rigi.velocity;
//		float alphx = currV.x;
//		float alphy = currV.y;
//		float thy = (targetX - selfX) * alphy / alphx + selfY;

		float x = this.rigi.velocity.x * chasingVelocity.magnitude/this.rigi.velocity.magnitude;
		float y = this.rigi.velocity.y * chasingVelocity.magnitude/this.rigi.velocity.magnitude;
		this.rigi.velocity = new Vector2(x * Mathf.Cos(angle) + y * Mathf.Sin(angle), (-1) * x * Mathf.Sin(angle) + y * Mathf.Cos(angle));
		//this.rigi.velocity = Vector2.zero;
	}
	void randomMove(){

		if (!isTurning) {
			if (rnd.Next (0, 15) < threadHold) {
				randomDir ();
				isTurning = true;
			}
		} else {
			Turningcount++;
			if (Turningcount == 40) {
				isTurning = false;
				Turningcount = 0;
			}
		}
		float move = currentDir;
		//		print(rigi.velocity.magnitude);

		if (move == 1)
		{
			float x = this.rigi.velocity.x;
			float y = this.rigi.velocity.y;
			float angle = this.turningAngle;
			this.rigi.velocity = new Vector2(x * Mathf.Cos(angle) + y * Mathf.Sin(angle), (-1) * x * Mathf.Sin(angle) + y * Mathf.Cos(angle));
			//			transform.Rotate(0, 0, (-1) * angle * 180 / Mathf.PI);
		}
		else if (move == -1)
		{
			float x = this.rigi.velocity.x;
			float y = this.rigi.velocity.y;
			float angle = (-1) * this.turningAngle;
			this.rigi.velocity = new Vector2(x * Mathf.Cos(angle) + y * Mathf.Sin(angle), (-1) * x * Mathf.Sin(angle) + y * Mathf.Cos(angle));
			//			transform.Rotate(0, 0, (-1) * angle * 180 / Mathf.PI);
		}

	}
	void randomDir (){
		currentDir = rnd.Next (-1, 2);
	}

	private void OnTriggerStay2D(Collider2D other){
		if (other.GetComponent<Car> () != null && GameController.instance.isGameStart ()) {
			isCarFound = true;
			targetX = other.transform.position.x;
			targetY = other.transform.position.y;
			float zX = this.transform.position.x;
			float zY = this.transform.position.y;
			float dx = zX - targetX;
			float dy = zY - targetY;
			if (dx * dx + dy * dy < 0.05) {
				Destroy (this.gameObject);
				Vector2 objectPoolPosition = new Vector2 (zX, zY);
				Instantiate (zombiePrefab,objectPoolPosition, gameObject.transform.rotation);
				explosion ();

			}
		}
		if (other.GetComponent<Trap> () != null && GameController.instance.isGameStart ()) {
			targetX = other.transform.position.x;
			targetY = other.transform.position.y;
			float zX = this.transform.position.x;
			float zY = this.transform.position.y;
			float dx = zX - targetX;
			float dy = zY - targetY;
			if (dx * dx + dy * dy < 0.1) {
				Destroy (this.gameObject);
				Vector2 objectPoolPosition = new Vector2 (zX, zY);
				Instantiate (zombiePrefab,objectPoolPosition, gameObject.transform.rotation);
				explosion ();

			}
		}
		if (other.GetComponent<ExplisonFire> () != null) {
			float targetX = other.transform.position.x;
			float targetY = other.transform.position.y;
			float carX = this.transform.position.x;
			float carY = this.transform.position.y;
			float dx = carX - targetX;
			float dy = carY - targetY;
			if (dx * dx + dy * dy < 16) {
				explosion ();
				Destroy (this.gameObject);
//				
			}
		}

	}
	private void OnTriggerExit2D(Collider2D other){

		if (other.GetComponent<Car> () != null) {
			isCarFound = false;

		}
	}
	private void OnTriggerEnter2D(Collider2D other){
		if (other.GetComponent<wall>() != null) {
			this.rigi.velocity = (-1) * this.rigi.velocity;
		}

	}
}
