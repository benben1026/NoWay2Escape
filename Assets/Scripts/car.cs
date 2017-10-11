using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class CarConstant
{
    public static float BaseVelocity = 1f;

    public static float OnGrassDeaccScale = 0.5f;

    public static float AccScale = 3f;

    public static int AccNoOfFrames = 30;

    public static float TurningAngle = 0.03f; // Radian

    public static int BaseAccTimes = 3;
}

public class Car : MonoBehaviour {
    public int accelerateTimeLeft;
	public static Car instance;
    public enum CarStatusType { Normal, Accelerate, Die, Win };
    public enum LandType { Grass, Road };

    private Rigidbody2D rigi;
    private int accelerateCountdown;
    private enum VelocityValue { }
    private CarStatusType carStatus;
    private LandType landType;
    private bool ifInit;
    
    
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

        float move = Input.GetAxis("Horizontal");
        bool ifAcc = Input.GetKeyDown("q");
        if (Input.touchCount > 0 && Input.GetTouch(0).position.x <= Screen.width * 0.3)
        {
            move = -1;
        }
        else if (Input.touchCount > 0 && Input.GetTouch(0).position.x >= Screen.width * 0.7)
        {
            move = 1;
        }
        else if (Input.touchCount > 0 && Input.GetTouch(0).position.x > Screen.width * 0.35 && Input.GetTouch(0).position.x < Screen.width * 0.65)
        {
            ifAcc = true;
        }

        if (move != 0)
        {
            float x = this.rigi.velocity.x;
            float y = this.rigi.velocity.y;
            float angle = move * CarConstant.TurningAngle;
            this.rigi.velocity = new Vector2(x * Mathf.Cos(angle) + y * Mathf.Sin(angle), (-1) * x * Mathf.Sin(angle) + y * Mathf.Cos(angle));
            transform.Rotate(0, 0, (-1) * angle * 180 / Mathf.PI);
        }
        
        if (ifAcc && this.carStatus == CarStatusType.Normal && this.accelerateTimeLeft > 0)
        {
            this.carStatus = CarStatusType.Accelerate;
            this.rigi.velocity = CarConstant.AccScale * this.rigi.velocity;
            this.accelerateTimeLeft--;
        }
        else if(this.carStatus == CarStatusType.Accelerate && this.accelerateCountdown > 0)
        {
            this.accelerateCountdown--;
        }
        else if(this.carStatus == CarStatusType.Accelerate)
        {
            this.carStatus = CarStatusType.Normal;
            this.rigi.velocity = this.rigi.velocity / CarConstant.AccScale;
            this.accelerateCountdown = CarConstant.AccNoOfFrames;
        }

        if (Input.GetKeyDown("z"))
        {
            this.SetLandType(LandType.Grass);
        }
        if (Input.GetKeyDown("x"))
        {
            this.SetLandType(LandType.Road);
        }
        
    }

	private void OnTriggerEnter2D(Collider2D other){
        if (other.GetComponent<DestinationFlag>() != null)
        {
            GameController.instance.gameWin();
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
				GameController.instance.gameOver ();
			}

		}
	}

}
