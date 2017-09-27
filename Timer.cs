using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour {

    Text timerReadout;
    float gameTime;
    string extra0;

	void Start () {
        timerReadout = GetComponent<Text>();
        gameTime = 0;
	}
	
	void Update () {
        gameTime += Time.deltaTime;
        int minutes = Mathf.FloorToInt(gameTime) / 60;
        int seconds = Mathf.FloorToInt(gameTime) % 60;
        extra0 = "";
        if (seconds < 10) extra0 = "0";
        timerReadout.text = minutes + ":" + extra0 + seconds;
	}

    public void StopTime()
    {
        HighScoreRecorder.SetTime(gameTime);
        Destroy(this);
    }
}
