using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class FlowerTriggerHandler : MonoBehaviour {

    public GameObject winText;
    public SceneHandler sceneHandler;
    public Timer timer;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            //activate 'win' UI
            //give player the flower
            transform.SetParent(other.transform);
            SFXController.PlaySound(2);
            winText.SetActive(true);
            timer.StopTime(); //stop timer, which throws player's time at highscorerecorder
            switch (HighScoreRecorder.CheckScore())
            {
                case HighScoreRecorder.ScoreState.None: //not a record, back to menu
                    sceneHandler.StartSceneExitTimer();
                    break;
                default: //a low or high record, go to score entry
                    sceneHandler.StartSceneResetTimer(new List<int>() { 0, 1 });
                    //build index: 2 is forest, 3 is mountain, 4 is space
                    HighScoreRecorder.SetLevel(SceneManager.GetActiveScene().buildIndex);
                    break;
            }
            Destroy(this);
        }
    }
}
