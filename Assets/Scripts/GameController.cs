using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {
	public static GameController instance;
    public Text gameFalseInfo;
    public Text gameWinInfo;
    public Text countDownText;
	public Text accTimeLeft;
    public enum GameStatus { prepare, starting, start, win, fail};

    private int sceneIndex;
    private int timeLeft;
    private float time;
    private GameStatus gameStatus;
    public GameObject gameWinPanel, gameFalsePanel;
	private double speedFactor;
	private int initTime;
	private float initDistance;
	private double distance;
	public  bool isFreezing;
	public  int freezCount;

	// Use this for initialization
	void Awake () {
		if (instance == null) {
			instance = this;
		} else if (instance != this) {
			Destroy (gameObject);
		}
	}
	void Start () {
        sceneIndex = SceneManager.GetActiveScene().buildIndex;
        freezCount = 80;
		isFreezing = false;
        speedFactor = 1;
        gameFalseInfo.enabled = false;
        gameWinInfo.enabled = false;
        initTime = timeLeft = 30;
        updateTime();
        time = 0.0f;
        gameStatus = 0;
        gameStatus = GameStatus.prepare;
        gameWinPanel.SetActive(false);
        gameFalsePanel.SetActive(false);
		initDistance = calculateDistance();
		updateAccTiemLeft ();
		updateTime ();
	}
	float calculateDistance(){
		float cx = Car.instance.transform.position.x;
		float cy = Car.instance.transform.position.y;
		cx -= DestinationFlag.instance.transform.position.x;
		cy -= DestinationFlag.instance.transform.position.y;
		return cx * cx + cy * cy;
	}



	// Update is called once per frame
	void Update () {
		updateSpeedFacotor ();
		if (isFreezing) {
			freezCount--;
			if (freezCount == 0) {
				freezCount = 80;
				isFreezing = false;
				speedFactor = 1;
			}
		} 

//		print (speedFactor);
        if (gameStatus == GameStatus.prepare || gameStatus == GameStatus.starting)
        {
            if (Input.GetKeyDown("s") || Input.touchCount > 0)
            {
                gameStatus = GameStatus.starting;
            }
            return;
        }
		updateAccTiemLeft ();
		if (timeLeft <= 0){
			timeLeft = 0;
			updateTime ();
			this.gameOver ();
			return;
		}
        time += Time.deltaTime;

        if (time > 1.0f)
        {
            time = 0.0f;
            if (gameStatus == GameStatus.start) {
                timeLeft--;
            }
            updateTime();
        }
        
	}
	void updateSpeedFacotor(){
        distance = calculateDistance();
        double slope = sceneIndex * 0.00000001;
        speedFactor += (initTime - timeLeft) * slope + (initDistance - distance) * slope;
	}
	public double getSpeedFactor(){
		return speedFactor;
	}

	public bool isGameStart()
    {
		return gameStatus == GameStatus.start;
    }

	public GameStatus GetGameStatus()
	{
		return this.gameStatus;
	}

    public void StartGame()
    {
        Thread.Sleep(1000);
        gameStatus = GameStatus.start;
    }

	public void gameOver(){
		if (gameStatus == GameStatus.win) {
			return;
		}
        gameStatus = GameStatus.fail;
        gameFalseInfo.text = "Game Over";

		FindObjectOfType<AudioManager>().Play("GameOver");

        gameFalseInfo.enabled = true;
        Car.instance.SetCarStatus(Car.CarStatusType.Die);
        gameFalsePanel.SetActive(true);
        gameWinPanel.SetActive(false);

	}

    public void gameWin()
    {
        gameStatus = GameStatus.win;
        gameWinInfo.text = "Congratulations!";
        gameWinInfo.enabled = true;        
        Car.instance.SetCarStatus(Car.CarStatusType.Win);
        gameWinPanel.SetActive(true);
        gameFalsePanel.SetActive(false);

    }

    private void updateTime()
    {
        countDownText.text = "Time Left: " + timeLeft.ToString() + " s";
    }

	private void updateAccTiemLeft()
	{
		accTimeLeft.text = "Dash Left: " + Car.instance.accelerateTimeLeft;
	}
	public void bonusTime(){
//		print ("here to add bonus time 5s");
		this.timeLeft += 5;
		updateTime ();
	}
	public void freezAll(){
		isFreezing = true;
		speedFactor = 0;
	}
}
