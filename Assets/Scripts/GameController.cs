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
    public enum GameStatus { prepare, starting, start, win, fail};
    
    private int timeLeft;
    private float time;
    private GameStatus gameStatus;
    public GameObject gameWinPanel, gameFalsePanel;

	// Use this for initialization
	void Awake () {
		if (instance == null) {
			instance = this;
		} else if (instance != this) {
			Destroy (gameObject);
		}
	}
	void Start () {
        gameFalseInfo.enabled = false;
        gameWinInfo.enabled = false;
        timeLeft = 60;
        updateTime();
        time = 0.0f;
        gameStatus = 0;
        gameStatus = GameStatus.prepare;
        gameWinPanel.SetActive(false);
        gameFalsePanel.SetActive(false);
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
        gameFalseInfo.text = "Game Over";
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
}
