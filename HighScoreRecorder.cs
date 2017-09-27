using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public static class HighScoreRecorder {

    public enum ScoreState { High, Low, None }

    static string path = Path.Combine(Environment.CurrentDirectory, "hs.sloth");
    static int scoreTime = 40;
    static string scoreName = "aaaa";
    static int levelIndex = 2;
    public static string[] hsTokens = new string[5] { "600 slow 2", "300 #yes 3", "200 oh!! 4", "110 goob 3", "70 uwu? 2" };
    public static string[] lsTokens = new string[5] { "60 wham 4", "45 helo 3", "35 asdf 4", "25 ya$$ 2", "10 fast 2" };

    public static void SetTime(float timeElapsed)
    {
        scoreTime = Mathf.FloorToInt(timeElapsed);
    }

    public static void SetName(string name)
    {
        scoreName = name;
        WriteHighScore(); //if we're setting the name, that means we got a high score. so, do this.
    }

    public static void SetLevel(int level)
    {
        levelIndex = level;
    }

    static void WriteHighScore()
    {
        //get values from file, just in case
        Read();
        //check for high score
        switch (CheckScore())
        {
            case ScoreState.Low:
                List<int> lowScores = new List<int>();
                for (int i = 0; i < 5; i++)
                {
                    lowScores.Add(int.Parse(lsTokens[i].Split(' ')[0]));
                }
                lowScores.Add(scoreTime); //list now includes existing scores, and the new record
                lowScores.Sort(); //sort list (low to high)
                lowScores.Reverse(); //reverse list (high to low)
                lowScores.RemoveAt(0); //remove highest record (slowest time)
                int lowIndex = lowScores.IndexOf(scoreTime); //this is the index of our friend, the new low score
                string[] lowResult = new string[5] { "", "", "", "", "" };
                for (int i = 0; i < 5; i++)
                {
                    if (i < lowIndex) //shifted down a slot, so they reference i+1 for name
                    {
                        lowResult[i] = lowScores[i] + " " + lsTokens[i + 1].Split(' ')[1] + " " + lsTokens[i+1].Split(' ')[2];
                    }
                    else if (i == lowIndex)
                    {
                        lowResult[i] = scoreTime + " " + scoreName + " " + levelIndex;
                    }
                    else
                    {
                        lowResult[i] = lowScores[i] + " " + lsTokens[i].Split(' ')[1] + " " + lsTokens[i].Split(' ')[2];
                    }
                }
                lsTokens = lowResult;
                Record();
                break;
            case ScoreState.High:
                List<int> highScores = new List<int>();
                for (int i = 0; i < 5; i++)
                {
                    highScores.Add(int.Parse(hsTokens[i].Split(' ')[0]));
                }
                highScores.Add(scoreTime);
                highScores.Sort();
                highScores.Reverse();
                highScores.RemoveAt(5);
                int highIndex = highScores.IndexOf(scoreTime);
                string[] highResult = new string[5] { "", "", "", "", "" };
                for (int i = 0; i < 5; i++)
                {
                    if (i < highIndex)
                    {
                        highResult[i] = highScores[i] + " " + hsTokens[i].Split(' ')[1] + " " + hsTokens[i].Split(' ')[2];
                    }
                    else if (i == highIndex)
                    {
                        highResult[i] = scoreTime + " " + scoreName + " " + levelIndex;
                    }
                    else
                    {
                        highResult[i] = highScores[i] + " " + hsTokens[i - 1].Split(' ')[1] + " " + hsTokens[i-1].Split(' ')[2];
                    }
                }
                hsTokens = highResult;
                Record();
                break;
            default:break;
        }
    }

    static void Record()
    {
        StreamWriter sw = new StreamWriter(path, false, System.Text.Encoding.UTF8);
        for(int i = 0; i < 5; i++)
        {
            sw.WriteLine(hsTokens[i]);
        }
        for(int i = 0; i < 5; i++)
        {
            sw.WriteLine(lsTokens[i]);
        }
        sw.Close();
    }

    public static void Read()
    {
        StreamReader sr = new StreamReader(CreatePath(), System.Text.Encoding.UTF8);
        for (int i = 0; i < 10; i++)
        {
            string line = sr.ReadLine();
            if (line != null)
            {
                if (i < 5)
                {
                    hsTokens[i] = line;
                }
                else
                {
                    lsTokens[i-5] = line;
                }
            }
        }
        sr.Close();
    }

    static string CreatePath()
    {
        if (!File.Exists(path))
        {
            Record();
        }
        return path;
    }

    public static ScoreState CheckScore()
    {
        string minHigh = hsTokens[4].Split(' ')[0];
        string minLow = lsTokens[0].Split(' ')[0];
        
        if (scoreTime > int.Parse(minHigh))
        {
            return ScoreState.High;
        }
        else if (scoreTime < int.Parse(minLow))
        {
            return ScoreState.Low;
        }

        return ScoreState.None;
    }
}
