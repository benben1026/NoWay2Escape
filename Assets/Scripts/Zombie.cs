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

	public GameObject zombiePrefab;

	private void Awake()
	{
		rigi = GetComponent<Rigidbody2D>();
	}

	// Use this for initialization
	void Start () {
		constantV = 0.5f;
		this.rigi.velocity = new Vector2(0f, constantV);
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
			chasing ();
			//			print ("carfound");
		} else {
			randomMove ();
			//			print ("carnofound");
		}
	}
	void chasing(){
        if (Car.instance.GetCarStatus() == Car.CarStatusType.Die || Car.instance.GetCarStatus() == Car.CarStatusType.Win)
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
	//	private void OnTriggerEnter2D(Collider2D other){
	//
	//		if (other.GetComponent<car> () != null) {
	//			isCarFound = true;
	//			//			print ("found");
	//			targetX = other.transform.position.x;
	//			targetY = other.transform.position.y;
	//			//			print (targetX);
	//			//			print (targetY);
	//		}
	//	}
	private void OnTriggerStay2D(Collider2D other){
//		print ("stay");
		if ((other.GetComponent<Car> () != null && GameController.instance.isGameStart())|| other.GetComponent<Trap>() != null ) {
			isCarFound = true;
			print ("iscar");
			targetX = other.transform.position.x;
			targetY = other.transform.position.y;

			float zX = this.transform.position.x;
			float zY = this.transform.position.y;
			float dx = zX - targetX;
			float dy = zY - targetY;
			if (dx * dx + dy * dy < 0.05) {
				print ("too close");

				Destroy (this.gameObject);
				Vector2 objectPoolPosition = new Vector2 (zX, zY);
				Instantiate (zombiePrefab,objectPoolPosition, gameObject.transform.rotation);

			}
		}

	}
	private void OnTriggerExit2D(Collider2D other){

		if (other.GetComponent<Car> () != null) {
			isCarFound = false;


		}


	}
	private void OnTriggerEnter(Collider2D other){
		if (other.GetComponent<wall>() != null) {
			print ("reach the edge");
			float zX = this.transform.position.x;
			float zY = this.transform.position.y;
			float dx = centerx - zX;
			float dy = centery - zY;
			float mag = Mathf.Sqrt (dx * dx + dy * dy);
			float v = mag / this.rigi.velocity.magnitude;
			this.rigi.velocity = new Vector2 (dx * v, dy * v);
		}
	}
}
