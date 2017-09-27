using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//populates 'records' screen with high scores, on loading that menu.
public class RecordsGrabber : MonoBehaviour {

    public Text bestTimes, bestNames, worstTimes, worstNames;
    public Image[] bestImages, worstImages;
    public Sprite[] levelIcons;

	void Awake()
    {
        HighScoreRecorder.Read();
        string bt = "", bn = "", wt = "", wn = "";
        for (int i = 0; i < 5; i++)
        {
            string[] bestTokens = HighScoreRecorder.hsTokens[i].Split(' ');
            string[] worstTokens = HighScoreRecorder.lsTokens[i].Split(' ');
            bt += GetFormattedTime(int.Parse(bestTokens[0])) + "\n";
            wt += GetFormattedTime(int.Parse(worstTokens[0])) + "\n";
            bn += bestTokens[1] + "\n";
            wn += worstTokens[1] + "\n";
            bestImages[i].sprite = levelIcons[int.Parse(bestTokens[2]) - 2];
            worstImages[i].sprite = levelIcons[int.Parse(worstTokens[2]) - 2];
        }
        bestTimes.text = bt;
        worstTimes.text = wt;
        bestNames.text = bn;
        worstNames.text = wn;
    }

    string GetFormattedTime(int time)
    {
        return (time / 60) + ":" + ExtraZero(time % 60) + (time % 60);
    }

    string ExtraZero(int seconds)
    {
        if (seconds < 10) return "0";
        return "";
    }

}
