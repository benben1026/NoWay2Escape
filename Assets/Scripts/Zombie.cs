using System; 
using UnityEngine;

public class Zombie : MonoBehaviour {
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
	public float centerx = 12.5f;
	public float centery = 9.36f;
	public int chasingCount = 0;
	public int chasingCountThreadHold = 2;
	private bool isDead = false;
//	private float speedFactor;

	public GameObject zombiePrefab;
	public GameObject movingCenter;

	private void Awake()
	{
		rigi = GetComponent<Rigidbody2D>();
	}

	// Use this for initialization
	void Start () {
//		speedFactor = 1;
		constantV = 0.5f;
		this.rigi.velocity = new Vector2(0f, constantV);
		this.turningAngle = 0.02f; // Radian

		rnd = new System.Random(Guid.NewGuid().GetHashCode());

		currentDir = 0;
		randomDir();
		isTurning = false;
		Turningcount = 0;
		isCarFound = false;
		movingCenter = (GameObject)Instantiate (movingCenter, this.transform.position, gameObject.transform.rotation);
	}

	// Update is called once per frame
	void FixedUpdate () {
		
		if (this.rigi.velocity.magnitude < constantV) {
			this.rigi.velocity = new Vector2(0f, constantV);
			//			transform.Rotate(0, 0, 30 / Mathf.PI);
		}

		this.rigi.velocity = (float)GameController.instance.getSpeedFactor() * this.rigi.velocity;
		if (this.rigi.velocity.magnitude > 2.5)
			this.rigi.velocity = this.rigi.velocity * ((float)2.5 / this.rigi.velocity.magnitude);

		if (isCarFound) {
			chasing ();
			//			print ("carfound");
		} else {
			randomMove ();
			//			print ("carnofound");
		}
	}
	void chasing(){
        if (GameController.instance.GetGameStatus() != GameController.GameStatus.start)
        {
            return;
        }
		if (chasingCount == 0) {
			chasingCount = chasingCountThreadHold;
		} else {
			chasingCount--;
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

		//		if (thy < targetY) {
		//			if (alphx > 0)
		////				this.transform.Rotate (0, 0, (-1) * angle * 180 / Mathf.PI);
		//				this.transform.Rotate (0, 0, angle * 180 / Mathf.PI);
		//			else if (alphx < 0)
		////				this.transform.Rotate (0, 0, angle * 180 / Mathf.PI);
		//				this.transform.Rotate (0, 0, (-1) * angle * 180 / Mathf.PI);
		//		} else if (thy > targetY) {
		//			if (alphx > 0)
		////				this.transform.Rotate (0, 0, angle * 180 / Mathf.PI);
		//				this.transform.Rotate (0, 0, (-1) * angle * 180 / Mathf.PI);
		//			else if (alphx < 0)
		////				this.transform.Rotate (0, 0, (-1) * angle * 180 / Mathf.PI);
		//				this.transform.Rotate (0, 0, angle * 180 / Mathf.PI);
		//		}

		//		this.transform.Rotate (dir.x * Time.deltaTime, dir.y * Time.deltaTime, 0, Space.Self);

		float x = this.rigi.velocity.x;
		float y = this.rigi.velocity.y;
		Vector2 nv = new Vector2(x * Mathf.Cos(angle) + y * Mathf.Sin(angle), (-1) * x * Mathf.Sin(angle) + y * Mathf.Cos(angle));
		if(!float.IsNaN(nv.x)) this.rigi.velocity = nv;
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
				if (!this.isDead) {
					isDead = true;
					Instantiate (zombiePrefab,objectPoolPosition, gameObject.transform.rotation);

				}


			}
		}
		if (other.GetComponent<ExplisonFire> () != null) {
			float targetX = other.transform.position.x;
			float targetY = other.transform.position.y;
			float carX = this.transform.position.x;
			float carY = this.transform.position.y;
			float dx = carX - targetX;
			float dy = carY - targetY;
			if (dx * dx + dy * dy < 0.1) {
				
					Destroy (this.gameObject);

			}
		}
	}

	private void OnTriggerExit2D(Collider2D other){
		if (other.GetComponent<Car> () != null) {
			isCarFound = false;
			Destroy (this.movingCenter.gameObject);
			movingCenter = (GameObject)Instantiate (movingCenter, this.transform.position, gameObject.transform.rotation);
		}
		if (!isCarFound) {
			if (other.GetComponent<MovingCenter> () != null) {
//				print ("out of center");
				this.rigi.velocity = (-1) * this.rigi.velocity;
			}
		}



	}
	private void OnTriggerEnter2D(Collider2D other){
		if (other.GetComponent<wall>() != null) {
			this.rigi.velocity = (-1) * this.rigi.velocity;
		}
	}
}
