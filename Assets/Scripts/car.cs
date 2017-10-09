using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour {
    public float turningAngle;
    public const int ACCELERATE_TIME = 30;
	public const float VELOCITY = 4f;
    public int accelerateTimeLeft;
    public float velocity ;
	public static Car instance;

    private Rigidbody2D rigi;
    private Vector2 previousVelocity;
    private bool engineFlag;
    private bool accelerateFlag;
    private int accelerateCountdown;
	private bool alive;
	private bool grassFlag;
    
    private void Awake()
    {
        rigi = GetComponent<Rigidbody2D>();
		if (instance == null) {
			instance = this;
		} else if (instance != this) {
			Destroy (instance);
		}
    }
    
    // Use this for initialization
    void Start () {
		this.rigi.velocity = new Vector2(0f, VELOCITY);
        this.previousVelocity = this.rigi.velocity;
        this.turningAngle = 0.02f; // Radian
        this.engineFlag = true;
        this.accelerateFlag = false;
        this.accelerateCountdown = ACCELERATE_TIME;
        this.accelerateTimeLeft = 3;
		this.alive = true;
		this.grassFlag = false;
    }

	public void updateVeclocity(float scale){
		if (scale <= 0){
			return;
		}
		if (this.rigi.velocity.magnitude == 0) {
			return;
		}
		this.rigi.velocity = this.rigi.velocity * scale;
	}

	public Vector2 getVelocity(){
		return this.rigi.velocity;
	}

	public Vector2 getPosition(){
		return this.rigi.position;
	}

	public bool isMoving(){
		if (this.rigi.velocity.magnitude == 0)
			return false;
		else
			return true;
	}

	public bool isAlive(){
		return this.alive;
	}

	public void setCarDead(){
		this.rigi.velocity = Vector2.zero;
		this.alive = false;
	}
    
    // Update is called once per frame
    void FixedUpdate () {
		if (this.alive == false)
			return;
        //float move = Input.GetAxis("Horizontal");

		int move = 0;
		bool ifAcc = false;
		if (Input.touchCount > 0 && Input.GetTouch (0).position.x <= Screen.width * 0.3) {
			move = -1;
		} else if (Input.touchCount > 0 && Input.GetTouch (0).position.x >= Screen.width * 0.7) {
			move = 1;
		} else if (Input.touchCount > 0 && Input.GetTouch (0).position.x > Screen.width * 0.35 && Input.GetTouch (0).position.x < Screen.width * 0.65) {
			ifAcc = true;
		}

//		print ("ifAcc :" + ifAcc);
//		print ("accelerateFlag: " + accelerateFlag);
				

        if (move == 1 && this.engineFlag && !this.accelerateFlag)
        {
            float x = this.rigi.velocity.x;
            float y = this.rigi.velocity.y;
            float angle = this.turningAngle;
            this.rigi.velocity = new Vector2(x * Mathf.Cos(angle) + y * Mathf.Sin(angle), (-1) * x * Mathf.Sin(angle) + y * Mathf.Cos(angle));
            transform.Rotate(0, 0, (-1) * angle * 180 / Mathf.PI);
        }
        else if (move == -1 && this.engineFlag && !this.accelerateFlag)
        {
            float x = this.rigi.velocity.x;
            float y = this.rigi.velocity.y;
            float angle = (-1) * this.turningAngle;
            this.rigi.velocity = new Vector2(x * Mathf.Cos(angle) + y * Mathf.Sin(angle), (-1) * x * Mathf.Sin(angle) + y * Mathf.Cos(angle));
            transform.Rotate(0, 0, (-1) * angle * 180 / Mathf.PI);
        }

		if (Input.GetKeyDown ("s") && this.engineFlag && !this.accelerateFlag) {
            this.previousVelocity = this.rigi.velocity;
            this.rigi.velocity = new Vector2 (0, 0);
            this.engineFlag = !this.engineFlag;
        } else if (Input.GetKeyDown ("s") && !this.accelerateFlag) {
            this.rigi.velocity = this.previousVelocity;
            this.engineFlag = !this.engineFlag;
        }

		print ("accFlag = " + accelerateFlag);
		if (ifAcc && !this.accelerateFlag && this.accelerateTimeLeft > 0) {
            this.accelerateFlag = true;
			this.updateVeclocity (5f);
            this.accelerateTimeLeft--;
			print ("first");
        } else if (this.accelerateFlag && this.accelerateCountdown > 0) {
            this.accelerateCountdown--;
			print (this.accelerateCountdown);
        } else if (this.accelerateFlag) {
            this.accelerateFlag = false;
            this.accelerateCountdown = ACCELERATE_TIME;
			this.updateVeclocity(0.2f);
			print ("third");
        }
        
    }

	private void OnTriggerEnter2D(Collider2D other){
        if (other.GetComponent<DestinationFlag>() != null)
        {
            print("win");
            this.alive = false;
            this.rigi.velocity = Vector2.zero;
            GameController.instance.gameWin();
        }
		if (other.GetComponent<GrassLand> () != null) {
			this.updateVeclocity (0.5f);
		}
	}

	private void OnTriggerExit2D(Collider2D other){
		if (other.GetComponent<GrassLand> () != null) {
			this.updateVeclocity (2f);
		}
	}

	private void OnTriggerStay2D(Collider2D other){
		if (other.GetComponent<Zombie> () != null) {
			float targetX = other.transform.position.x;
			float targetY = other.transform.position.y;
			float carX = this.transform.position.x;
			float carY = this.transform.position.y;
			float dx = carX - targetX;
			float dy = carY - targetY;
			if (dx * dx + dy * dy < 1) {
				//this.stopCar ();
//				this.rigi.velocity = Vector2.zero;
//				this.alive = false;
				GameController.instance.gameOver ();
			}

		}
	}

}