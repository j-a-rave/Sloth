using UnityEngine;
using UnityEngine.UI;

public class HoldToQuitHandler : MonoBehaviour {

    Image backDial;
    Text backText;
    float textLerp;
    Color textColor;

	void Start () {
        backDial = GetComponent<Image>();
        backDial.fillAmount = 0;

        backText = GetComponentInChildren<Text>();
        textLerp = 0;
        textColor = backText.color;
	}
	
	void Update () {
        textColor.a = Mathf.Lerp(textColor.a, textLerp, 0.05f);
        backText.color = textColor;

        if (Input.GetButtonDown("Cancel"))
        {
            textLerp = 10;
        }

		if (backDial.fillAmount >= 1)
        {
            SceneHandler.ExitScene();
        }
        else if (backDial.fillAmount >= 0)
        {
            if (Input.GetButton("Cancel"))
            {
                backDial.fillAmount += Time.deltaTime;
            }
            else backDial.fillAmount -= Time.deltaTime / 2f;
            if (backDial.fillAmount == 0)
            {
                textLerp = 0;
            }
        }
	}
}
