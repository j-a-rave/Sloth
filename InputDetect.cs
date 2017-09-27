using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputDetect : MonoBehaviour {

    public Image[] controllerImages;
    public Sprite[] noJoystickSprite;
    public Sprite[] joystickSprites;

    int joystickLength;
    int count;

    void Start()
    {
        joystickLength = 0;
    }

	void Update () {
        count = GetValidControllerCount();

	    if (joystickLength != count)
        {
            joystickLength = count;
            for(int i = 0; i < 4; i++)
            {
                if (i < joystickLength)
                {
                    controllerImages[i].sprite = joystickSprites[i];
                }
                else
                {
                    controllerImages[i].sprite = noJoystickSprite[i];
                }
            }
        }
	}

    int GetValidControllerCount()
    {
        int result = 0;
        foreach(string name in Input.GetJoystickNames())
        {
            if (name != "") result++;
        }
        return result;
    }

    public void Deactivate()
    {
        foreach(Image image in controllerImages)
        {
            image.gameObject.SetActive(false);
        }
    }
}
