using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class CarConstant
{
    public static float BaseVelocity = 1f;

    public static float OnGrassDeaccScale = 0.4f;

    public static float AccScale = 4f;

    public static int AccNoOfFrames = 40;

    public static float TurningAngle = 0.03f; // Radian

    public static int BaseAccTimes = 5;

    public static int DashCoolDownTime = 4;
}

public class Car : MonoBehaviour {
    public int accelerateTimeLeft;
	public static Car instance;
    public enum CarStatusType { Normal, Accelerate, Freeze, Die, Win };
    public enum LandType { Grass, Road };

    private Rigidbody2D rigi;
    private int accelerateCountdown;
    private enum VelocityValue { }
    private CarStatusType carStatus;
    private LandType landType;
    private bool ifInit;
    private bool ifDash;
    private float dashCoolDownTimeCounter;
    private float move;
    private Vector2 originVelocity;
    private int freezeTimeCount;
    
    
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
        this.accelerateCountdown = CarConstant.AccNoOfFrames;
        this.accelerateTimeLeft = CarConstant.BaseAccTimes;
        this.rigi.velocity = Vector2.zero;
        this.ifInit = false;
        carStatus = CarStatusType.Normal;
        landType = LandType.Road;
        ifDash = false;
		dashCoolDownTimeCounter = CarConstant.DashCoolDownTime;
        move = 0;
        this.freezeTimeCount = 0;
    }

    private void Init()
    {
        this.rigi.velocity = new Vector2(0f, CarConstant.BaseVelocity);
    }

    public CarStatusType GetCarStatus()
    {
        return this.carStatus;
    }

    public void SetCarStatus(CarStatusType t)
    {
        this.carStatus = t;
    }

    public LandType GetLandType()
    {
        return this.landType;
    }

    public void SetLandType(LandType t)
    {
        if (this.landType == t){ return; }
        if (t == LandType.Grass)
        {
            this.rigi.velocity = this.rigi.velocity * CarConstant.OnGrassDeaccScale;
            this.landType = t;
        }
        else if(t == LandType.Road)
        {
            this.rigi.velocity = this.rigi.velocity / CarConstant.OnGrassDeaccScale;
            this.landType = t;
        }
    }

	public Vector2 GetPosition(){
		return this.rigi.position;
	}

	public bool IsDashReady(){
		return this.dashCoolDownTimeCounter > CarConstant.DashCoolDownTime && this.accelerateTimeLeft > 0;
	}

    public bool Dash()
    {
		if (!this.IsDashReady())
			return false;
		this.carStatus = CarStatusType.Accelerate;
		this.rigi.velocity = CarConstant.AccScale * this.rigi.velocity;
		this.accelerateTimeLeft--;
		dashCoolDownTimeCounter = 0;
		return true;
    }

    public void Freeze(){
        if (carStatus == CarStatusType.Freeze){
            return;
        }
        this.originVelocity = this.rigi.velocity;
        this.rigi.velocity = Vector2.zero;
        this.carStatus = CarStatusType.Freeze;
        this.freezeTimeCount = 0;
    }

    public void TurnLeft()
    {
        move = -1;
    }

    public void TurnRight()
    {
        move = 1;
    }
    
    // Update is called once per frame
    void FixedUpdate ()
    {

        if (this.carStatus == CarStatusType.Win || this.carStatus == CarStatusType.Die)
        {
            this.rigi.velocity = Vector2.zero;
            return;
        }
        if (GameController.instance.GetGameStatus() != GameController.GameStatus.start)
        {
            return;
        }
        if (!ifInit)
        {
            Init();
            ifInit = true;
        }


        if (this.carStatus == CarStatusType.Freeze && this.freezeTimeCount > 30){
            this.freezeTimeCount = 0;
            this.carStatus = CarStatusType.Normal;
            this.rigi.velocity = this.originVelocity;
            return;
        } else if (this.carStatus == CarStatusType.Freeze) {
            this.freezeTimeCount ++;
            return;
        }

        //float move = Input.GetAxis("Horizontal");
        ////ifDash = Input.GetKeyDown("q") || ifDash;
        if (Input.GetKeyDown("q"))
        {
            this.Dash();
        }
		if (Input.GetAxis ("Horizontal") == 1) {
			this.TurnRight ();
		} else if (Input.GetAxis ("Horizontal") == -1) {
			this.TurnLeft ();
		}
//        if (Input.touchCount > 0 && Input.GetTouch(0).position.x <= Screen.width * 0.3)
//        {
//            move = -1;
//        }
//        else if (Input.touchCount > 0 && Input.GetTouch(0).position.x >= Screen.width * 0.7)
//        {
//            move = 1;
//        }
        //else if (Input.touchCount > 0 && Input.GetTouch(0).position.x > Screen.width * 0.35 && Input.GetTouch(0).position.x < Screen.width * 0.65)
        //{
        //    ifDash = true;
        //}

        if (move != 0)
        {
            float x = this.rigi.velocity.x;
            float y = this.rigi.velocity.y;
            float angle = move * CarConstant.TurningAngle;
            this.rigi.velocity = new Vector2(x * Mathf.Cos(angle) + y * Mathf.Sin(angle), (-1) * x * Mathf.Sin(angle) + y * Mathf.Cos(angle));
            transform.Rotate(0, 0, (-1) * angle * 180 / Mathf.PI);
            move = 0;
        }

        dashCoolDownTimeCounter += Time.deltaTime;
//        if (ifDash && this.carStatus == CarStatusType.Normal && this.accelerateTimeLeft > 0)
//        {
//            this.carStatus = CarStatusType.Accelerate;
//            this.rigi.velocity = CarConstant.AccScale * this.rigi.velocity;
//            this.accelerateTimeLeft--;
//            ifDash = false;
//            dashCoolDownTimeCounter = 0;
//        }
        if(this.carStatus == CarStatusType.Accelerate && this.accelerateCountdown > 0)
        {
            this.accelerateCountdown--;
        }
        else if(this.carStatus == CarStatusType.Accelerate)
        {
            this.carStatus = CarStatusType.Normal;
            this.rigi.velocity = this.rigi.velocity / CarConstant.AccScale;
            this.accelerateCountdown = CarConstant.AccNoOfFrames;
        }
        
    }

	private void OnTriggerEnter2D(Collider2D other){
        if (other.GetComponent<DestinationFlag>() != null)
        {
            GameController.instance.gameWin();
        }
		if (other.GetComponent<wall> () != null) 
		{
			GameController.instance.gameOver ();
		}
	}

	private void OnTriggerStay2D(Collider2D other){
		if (other.GetComponent<Zombie> () != null ||other.GetComponent<explosionZombie> () != null ||other.GetComponent<helmetZombie> () != null) {
			if (this.carStatus == Car.CarStatusType.Accelerate || GameController.instance.GetGameStatus() != GameController.GameStatus.start) {
				return;
			}
			float targetX = other.transform.position.x;
			float targetY = other.transform.position.y;
			float carX = this.transform.position.x;
			float carY = this.transform.position.y;
			float dx = carX - targetX;
			float dy = carY - targetY;
			if (dx * dx + dy * dy < 0.05) {
				GameController.instance.gameOver ();
			}

		} else if (other.GetComponent<ExplisonFire> () != null && this.carStatus != CarStatusType.Accelerate) {

			// float targetX = other.transform.position.x;
			// float targetY = other.transform.position.y;
			// float carX = this.transform.position.x;
			// float carY = this.transform.position.y;
			// float dx = carX - targetX;
			// float dy = carY - targetY;
            this.Freeze();
			// if (dx * dx + dy * dy < 0.4) {
			// 	//GameController.instance.gameOver ();
   //              this.Freeze();
			// }
		}
	}

}
