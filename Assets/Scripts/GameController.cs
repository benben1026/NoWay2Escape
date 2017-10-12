using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {
	public static GameController instance;
    public Text gamePromptText;
    public Text countDownText;
    public enum GameStatus { prepare, starting, start, win, fail};
    
    private int timeLeft;
    private float time;
    private GameStatus gameStatus;   // 0 -> ongoing; 1-> success; 2-> fail
	// Use this for initialization
	void Awake () {
		if (instance == null) {
			instance = this;
		} else if (instance != this) {
			Destroy (gameObject);
		}
	}
	void Start () {
        gamePromptText.enabled = false;
        timeLeft = 60;
        updateTime();
        time = 0.0f;
        gameStatus = 0;
        gameStatus = GameStatus.prepare;
	}
	
	// Update is called once per frame
	void Update () {
        if (gameStatus == GameStatus.prepare || gameStatus == GameStatus.starting)
        {
            if (Input.GetKeyDown("s") || Input.touchCount > 0)
            {
                gameStatus = GameStatus.starting;
            }
            return;
        }
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
        gamePromptText.text = "Game Over";
        gamePromptText.enabled = true;
        Car.instance.SetCarStatus(Car.CarStatusType.Die);
	}

    public void gameWin()
    {
        gameStatus = GameStatus.win;
        gamePromptText.text = "Congratulations!";
        gamePromptText.enabled = true;
        Car.instance.SetCarStatus(Car.CarStatusType.Win);
    }

    private void updateTime()
    {
        countDownText.text = "Time Left: " + timeLeft.ToString() + " s";
    }
}
