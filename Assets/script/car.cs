using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class car : MonoBehaviour {
	public float turningAngle;
	public const int ACCELERATE_TIME = 30;
	public int accelerateTimeLeft;

    private Rigidbody2D rigi;
	private Vector2 previousVelocity;
	private bool engineFlag;
	private bool accelerateFlag;
	private int accelerateCountdown;
    
    private void Awake()
    {
        rigi = GetComponent<Rigidbody2D>();
    }
    
    // Use this for initialization
    void Start () {
        this.rigi.velocity = new Vector2(0f, 0.5f);
		this.previousVelocity = this.rigi.velocity;
        this.turningAngle = 0.02f; // Radian
		this.engineFlag = true;
		this.accelerateFlag = false;
		this.accelerateCountdown = ACCELERATE_TIME;
		this.accelerateTimeLeft = 3;
    }
	
	// Update is called once per frame
	void FixedUpdate () {
		float move = Input.GetAxis("Horizontal");

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

		if (Input.GetKeyDown ("a") && !this.accelerateFlag && this.accelerateTimeLeft > 0) {
			this.accelerateFlag = true;
			this.rigi.velocity = 8 * this.rigi.velocity;
			this.accelerateTimeLeft--;
		} else if (this.accelerateFlag && this.accelerateCountdown > 0) {
			this.accelerateCountdown--;
		} else if (this.accelerateFlag) {
			this.accelerateFlag = false;
			this.accelerateCountdown = ACCELERATE_TIME;
			this.rigi.velocity = this.rigi.velocity / 8;
		}
        
    }
}
