using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {
	public static GameController instance;
    public Text gamePromptText;
    public Text countDownText;

    private int timeLeft;
    private float time;
    private int gameStatus;   // 0 -> ongoing; 1-> success; 2-> fail
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
	}
	
	// Update is called once per frame
	void Update () {
        time += Time.deltaTime;

        if (time > 1.0f)
        {
            time = 0.0f;
            if (gameStatus == 0) {
                timeLeft--;
            }
            updateTime();
        }
        
	}
	public void gameOver(){
        gameStatus = 2;
        gamePromptText.text = "Game Over";
        gamePromptText.enabled = true;
	}

    public void gameWin()
    {
        gameStatus = 1;
        gamePromptText.text = "Congratulations!";
        gamePromptText.enabled = true;
    }

    private void updateTime()
    {
        countDownText.text = "Time Left: " + timeLeft.ToString() + " s";
    }
}
