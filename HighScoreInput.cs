using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighScoreInput : MonoBehaviour {

    const int ALPHA = 41;

    static char[] alphabet = new char[ALPHA] { 'a','b','c','d','e','f','g','h','i','j',
                                            'k','l','m','n','o','p','q','r','s','t',
                                            'u','v','w','x','y','z',
                                            '1','2','3','4','5','6','7','8','9','0',
                                            '!','@','#','$','&' };

    public SceneHandler sceneHandler;
    public Text[] texts;
    int[] nameVals;
    bool[] editable;
    float input;
    float time;
    bool allDone;

	// Use this for initialization
	void Start () {
        nameVals = new int[4] { 0, 0, 0, 0 };
        editable = new bool[4] { true, true, true, true };
	}
	
	// Update is called once per frame
	void Update () {

        allDone = true;

        for (int i = 0; i < 4; i++)
        {
            if (Input.GetButtonDown("Fire" + (i + 1)))
            {
                editable[i] = false;
                texts[i].color = Color.white;
                texts[i].transform.parent.GetComponent<Image>().enabled = false;
                SFXController.PlaySound(1);
            }
            if (editable[i]) allDone = false;
        }

        time += Time.deltaTime * 8;

        if (allDone)
        {
            string fullName = "";
            for (int i = 0; i < 4; i++)
            {
                fullName += alphabet[nameVals[i]];
            }
            HighScoreRecorder.SetName(fullName);
            sceneHandler.StartSceneExitTimer();
            Destroy(this);
        }

        else if (time > 1)
        {
            time = 0;
            for (int i = 0; i < 4; i++)
            {
                if (editable[i])
                {
                    float input = Input.GetAxisRaw("Vertical" + (i + 1));
                    if (input < 0)
                    {
                        nameVals[i] = (nameVals[i] + 1) % ALPHA;
                        texts[i].text = alphabet[nameVals[i]].ToString();
                    }
                    else if (input > 0)
                    {
                        nameVals[i] = (nameVals[i] + ALPHA - 1) % ALPHA;
                        texts[i].text = alphabet[nameVals[i]].ToString();
                    }
                }
            }
        }
		
	}
}
